using ErrorOr;
using Mapster;
using PatientPortal.Application.Contracts.DTOs.Allergie;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Application.Services;

public class AllergieManagementService(IApplicationUnitOfWork unitOfWork) : IAllergieManagementService
{
    public async Task<ErrorOr<IList<AllergieDto>>> GetAllergiesAsync(CancellationToken cancellationToken)
    {
        var allergies =await unitOfWork.AllergieRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (allergies.Count == 0)
        {
            return Error.NotFound("Allergies not found", "Allergies not found");
        }

        return await allergies.BuildAdapter().AdaptToTypeAsync<List<AllergieDto>>().ConfigureAwait(false);
    }
}