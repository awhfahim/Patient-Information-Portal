using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain.Allergies;
using PatientPortal.Domain.DiseaseInfo;
using PatientPortal.Domain.NCD;
using PatientPortal.Domain.PatientAggregate;
using PatientPortal.Infrastructure.Configurations;
using PatientPortal.Infrastructure.IdentityMembers;

namespace PatientPortal.Infrastructure;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<NCD> Ncds { get; set; }
    public DbSet<DiseaseInfo> DiseaseInfos { get; set; }
    public DbSet<Allergies> Allergies { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(PatientTypeConfiguration).Assembly);

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
