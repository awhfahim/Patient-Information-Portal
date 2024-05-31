using Autofac;
using ErrorOr;
using MapsterMapper;
using PatientPortal.Application.Contracts.DTOs;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Api.RequestHandlers;

public class PatientUpdateRequestHandler
{
    private IPatientManagementService _patientManagementService;
    private IMapper _mapper;
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint Age { get; set; }
    public string Gender { get; set; }
    public string BloodGroup { get; set; }
    public int EpilepsyStatus { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid DiseaseInfoId { get; set; }
    
    public PatientUpdateRequestHandler(){}
    public PatientUpdateRequestHandler(IPatientManagementService patientManagementService, IMapper mapper)
    {
        _patientManagementService = patientManagementService;
        _mapper = mapper;
    }

    internal void Resolve(ILifetimeScope scope)
    {
        _patientManagementService = scope.Resolve<IPatientManagementService>();
        _mapper = scope.Resolve<IMapper>();
    }
    

    public async Task<ErrorOr<Guid>> UpdatePatientAsync(CancellationToken cancellationToken)
    {
        var patientUpdateDto = _mapper.Map<PatientUpdateDto>(this);
        return await _patientManagementService.UpdatePatientAsync(patientUpdateDto, cancellationToken);
    }
}