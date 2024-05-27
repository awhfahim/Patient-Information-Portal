namespace PatientPortal.Domain.Patient;

public sealed class NcdDetail(Guid patientId, Guid ncdId) : IEntity<Guid>
{
    public Guid PatientId { get; } = patientId;
    public Guid NcdId { get; } = ncdId;
    public Guid Id { get; set; }
}