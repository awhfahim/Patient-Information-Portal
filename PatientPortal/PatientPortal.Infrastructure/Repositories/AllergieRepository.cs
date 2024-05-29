using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain.Allergies;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Infrastructure.Repositories;

public class AllergieRepository(ApplicationDbContext context) : Repository<Allergie, Guid>(context), IAllergieRepository;