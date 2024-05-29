namespace PatientPortal.Web.Models.Patients;

public class PatientModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}