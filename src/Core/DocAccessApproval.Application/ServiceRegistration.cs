using DocAccessApproval.Application.Common;
using DocAccessApproval.Application.Interfaces;
using DocAccessApproval.Application.Security.JWT;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DocAccessApproval.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(assembly);
        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
