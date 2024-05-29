using PatientPortal.Domain;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Application;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    IPatientRepository PatientRepository { get; }
    IAllergieRepository AllergieRepository { get; }
    IDiseaseInfoRepository DiseaseInfoRepository { get; }
    INcdRepository NcdRepository { get; }
}