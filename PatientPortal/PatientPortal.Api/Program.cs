using Autofac;
using Autofac.Extensions.DependencyInjection;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using PatientPortal.Api;
using PatientPortal.Api.Extensions;
using PatientPortal.Api.Options;
using PatientPortal.Application;
using PatientPortal.Infrastructure;
using Serilog;
using Serilog.Events;


Env.Load();

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    
    //Add Serilog Configuration
    builder.Services.AddSerilog((_, lc) => lc
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(configuration)
        .WriteTo.ConfigureEmailSink(builder.Configuration));
    
    //Add Autofac Configuration
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(b =>
    {
        b.RegisterModule(new ApplicationModule());
        b.RegisterModule(new InfrastructureModule());
        b.RegisterModule(new ApiModule());
    });
    
    // Add services to the container.
    var connectionString= builder.Configuration
        .GetRequiredSection(ConnectionStringOptions.SectionName)
        .GetValue<string>(nameof(ConnectionStringOptions.PatientPortalDb));

    var migrationAssembly = typeof(ApplicationDbContext).Assembly.GetName().Name;
    
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(connectionString, 
                m => m.MigrationsAssembly(migrationAssembly));
        
        ArgumentNullException.ThrowIfNull(connectionString, nameof(ConnectionStringOptions));
    });
    
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync();
    return 0;
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Application start-up failed");
    return 1;
}
finally
{
    Log.Information("Shut down complete");
    await Log.CloseAndFlushAsync();
}

