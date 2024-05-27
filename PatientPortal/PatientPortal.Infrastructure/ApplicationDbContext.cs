using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain.Patient;
using PatientPortal.Infrastructure.Configurations;
using PatientPortal.Infrastructure.IdentityMembers;

namespace PatientPortal.Infrastructure;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(PatientTypeConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(NcdTypeConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(DiseaseInfoTypeConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(AllergieTypeConfiguration).Assembly);

        builder.Entity<NcdDetail>()
            .Property(x => x.NcdId)
            .IsRequired();
        
        builder.Entity<NcdDetail>()
            .Property(x => x.PatientId)
            .IsRequired();
        
        builder.Entity<AllergiesDetail>()
            .Property(x => x.AllergyId)
            .IsRequired();
        
        builder.Entity<AllergiesDetail>()
            .Property(x => x.PatientId)
            .IsRequired();

    }
}
