using Microsoft.AspNetCore.Identity;

namespace Domain;

public sealed class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Roles = new HashSet<IdentityUserRole<string>>();
        this.Claims = new HashSet<IdentityUserClaim<string>>();
        this.Logins = new HashSet<IdentityUserLogin<string>>();
        this.NormalizedUserName = this.UserName?.ToUpperInvariant();
        this.Email = this.NormalizedUserName;
    }
    
    public ICollection<IdentityUserRole<string>> Roles { get; set; }

    public ICollection<IdentityUserClaim<string>> Claims { get; set; }

    public ICollection<IdentityUserLogin<string>> Logins { get; set; }
    
    public string? StudentId { get; set; } = null!;
    public Student? Student { get; set; }
}