using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain.PatientAggregate;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Infrastructure.Repositories;

public class PatientRepository(ApplicationDbContext context) : Repository<Patient, Guid>(context), IPatientRepository
{
    
}