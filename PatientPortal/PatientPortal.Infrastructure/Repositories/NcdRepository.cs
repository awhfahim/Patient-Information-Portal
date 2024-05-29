using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain.NCD;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Infrastructure.Repositories;

public class NcdRepository(ApplicationDbContext context) : Repository<Ncd, Guid>(context), INcdRepository;