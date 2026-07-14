using CapMethod.Saas.Application.Beneficiaries;
using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Infrastructure.Beneficiaries;
using CapMethod.Saas.Infrastructure.Persistence;
using CapMethod.Saas.Infrastructure.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CapMethod.Saas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCapMethodSaasInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICapSessionRepository, InMemoryCapSessionRepository>();
        services.AddSingleton<IBeneficiaryRepository, InMemoryBeneficiaryRepository>();
        return services;
    }

    public static IServiceCollection AddCapMethodSaasPostgreSqlInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<CapMethodSaasDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ICapSessionRepository, EfCapSessionRepository>();
        services.AddScoped<IBeneficiaryRepository, EfBeneficiaryRepository>();

        return services;
    }
}
