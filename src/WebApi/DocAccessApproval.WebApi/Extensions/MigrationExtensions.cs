using DocAccessApproval.Application.Common;
using DocAccessApproval.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DocAccessApproval.WebApi.Extensions
{
    public static class MigrationExtensions
    {
        public static void MigrateAndSeed(this IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DocAccessApprovalDbContext>()
                .UseSqlite(configuration.GetConnectionString("DocAccessApprovalConnectionString"));

            var dbContext = new DocAccessApprovalDbContext(optionsBuilder.Options, new NoOpDomainEventDispatcher());
            dbContext.Database.Migrate();
            DocAccessApprovalDbContextSeed seeder = new DocAccessApprovalDbContextSeed();
            seeder.SeedAsync(dbContext).Wait();
        }
    }
}
