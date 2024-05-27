namespace PatientPortal.Domain.Allergies;

public sealed class Allergies(string name) : IEntity<Guid>
{
    public string Name { get; } = name;
    public Guid Id { get; set; }
}