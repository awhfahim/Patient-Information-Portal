using ErrorOr;
using PatientPortal.Application.Contracts.DTOs.Allergie;

namespace PatientPortal.Application.Contracts.ServiceInterfaces;

public interface IAllergieManagementService
{
    Task<ErrorOr<IList<AllergieDto>>> GetAllergiesAsync(CancellationToken cancellationToken);
}