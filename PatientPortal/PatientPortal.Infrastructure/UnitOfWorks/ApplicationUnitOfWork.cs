using PatientPortal.Application;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Infrastructure.UnitOfWorks;

public class ApplicationUnitOfWork(ApplicationDbContext context, IPatientRepository patients, 
    IAllergieRepository allergieRepository, IDiseaseInfoRepository diseaseInfoRepository, 
    INcdRepository ncdRepository) : UnitOfWork(context), IApplicationUnitOfWork
{
    public IPatientRepository PatientRepository { get; } = patients;
    public IAllergieRepository AllergieRepository { get; } = allergieRepository;
    public IDiseaseInfoRepository DiseaseInfoRepository { get; } = diseaseInfoRepository;
    public INcdRepository NcdRepository { get; } = ncdRepository;
}