using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain;

namespace PatientPortal.Infrastructure.UnitOfWorks;

public class UnitOfWork(DbContext context) : IUnitOfWork
{
    private readonly DbContext _context = context;

    public void Dispose() => _context?.Dispose();

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

    public void Save() => _context?.SaveChanges();

    public Task SaveAsync() => _context.SaveChangesAsync();

    public DbTransaction BeginTransaction()
    {
        throw new NotImplementedException();
    }
}