using PatientPortal.Application;

namespace PatientPortal.Infrastructure.UnitOfWorks;

public class ApplicationUnitOfWork(ApplicationDbContext context) : UnitOfWork(context), IApplicationUnitOfWork
{
    
}