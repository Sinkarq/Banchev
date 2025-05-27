using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace Persistence.Seeding;

internal class AdminSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.Users.Any(u => u.UserName == "admin"))
        {
            return;
        }

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<ApplicationUser>>();
        
        var adminRole = await roleManager.FindByNameAsync("Admin");
        if (adminRole == null)
        {
            throw new InvalidOperationException("Admin role not found.");
        }

        var adminUser = new ApplicationUser
        {
            UserName = "admin",
        };
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin");
        
        var result = await userManager.CreateAsync(adminUser);
        
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
        
        result = await userManager.AddToRoleAsync(adminUser, adminRole.Name);
    }
}