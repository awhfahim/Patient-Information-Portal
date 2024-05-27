namespace PatientPortal.Domain.NCD;

public class NCD(string name) : IEntity<Guid>
{
    public string Name { get; } = name;
    public Guid Id { get; set; }
}