using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CapMethod.Saas.Infrastructure.Persistence;

public sealed class CapMethodSaasDbContextFactory : IDesignTimeDbContextFactory<CapMethodSaasDbContext>
{
    private const string DefaultConnectionString = "Host=localhost;Port=5432;Database=cap_method_saas;Username=cap_method;Password=cap_method";

    public CapMethodSaasDbContext CreateDbContext(string[] args)
    {
        string? connectionString = Environment.GetEnvironmentVariable("CAP_METHOD_SAAS_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = DefaultConnectionString;
        }

        DbContextOptionsBuilder<CapMethodSaasDbContext> builder = new();
        builder.UseNpgsql(connectionString);

        return new CapMethodSaasDbContext(builder.Options);
    }
}
