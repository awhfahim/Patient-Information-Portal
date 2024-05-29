using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientPortal.Domain.Allergies;

namespace PatientPortal.Infrastructure.Configurations;

public class AllergieTypeConfiguration : IEntityTypeConfiguration<Allergie>
{
    public void Configure(EntityTypeBuilder<Allergie> builder)
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

        builder.HasData([
            new("Peanut") { Id = Guid.Parse("1506A76E-7E7A-4C66-9376-FA18B8C5823D") },
            new("Shellfish") { Id = Guid.Parse("6F0C798C-BDB0-43A8-A2AB-E23A74BBB99A") },
            new("Pollen") { Id = Guid.Parse("AC98F120-06A6-4A6B-8736-08C523B01E39") },
            new("Dust Mite") { Id = Guid.Parse("6D298DF9-BF03-469F-BE93-FB62D73078C2") },
            new("Penicillin") { Id = Guid.Parse("FCBA47B1-961C-4B9D-9A39-58EE4EDC027E") }
        ]);
    }
}