namespace PatientPortal.Application.Contracts.DTOs;

public record GetPatientByIdDto(
Guid Id, 
string Name, 
uint Age, 
string Gender, 
string BloodGroup, 
int EpilepsyStatus, 
string? Address, 
string? PhoneNumber, 
Guid DiseaseInfoId, 
List<string> NcdDetails, 
List<string> AllergiesDetails);