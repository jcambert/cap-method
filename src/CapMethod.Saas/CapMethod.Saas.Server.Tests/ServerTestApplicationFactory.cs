using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace CapMethod.Saas.Server.Tests;

public sealed class ServerTestApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureAppConfiguration((_, configuration) =>
        {
            Dictionary<string, string?> values = new()
            {
                ["Persistence:Provider"] = "InMemory",
                ["Authentication:Jwt:Issuer"] = "cap-method-saas-dev",
                ["Authentication:Jwt:Audience"] = "cap-method-saas-client-dev",
                ["Authentication:Jwt:SigningKey"] = "CAP_METHOD_TEST_SIGNING_KEY_0123456789_ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                ["Security:EnableDevelopmentUserContext"] = "true",
                ["Security:DevelopmentTenantId"] = "11111111-1111-1111-1111-111111111111",
                ["Security:DevelopmentUserId"] = "22222222-2222-2222-2222-222222222222",
                ["Authentication:ProductionUser:Email"] = "admin@cap-method.local",
                ["Authentication:ProductionUser:PasswordSha256"] = "9a4aabf0e5cf71cae2cea646613ce7e2a5919fa758e56819704be25a3a2c1f0b",
                ["Authentication:ProductionUser:TenantId"] = "11111111-1111-1111-1111-111111111111",
                ["Authentication:ProductionUser:UserId"] = "22222222-2222-2222-2222-222222222222",
                ["Authentication:BeneficiaryPortal:Email"] = "beneficiaire@cap-method.local",
                ["Authentication:BeneficiaryPortal:AccessCodeSha256"] = "22c1648dc63d9e16b8ad78be443db17a1d068ac8d86c99cf10d2662c770883af",
                ["Authentication:BeneficiaryPortal:TenantId"] = "11111111-1111-1111-1111-111111111111",
                ["Authentication:BeneficiaryPortal:BeneficiaryId"] = "33333333-3333-3333-3333-333333333333"
            };

            configuration.AddInMemoryCollection(values);
        });
    }
}