using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Persistence.Context;
using DocAccessApproval.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocAccessApproval.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DocAccessApprovalDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DocAccessApprovalConnectionString"));
        });

        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}
