using ErrorOr;
using PatientPortal.Application.Contracts.DTOs;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Api.RequestHandlers;

public class GetPatientRequestHandler(IPatientManagementService patientManagementService)
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint Age { get; set; }
    public string Gender { get; set; }
    public string BloodGroup { get; set; }
    public int EpilepsyStatus { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid DiseaseInfoId { get; set; }
    public List<string> NcdDetails { get; set; }
    public List<string> AllergiesDetails { get; set; }
    public Task<ErrorOr<List<GetPatientDto>>> GetPatientsAsync(CancellationToken cancellationToken)
    {
         return patientManagementService.GetPatientsAsync(cancellationToken);
    }

    public async Task<IErrorOr<GetPatientRequestHandler>> GetPatientAsync(Guid id, CancellationToken cancellationToken)
    {
        var patient = await patientManagementService.GetPatientAsync(id, cancellationToken).ConfigureAwait(false);
        return null;
    }
}