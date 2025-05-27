using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchoolManagement.Data;

namespace Persistence.Seeding;

public sealed class ApplicationDbContextSeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        if (serviceProvider == null)
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }

        var logger = serviceProvider.GetService<ILogger<ApplicationDbContextSeeder>>();

        var seeders = new List<ISeeder>
        {
        };

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
        }
    }
}