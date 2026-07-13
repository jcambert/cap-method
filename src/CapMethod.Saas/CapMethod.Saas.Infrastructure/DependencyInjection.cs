using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Infrastructure.Sessions;
using Microsoft.Extensions.DependencyInjection;

namespace CapMethod.Saas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCapMethodSaasInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICapSessionRepository, InMemoryCapSessionRepository>();
        return services;
    }
}
