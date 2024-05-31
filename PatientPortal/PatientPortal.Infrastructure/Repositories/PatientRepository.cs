using Microsoft.EntityFrameworkCore;
using PatientPortal.Domain.PatientAggregate;
using PatientPortal.Domain.Primitives;
using PatientPortal.Domain.Repositories;

namespace PatientPortal.Infrastructure.Repositories;

public class PatientRepository(ApplicationDbContext context) : Repository<Patient, Guid>(context), IPatientRepository
{
    private readonly ApplicationDbContext _context = context;

    public Task<List<PatientData>> GetAllPatientsAsync(CancellationToken cancellationToken)
        => _context.Patients
            .Select(x => new PatientData { Id = x.Id, Name = x.Name, Gender = x.Gender, PhoneNumber = x.PhoneNumber, Address = x.Address })
            .ToListAsync(cancellationToken);

    public object GetByIdAsync2(Guid id, CancellationToken cancellationToken)
    {
        var p = _context.Patients
            .Where(x => x.Id == id)
            .Include(p => p.AllergiesDetails)
            .Include(p => p.NcdDetails)
            .Select(p => new
            {
                p.Name, p.Age, p.Gender, p.Address,
                p.BloodGroup,p.PhoneNumber,p.DiseaseInfoId,
                AllergyIds = p.AllergiesDetails.Select(a => a.AllergyId).ToList(),
                NcdIds = p.NcdDetails.Select(n => n.NcdId).ToList()
            })
            .Join(_context.DiseaseInfos, p => p.DiseaseInfoId, info => info.Id, (arg1, info) => new
            {
                arg1,
                DiseaseName = info.Name
            })
            .AsSplitQuery()
            .FirstOrDefault();

        var allergieNames = _context.Allergies.Where(x => p.arg1.AllergyIds.Contains(x.Id))
            .Select(x => x.Name).ToList();
        
        var ncdNames = _context.Ncds.Where(x => p.arg1.NcdIds.Contains(x.Id))
            .Select(x => x.Name).ToList();

        return (p, allergieNames, ncdNames);
    }
}