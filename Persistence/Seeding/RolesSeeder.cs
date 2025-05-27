using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace Persistence.Seeding;

internal class RolesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.Roles.Any())
        {
            return;
        }
        
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await roleManager.CreateAsync(new ApplicationRole("Admin"));
        await roleManager.CreateAsync(new ApplicationRole("Teacher"));
        await roleManager.CreateAsync(new ApplicationRole("Student"));
    }
}