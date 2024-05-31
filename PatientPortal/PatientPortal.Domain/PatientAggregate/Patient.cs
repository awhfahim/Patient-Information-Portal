namespace PatientPortal.Domain.PatientAggregate;

public sealed class Patient(
    string name,
    uint age,
    string gender,
    string bloodGroup,
    Guid diseaseInfoId,
    Epilepsy epilepsyStatus,
    string? address = null,
    string? phoneNumber = null)
    : IEntity<Guid>
{
    public string Name { get; set; } = name;
    public uint Age { get; set; } = age;
    public string Gender { get; set; } = gender;
    public string BloodGroup { get; set; } = bloodGroup;
    public Epilepsy EpilepsyStatus { get; set; } = epilepsyStatus;
    public string? Address { get; set; } = address;
    public string? PhoneNumber { get; set; } = phoneNumber;
    public Guid Id { get; set; }

    public Guid DiseaseInfoId { get; set; } = diseaseInfoId;
    public IList<NcdDetail> NcdDetails { get; set; }
    public IList<AllergiesDetail> AllergiesDetails { get; set; }
    
    public void AddNcdDetail(IList<NcdDetail> ncdDetail)
    {
        NcdDetails = ncdDetail;
    }
    
    public void AddAllergiesDetail(IList<AllergiesDetail> allergiesDetail)
    {
        AllergiesDetails = allergiesDetail;
    }
    
}

public enum Epilepsy
{
    Yes = 1,
    No = 2
} 