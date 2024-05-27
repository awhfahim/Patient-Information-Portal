namespace PatientPortal.Domain.Patient;

public sealed class AllergiesDetail(Guid patientId, Guid allergyId) : IEntity<Guid>
{
    public Guid PatientId { get; } = patientId;
    public Guid AllergyId { get; } = allergyId;
    public Guid Id { get; set; }
}