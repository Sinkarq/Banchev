using Persistence.Data;

namespace Persistence.Seeding;

public interface ISeeder
{
    Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
}