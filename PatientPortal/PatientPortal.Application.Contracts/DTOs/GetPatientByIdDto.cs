namespace PatientPortal.Application.Contracts.DTOs;

public class GetPatientByIdDto
{
   public Guid Id { get; set; }
   public string Name { get; set; }
   public uint Age { get; set; }
   public string Gender { get; set; }
   public string BloodGroup { get; set; }
   public int EpilepsyStatus { get; set; }
   public string? Address { get; set; }
   public string? PhoneNumber { get; set; }
   public string DiseaseName { get; set; }
   public List<string> NcdDetails { get; set; }
   public List<string> AllergiesDetails { get; set; }
}