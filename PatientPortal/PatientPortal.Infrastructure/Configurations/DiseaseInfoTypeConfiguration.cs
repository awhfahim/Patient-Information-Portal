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

          builder.HasData([
                new("Tuberculosis") { Id = Guid.Parse("10AE8881-BAC9-4BC8-B2B9-E2515E510DB4") },
                new("Malaria") { Id = Guid.Parse("7A8C12B1-19E8-454A-8D5E-1B84D9EA3FB1") },
                new("HIV/AIDS") { Id = Guid.Parse("DC239FCC-E87B-4F44-8F12-79C7FC05173A") },
                new("Diarrhea") { Id = Guid.Parse("5A590390-2ADF-4013-8084-15E0A9824063") },
                new("Pneumonia") { Id = Guid.Parse("9246D879-589E-4CD1-AA66-54D44F5EB334") }
          ]);
    }
}