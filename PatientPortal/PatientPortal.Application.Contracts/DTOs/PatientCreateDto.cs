namespace PatientPortal.Application.Contracts.DTOs;

public record PatientCreateDto(
    string Name,
    uint Age,
    string Gender,
    string BloodGroup,
    int EpilepsyStatus,
    Guid DiseaseInfoId,
    IList<string> NcdDetails,
    IList<string> AllergiesDetails,
    string? PhoneNumber = null,
    string? Address = null
);

public record PatientUpdateDto(
    Guid Id,
    string Name,
    uint Age,
    string Gender,
    string BloodGroup,
    int EpilepsyStatus,
    Guid DiseaseInfoId,
    string? PhoneNumber,
    string? Address
);

public enum Epilepsy
{
    Yes = 1,
    No = 2
}