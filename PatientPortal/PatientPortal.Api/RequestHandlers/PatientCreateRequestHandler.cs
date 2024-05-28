using Autofac;
using ErrorOr;
using MapsterMapper;
using PatientPortal.Application.Contracts.DTOs;
using PatientPortal.Application.Contracts.ServiceInterfaces;

namespace PatientPortal.Api.RequestHandlers;

public class PatientCreateRequestHandler
{
    private IPatientManagementService _patientManagementService;
    private IMapper _mapper;
    
    public string Name { get; set; }
    public uint Age { get; set; }
    public string Gender { get; set; }
    public string BloodGroup { get; set; }
    public Epilepsy EpilepsyStatus { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid DiseaseInfoId { get; set; }
    public IList<NcdDetailDto> NcdDetails { get; set; }
    public IList<AllergiesDetailDto> AllergiesDetails { get; set; }
    public PatientCreateRequestHandler(){}
    public PatientCreateRequestHandler(IPatientManagementService patientManagementService, IMapper mapper)
    {
        _patientManagementService = patientManagementService;
        _mapper = mapper;
    }

    internal void Resolve(ILifetimeScope scope)
    {
        _patientManagementService = scope.Resolve<IPatientManagementService>();
        _mapper = scope.Resolve<IMapper>();
    }

    public async Task<ErrorOr<Guid>> HandleAsync()
    {
        var patientCreateDto = _mapper.Map<PatientCreateDto>(this);
        return await _patientManagementService.AddPatientAsync(patientCreateDto).ConfigureAwait(false);
    }
}