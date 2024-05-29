namespace PatientPortal.Domain.Allergies;

public sealed class Allergie(string name) : IEntity<Guid>
{
    public string Name { get; } = name;
    public Guid Id { get; set; }
}