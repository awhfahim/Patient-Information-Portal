using PatientPortal.Application.Contracts.Utilities;

namespace PatientPortal.Application;

public class GuidProvider: IGuidProvider
{
    public Guid GetGuid() => Guid.NewGuid();
}