namespace PatientPortal.Domain.DiseaseInfo;

public sealed class DiseaseInfo(string name) : IEntity<Guid>
{
    public string Name { get; } = name;
    public Guid Id { get; set; }
}