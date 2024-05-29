using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using PatientPortal.Web.Models.Patients;
using PatientPortal.Web.Others;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
//builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<PatientCreateModel>();

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
    options.AppendTrailingSlash = false;
});

builder.Services.AddControllersWithViews(opts =>
    {
        opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        opts.OutputFormatters.RemoveType<StringOutputFormatter>();
        opts.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
    }
).AddJsonOptions(opts => { opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");

app.Run();

