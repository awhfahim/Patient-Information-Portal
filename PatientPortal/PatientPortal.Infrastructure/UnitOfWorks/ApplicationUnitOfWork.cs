using PatientPortal.Application;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Infrastructure.UnitOfWorks;

public class ApplicationUnitOfWork(ApplicationDbContext context, IPatientRepository patients) : UnitOfWork(context), IApplicationUnitOfWork
{
    public IPatientRepository PatientRepository { get; } = patients;
}