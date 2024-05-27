using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientPortal.Domain.NCD;

namespace PatientPortal.Infrastructure.Configurations;

public class NcdTypeConfiguration : IEntityTypeConfiguration<NCD>
{
    public void Configure(EntityTypeBuilder<NCD> builder)
    {
        builder
            .ToTable("NCDs")
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder
            .HasIndex(x => x.Name)
            .IsUnique();
            
          builder  
            .HasKey(x => x.Id)
            .HasName("PK_NCDs");
    }
}