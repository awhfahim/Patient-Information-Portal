using PatientPortal.Application.Contracts.Utilities;

namespace PatientPortal.Application;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNow() => DateTime.Now;
}