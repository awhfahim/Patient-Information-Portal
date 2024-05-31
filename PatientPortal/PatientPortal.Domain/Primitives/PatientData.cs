namespace PatientPortal.Domain.Primitives;

public class PatientData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string DiseaseName { get;set; }
    public string BloodGroup { get; set; }
    public uint Age { get; set; }
    public uint EpilepsyStatus { get; set; }
}