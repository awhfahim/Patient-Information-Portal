namespace PatientPortal.Application.Contracts.DTOs;

public record GetPatientDto(
    Guid Id,
    string Name,
    string Gender,
    string PhoneNumber,
    string Address
);