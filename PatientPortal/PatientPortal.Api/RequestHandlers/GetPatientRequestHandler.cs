using ErrorOr;
using PatientPortal.Application.Contracts.DTOs;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Api.RequestHandlers;

public class GetPatientRequestHandler(IPatientManagementService patientManagementService)
{
    public Task<ErrorOr<List<GetPatientDto>>> GetPatientsAsync(CancellationToken cancellationToken)
    {
         return patientManagementService.GetPatientsAsync(cancellationToken);
    }
}