using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain.DiseaseInfo;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Infrastructure.Repositories;

public class DiseaseRepository(ApplicationDbContext context) : Repository<DiseaseInfo, Guid>(context), IDiseaseInfoRepository;