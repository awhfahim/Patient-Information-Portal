using ErrorOr;
using Mapster;
using MapsterMapper;
using PatientPortal.Application.Contracts.DTOs;
using PatientPortal.Application.Contracts.ServiceInterfaces;
using PatientPortal.Application.Contracts.Utilities;
using PatientPortal.Domain.PatientAggregate;

namespace PatientPortal.Application.Services;

public class PatientManagementService(IApplicationUnitOfWork unitOfWork, IMapper mapper, 
    IGuidProvider guidProvider) : IPatientManagementService
{
    public async Task<ErrorOr<Guid>> AddPatientAsync(PatientCreateDto patientCreateDto)
    {
        try
        {
            var patient = await patientCreateDto.BuildAdapter().AdaptToTypeAsync<Patient>().ConfigureAwait(false);
            patient.Id = guidProvider.GetGuid();

            patient.AllergiesDetails = patientCreateDto.AllergiesDetails
                .Select(allergiesDetail => new AllergiesDetail(patient.Id, allergiesDetail.Id)).ToList();
            patient.NcdDetails = patientCreateDto.NcdDetails
                .Select(ncdDetail => new NcdDetail(patient.Id, ncdDetail.Id)).ToList();

            await unitOfWork.PatientRepository.AddAsync(patient).ConfigureAwait(false);
            await unitOfWork.SaveAsync().ConfigureAwait(false);

            return patient.Id;
        }
        catch (Exception e)
        {
            return Error.Unexpected("Unexpected Error Occured", e.Message);
        }
    }
}