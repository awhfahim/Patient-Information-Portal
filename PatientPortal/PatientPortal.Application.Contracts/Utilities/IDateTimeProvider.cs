namespace PatientPortal.Application.Contracts.Utilities;

public interface IDateTimeProvider
{
    DateTime GetUtcNow();
}