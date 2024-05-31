using PatientPortal.Domain.PatientAggregate;
using PatientPortal.Domain.Primitives;

namespace PatientPortal.Domain.Repositories;

public interface IPatientRepository : IRepositoryBase<Patient, Guid>
{
    Task<List<PatientData>> GetAllPatientsAsync(CancellationToken cancellationToken);
    Task<(PatientData, List<string> AllergyNames, List<string> NcdNames)?> MyGetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Patient> FetchByIdAsync(Guid id, CancellationToken cancellationToken);
}