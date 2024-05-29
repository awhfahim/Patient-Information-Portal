using ErrorOr;
using PatientPortal.Application.Contracts.DTOs;

namespace PatientPortal.Application.Contracts.ServiceInterfaces;

public interface IPatientManagementService
{
    Task<ErrorOr<Guid>> AddPatientAsync(PatientCreateDto patientCreateDto, CancellationToken cancellationToken);
    Task<ErrorOr<List<GetPatientDto>>> GetPatientsAsync(CancellationToken cancellationToken);
}