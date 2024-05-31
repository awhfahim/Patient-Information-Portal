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

    public async Task<(PatientData, List<string> AllergyNames, List<string> NcdNames)?> MyGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Where(x => x.Id == id)
            .Include(p => p.AllergiesDetails)
            .Include(p => p.NcdDetails)
            .Select(p => new
            {
                p.Id,p.Name, p.Age, p.Gender, p.Address,
                p.BloodGroup,p.PhoneNumber,p.DiseaseInfoId,
                p.EpilepsyStatus,
                AllergyIds = p.AllergiesDetails.Select(a => a.AllergyId).ToList(),
                NcdIds = p.NcdDetails.Select(n => n.NcdId).ToList()
            })
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);

        if (patient is null) 
            return null;


        var diseaseName = await context.DiseaseInfos.Where(x => x.Id == patient.DiseaseInfoId)
            .Select(x => x.Name).FirstOrDefaultAsync(cancellationToken);
        
        var allergieNames = await _context.Allergies.Where(x => patient.AllergyIds.Contains(x.Id))
            .Select(x => x.Name).ToListAsync(cancellationToken);
        
        var ncdNames = await _context.Ncds.Where(x => patient.NcdIds.Contains(x.Id))
            .Select(x => x.Name).ToListAsync(cancellationToken);

        PatientData patientData = new()
        {
            Address = patient.Address, Gender = patient.Gender, BloodGroup = patient.BloodGroup,
            PhoneNumber = patient.PhoneNumber, DiseaseName = diseaseName, Name = patient.Name, 
            Id = patient.Id, Age = patient.Age, EpilepsyStatus = (uint)patient.EpilepsyStatus
        };

        return (patientData, allergieNames, ncdNames);
    }

    public async Task<Patient> FetchByIdAsync(Guid id, CancellationToken cancellationToken)
        => (await _context.Patients.Where(x => x.Id == id)
            .Include(x => x.AllergiesDetails)
            .Include(x => x.NcdDetails)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken))!;
}