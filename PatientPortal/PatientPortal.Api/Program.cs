using Autofac;
using Autofac.Extensions.DependencyInjection;
using DotNetEnv;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using PatientPortal.Api;
using PatientPortal.Api.ExceptionHandlers;
using PatientPortal.Api.Extensions;
using PatientPortal.Api.Options;
using PatientPortal.Api.Validators.DIExtensionsForFluentValidator;
using PatientPortal.Application;
using PatientPortal.Infrastructure;
using Serilog;
using Serilog.Events;

Env.Load();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
            m => m.MigrationsAssembly(migrationAssembly))
        // Uncomment to see the Sql Generated By EF
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
        .LogTo(Console.WriteLine, LogLevel.Debug); // Log SQL details at Information level
        //
        options.UseLoggerFactory(LoggerFactory.Create(b => b.AddConsole())); // Log more details
        ArgumentNullException.ThrowIfNull(connectionString, nameof(ConnectionStringOptions));
    });
     
    builder.Services.AddMapster();
    builder.Services.MapsterConfig();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    });
    
    //Add FluentValidation
    builder.Services.AddFluentValidationAutoValidation(s => s.DisableDataAnnotationsValidation = false)
        .AddFluentValidationClientsideAdapters();
    builder.Services.AddFluentValidationServices();
    
    builder.Services.AddControllers();
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    // Add Exception Handling
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    var app = builder.Build();
    
    app.UseExceptionHandler();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        //app.UseSwagger();
       // app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors();
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

