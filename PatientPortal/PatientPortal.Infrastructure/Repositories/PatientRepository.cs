using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain.PatientAggregate;
using PatientPortal.Domain.Primitives;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Infrastructure.Repositories;

public class PatientRepository(ApplicationDbContext context) : Repository<Patient, Guid>(context), IPatientRepository
{
    public Task<List<PatientData>> GetAllPatientsAsync(CancellationToken cancellationToken)
        => context.Patients
            .Select(x => new PatientData { Id = x.Id, Name = x.Name, Gender = x.Gender, PhoneNumber = x.PhoneNumber, Address = x.Address })
            .ToListAsync(cancellationToken);
}