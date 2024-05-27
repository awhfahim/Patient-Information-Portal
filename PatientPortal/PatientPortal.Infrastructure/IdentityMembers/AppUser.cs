using Microsoft.AspNetCore.Identity;

namespace PatientPortal.Infrastructure.IdentityMembers;

public class AppUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}