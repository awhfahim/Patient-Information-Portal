using ErrorOr;
using PatientPortal.Application.Contracts.DTOs.Ncd;

namespace PatientPortal.Application.Contracts.ServiceInterfaces;

public interface INcdManagementService
{
    Task<ErrorOr<string>> CreateNcdAsync(NcdDto ncdDto, CancellationToken cancellationToken);
    Task<ErrorOr<string>> UpdateNcdAsync(UpdateNcdDto ncdDto, CancellationToken cancellationToken);
    Task<ErrorOr<string>> DeleteNcdAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<NcdDto>> GetNcdByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<IList<NcdDto>>> GetNcdsAsync(CancellationToken cancellationToken);
}