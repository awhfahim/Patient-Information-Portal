using FluentValidation;
using PatientPortal.Api.RequestHandlers;
using PatientPortal.Application.Contracts.DTOs;

namespace PatientPortal.Api.Validators;

public class PatientRequestHandlerValidator : AbstractValidator<PatientCreateRequestHandler>
{
    public PatientRequestHandlerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is Required")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters long")
            .Must(x => x.All(char.IsLetter)).WithMessage("Name must contain only letters");

        RuleFor(x => x.Age)
            .NotEmpty().WithMessage("Age is required");
        
        RuleFor(x => x.DiseaseInfoId)
            .NotEmpty().WithMessage("DiseaseInfoId is required")
            .Must(x => x != Guid.Empty).WithMessage("DiseaseInfoId must not be empty");
        
        RuleFor(x => x.BloodGroup)
            .NotEmpty().WithMessage("BloodGroup is required")
            .Must(x => x == "A+" || x == "A-" || x == "B+" || x == "B-" || x == "AB+" || x == "AB-" || x == "O+" || x == "O-")
            .WithMessage("Invalid BloodGroup");
        
        RuleFor(x => x.EpilepsyStatus)
            .NotEmpty().WithMessage("EpilepsyStatus is required")
            .Must(x => x == Epilepsy.Yes || x == Epilepsy.No)
            .WithMessage("Invalid EpilepsyStatus");
    }
}