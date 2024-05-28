using FluentValidation;
using PatientPortal.Api.RequestHandlers;

namespace PatientPortal.Api.Validators.DIExtensionsForFluentValidator;

public static class FluentValidationServices
{
    public static void AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<PatientCreateRequestHandler>, PatientRequestHandlerValidator>();
        //services.AddValidatorsFromAssemblyContaining<RegistrationRequestValidator>();
    }
}