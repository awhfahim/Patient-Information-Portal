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

          builder.HasData([
              new("Cardiovascular") { Id = Guid.Parse("844153BB-9033-4718-9F24-2A73AB0430FD") },
              new("Diabetes") { Id = Guid.Parse("A4F38F24-9FC2-4A5B-85DD-0053F24271AA") },
              new("Chronic Respiratory") { Id = Guid.Parse("3D4E9629-4FE3-4865-96AB-183B6F542C39") },
              new("Cancer") { Id = Guid.Parse("C4468BB9-60D2-44E9-A656-DE576C88BF3D") },
              new("Mental Health Disorders") { Id = Guid.Parse("AAD6E4C3-4A09-48E3-B77D-88F43B7F95BC") },
          ]);
    }
}