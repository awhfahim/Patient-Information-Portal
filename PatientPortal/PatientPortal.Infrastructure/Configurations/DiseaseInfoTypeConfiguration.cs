using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientPortal.Domain.DiseaseInfo;

namespace PatientPortal.Infrastructure.Configurations;

public class DiseaseInfoTypeConfiguration : IEntityTypeConfiguration<DiseaseInfo>
{
    public void Configure(EntityTypeBuilder<DiseaseInfo> builder)
    {
        builder
            .ToTable("DiseaseInfos")
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder
            .HasIndex(x => x.Name)
            .IsUnique();
            
          builder  
            .HasKey(x => x.Id)
            .HasName("PK_DiseaseInfos");
    }
}