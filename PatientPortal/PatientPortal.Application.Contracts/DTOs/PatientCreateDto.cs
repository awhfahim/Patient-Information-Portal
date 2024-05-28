namespace PatientPortal.Application.Contracts.DTOs;

public record PatientCreateDto(
    string Name,
    uint Age,
    string Gender,
    string BloodGroup,
    Epilepsy EpilepsyStatus,
    Guid DiseaseInfoId,
    IList<NcdDetailDto> NcdDetails,
    IList<AllergiesDetailDto> AllergiesDetails,
    string? PhoneNumber = null,
    string? Address = null
);

public enum Epilepsy
{
    Yes,
    No
}