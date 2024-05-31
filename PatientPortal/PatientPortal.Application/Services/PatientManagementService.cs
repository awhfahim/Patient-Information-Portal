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
                .Select(allergiesId => new AllergiesDetail(patient.Id, Guid.Parse(allergiesId))).ToList();
            
            patient.NcdDetails = patientCreateDto.NcdDetails
                .Select(ncdId => new NcdDetail(patient.Id, Guid.Parse(ncdId))).ToList();

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

    public async Task<ErrorOr<GetPatientByIdDto>> GetPatientAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var p = await unitOfWork.PatientRepository.MyGetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            var result = await p.Value.Item1.BuildAdapter().AdaptToTypeAsync<GetPatientByIdDto>();
            result.AllergiesDetails = p.Value.AllergyNames;
            result.NcdDetails = p.Value.NcdNames;
            return result;
        }
        catch (Exception exception)
        {
            return Error.Failure("Unable to Fetch Data");
        }
    }

    public async Task<ErrorOr<Guid>> DeletePatientAsync(Guid id, CancellationToken cancellationToken)
    {  
        await unitOfWork.PatientRepository.RemoveAsync(id, cancellationToken).ConfigureAwait(false);
        await unitOfWork.SaveAsync();
        return id;
    }

    public async Task<ErrorOr<Guid>> UpdatePatientAsync(PatientUpdateDto patientUpdateDto, CancellationToken cancellationToken)
    {
        try
        {
            var patient = await unitOfWork.PatientRepository.GetByIdAsync(patientUpdateDto.Id, cancellationToken);
            patientUpdateDto.Adapt(patient);
            await unitOfWork.SaveAsync();
            return patient.Id;
        }
        catch (Exception e)
        {
            return Error.Failure("Failed To Update Patient");
        }
    }
} 