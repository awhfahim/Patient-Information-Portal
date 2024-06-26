﻿using Autofac;
using ErrorOr;
using Mapster;
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
    public int EpilepsyStatus { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid DiseaseInfoId { get; set; }
    public List<string> NcdDetails { get; set; }
    public List<string> AllergiesDetails { get; set; }
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

    public async Task<ErrorOr<Guid>> HandleAsync(CancellationToken cancellationToken)
    {
        var patientCreateDto = _mapper.Map<PatientCreateDto>(this);
        return await _patientManagementService.AddPatientAsync(patientCreateDto, cancellationToken).ConfigureAwait(false);
    }
}