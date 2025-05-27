using Microsoft.AspNetCore.Identity;

namespace Domain;

public sealed class ApplicationRole : IdentityRole
{
    public ApplicationRole()
        : this(null)
    {
    }

    public ApplicationRole(string name)
        : base(name)
    {
        this.Id = Guid.NewGuid().ToString();
    }
}