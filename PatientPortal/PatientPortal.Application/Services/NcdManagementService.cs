using ErrorOr;
using Mapster;
using PatientPortal.Application.Contracts.DTOs.Ncd;
using PatientPortal.Application.Contracts.ServiceInterfaces;
using PatientPortal.Domain.NCD;

namespace PatientPortal.Application.Services;

public class NcdManagementService(IApplicationUnitOfWork unitOfWork) : INcdManagementService
{
    public async Task<ErrorOr<string>> CreateNcdAsync(NcdDto ncdDto, CancellationToken cancellationToken)
    {
        var ncd = await ncdDto.BuildAdapter().AdaptToTypeAsync<Ncd>().ConfigureAwait(false);
        await unitOfWork.NcdRepository.AddAsync(ncd, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveAsync().ConfigureAwait(false);
        
        return ncd.Name; 
    }

    public async Task<ErrorOr<string>> UpdateNcdAsync(UpdateNcdDto ncdDto, CancellationToken cancellationToken)
    {
        var ncd = await unitOfWork.NcdRepository.GetByIdAsync(ncdDto.Id).ConfigureAwait(false);
        if (ncd is null)
        {
            return Error.NotFound("Ncd not found");
        }
        ncdDto.BuildAdapter().AdaptTo(ncd);
        await unitOfWork.SaveAsync().ConfigureAwait(false);
        return ncd.Name;
    }


    public async Task<ErrorOr<string>> DeleteNcdAsync(Guid id, CancellationToken cancellationToken)
    {
        var ncd = await unitOfWork.NcdRepository.GetByIdAsync(id).ConfigureAwait(false);
        if (ncd is null)
        {
            return Error.NotFound("Ncd not found");
        }
        await unitOfWork.NcdRepository.RemoveAsync(ncd, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveAsync().ConfigureAwait(false);
        return ncd.Name;
    }

    public async Task<ErrorOr<NcdDto>> GetNcdByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var ncd = await unitOfWork.NcdRepository.GetByIdAsync(id).ConfigureAwait(false);
        if (ncd is null)
        {
            return Error.NotFound("Ncd not found", $"Ncd not found with the related {id}");
        }
        return await ncd.BuildAdapter().AdaptToTypeAsync<NcdDto>().ConfigureAwait(false);
    }
    
    public async Task<ErrorOr<IList<NcdDto>>> GetNcdsAsync(CancellationToken cancellationToken)
    {
        var ncds = await unitOfWork.NcdRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (ncds.Count == 0)
        {
            return Error.NotFound("Ncds not found", "Ncds not found");
        }
        return await ncds.BuildAdapter().AdaptToTypeAsync<List<NcdDto>>().ConfigureAwait(false);
    }
}