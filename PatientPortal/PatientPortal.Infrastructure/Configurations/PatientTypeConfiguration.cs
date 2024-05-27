using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientPortal.Domain.Patient;

namespace PatientPortal.Infrastructure.Configurations;

public class PatientTypeConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.BloodGroup).IsRequired();
        builder.Property(x => x.EpilepsyStatus).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(200);
        builder.Property(x => x.PhoneNumber).HasMaxLength(15);
        
        builder
            .HasMany(x => x.NcdDetails)
            .WithOne()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(x => x.AllergiesDetails)
            .WithOne()
            .HasForeignKey(x => x.AllergyId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }
}