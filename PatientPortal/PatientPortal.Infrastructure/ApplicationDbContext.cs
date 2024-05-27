using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientPortal.Infrastructure.IdentityMembers;

namespace PatientPortal.Infrastructure;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser,AppRole,Guid>(options);
