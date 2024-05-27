using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientPortal.Domain.Allergies;

namespace PatientPortal.Infrastructure.Configurations;

public class AllergieTypeConfiguration : IEntityTypeConfiguration<Allergies>
{
    public void Configure(EntityTypeBuilder<Allergies> builder)
    {
        builder
            .ToTable("Allergies")
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder
            .HasIndex(x => x.Name)
            .IsUnique();
            
          builder  
            .HasKey(x => x.Id)
            .HasName("PK_Allergies");
    }
}