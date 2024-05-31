using PatientPortal.Domain.PatientAggregate;
using PatientPortal.Domain.Primitives;

namespace PatientPortal.Domain.Repositories;

public interface IPatientRepository : IRepositoryBase<Patient, Guid>
{
    Task<List<PatientData>> GetAllPatientsAsync(CancellationToken cancellationToken);
    object GetByIdAsync2(Guid id, CancellationToken cancellationToken);
}