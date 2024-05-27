using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Api.Options;

public class ConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings";
    [Required] public required string PatientPortalDb { get; init; }
}