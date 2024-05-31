using Mapster;
using PatientPortal.Api.RequestHandlers;
using PatientPortal.Application.Contracts.DTOs;
using PatientPortal.Domain.PatientAggregate;
using Epilepsy = PatientPortal.Domain.PatientAggregate.Epilepsy;

namespace PatientPortal.Api.Extensions;

public static class MapsterExtension
{
    public static void MapsterConfig(this IServiceCollection services)
    {
        
        var config = TypeAdapterConfig.GlobalSettings;

        TypeAdapterConfig<PatientCreateRequestHandler, PatientCreateDto>
            .NewConfig()
            .MapWith(src => new PatientCreateDto
            (
                src.Name,
                src.Age,
                src.Gender,
                src.BloodGroup,
                src.EpilepsyStatus,
                src.DiseaseInfoId,
                src.NcdDetails,
                src.AllergiesDetails,
                src.PhoneNumber,
                src.Address
            ));
        
        TypeAdapterConfig<PatientCreateDto, Patient>
            .NewConfig()
            .MapWith(src => new Patient
            (
                src.Name,
                src.Age,
                src.Gender,
                src.BloodGroup,
                src.DiseaseInfoId,
                (Epilepsy)src.EpilepsyStatus,
                src.Address,
                src.PhoneNumber
            ));

    }
}