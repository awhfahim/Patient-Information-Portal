using ErrorOr;
using Mapster;
using PatientPortal.Application.Contracts.DTOs.Disease;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Application.Services;

public class DiseaseManagementService(IApplicationUnitOfWork unitOfWork) : IDiseaseManagementService
{
    public async Task<ErrorOr<List<DiseaseDto>>> GetDiseasesAsync(CancellationToken cancellationToken)
    {
        var diseases = await unitOfWork.DiseaseInfoRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if(diseases.Count == 0)
            return Error.NotFound("Diseases not found", "Diseases not found");
        return await diseases.BuildAdapter().AdaptToTypeAsync<List<DiseaseDto>>().ConfigureAwait(false);
    }
}