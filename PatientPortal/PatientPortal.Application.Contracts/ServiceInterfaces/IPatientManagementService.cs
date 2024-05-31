using ErrorOr;
using PatientPortal.Application.Contracts.DTOs;

namespace PatientPortal.Application.Contracts.ServiceInterfaces;

public interface IPatientManagementService
{
    Task<ErrorOr<Guid>> AddPatientAsync(PatientCreateDto patientCreateDto, CancellationToken cancellationToken);
    Task<ErrorOr<List<GetPatientDto>>> GetPatientsAsync(CancellationToken cancellationToken);
    Task<ErrorOr<GetPatientByIdDto>> GetPatientAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<Guid>> DeletePatientAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<Guid>> UpdatePatientAsync(PatientUpdateDto patientUpdateDto, CancellationToken cancellationToken);
}