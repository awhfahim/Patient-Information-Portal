namespace PatientPortal.Domain.Patient;

public sealed class Patient(
    string name,
    uint age,
    string gender,
    string bloodGroup,
    Epilepsy epilepsyStatus,
    string? address = null,
    string? phoneNumber = null)
    : IEntity<Guid>
{
    public string Name { get; } = name;
    public uint Age { get; } = age;
    public string Gender { get; } = gender;
    public string BloodGroup { get; } = bloodGroup;
    public Epilepsy EpilepsyStatus { get; } = epilepsyStatus;
    public string? Address { get; } = address;
    public string? PhoneNumber { get; } = phoneNumber;
    public Guid Id { get; set; }
    
    public Guid DiseaseInfoId { get; set; }
    public IList<NcdDetail> NcdDetails { get; set; }
    public IList<AllergiesDetail> AllergiesDetails { get; set; }
}

public enum Epilepsy
{
    Yes,
    No
} 