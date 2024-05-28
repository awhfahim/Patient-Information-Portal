using PatientPortal.Domain.PatientAggregate;

namespace PatientPortal.Domain.Repositories;

public interface IPatientRepository : IRepositoryBase<Patient, Guid>
{
    
}