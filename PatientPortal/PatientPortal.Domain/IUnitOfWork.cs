using System.Data.Common;

namespace PatientPortal.Domain
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        void Save();
        Task SaveAsync();
        DbTransaction BeginTransaction();
    }
}
