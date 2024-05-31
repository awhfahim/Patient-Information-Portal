namespace PatientPortal.Web.Models.Patients;

public class PatientUpdateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint Age { get; set; }
    public string Gender { get; set; }
    public string BloodGroup { get; set; }
    public int EpilepsyStatus { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

}