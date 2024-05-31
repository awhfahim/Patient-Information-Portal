using ErrorOr;
using Mapster;
using PatientPortal.Application.Contracts.DTOs;
using PatientPortal.Application.Contracts.ServiceInterfaces;
using PatientPortal.Application.Contracts.Utilities;
using PatientPortal.Domain.PatientAggregate;

namespace PatientPortal.Application.Services;

public class PatientManagementService(IApplicationUnitOfWork unitOfWork, 
    IGuidProvider guidProvider) : IPatientManagementService
{
    public async Task<ErrorOr<Guid>> AddPatientAsync(PatientCreateDto patientCreateDto, CancellationToken cancellationToken)
    {
        try
        {
            var patient = await patientCreateDto.BuildAdapter().AdaptToTypeAsync<Patient>().ConfigureAwait(false);
            patient.Id = guidProvider.GetGuid();

            patient.AllergiesDetails = patientCreateDto.AllergiesDetails
                .Select(allergiesDetail => new AllergiesDetail(patient.Id, Guid.Parse(allergiesDetail))).ToList();
            
            patient.NcdDetails = patientCreateDto.NcdDetails
                .Select(ncdDetail => new NcdDetail(patient.Id, Guid.Parse(ncdDetail))).ToList();

            await unitOfWork.PatientRepository.AddAsync(patient, cancellationToken).ConfigureAwait(false);
            await unitOfWork.SaveAsync().ConfigureAwait(false);

            return patient.Id;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Unexpected Error Occured", e.Message);
        }
    }

    public  async Task<ErrorOr<List<GetPatientDto>>> GetPatientsAsync(CancellationToken cancellationToken)
    {
      var patients = await unitOfWork.PatientRepository.GetAllPatientsAsync(cancellationToken).ConfigureAwait(false);
      return await patients.BuildAdapter().AdaptToTypeAsync<List<GetPatientDto>>().ConfigureAwait(false);
    }

    public async Task<IErrorOr<GetPatientByIdDto>> GetPatientAsync(Guid id, CancellationToken cancellationToken)
    {
        unitOfWork.PatientRepository.GetByIdAsync2(id, cancellationToken);
        return null;
    }
} 