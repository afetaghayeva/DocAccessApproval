using DocAccessApproval.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DocAccessApproval.Persistence.Context;

public class DocAccessApprovalDbContextDesignFactory : IDesignTimeDbContextFactory<DocAccessApprovalDbContext>
{

    public DocAccessApprovalDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DocAccessApprovalConnectionString");

        var optionsBuilder = new DbContextOptionsBuilder<DocAccessApprovalDbContext>()
                                 .UseSqlite(connectionString);

        return new DocAccessApprovalDbContext(optionsBuilder.Options,new NoOpDomainEventDispatcher());
    }
}
