using ErrorOr;
using PatientPortal.Application.Contracts.DTOs.Disease;

namespace PatientPortal.Application.Contracts.ServiceInterfaces;

public interface IDiseaseManagementService
{
    Task<ErrorOr<List<DiseaseDto>>> GetDiseasesAsync(CancellationToken cancellationToken);
}