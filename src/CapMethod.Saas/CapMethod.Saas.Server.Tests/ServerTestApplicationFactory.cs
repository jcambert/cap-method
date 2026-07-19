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
                ["Authentication:Jwt:SigningKey"] = "CAP_METHOD_SAAS_DEV_SIGNING_KEY_32_CHARS_MIN",
                ["Security:EnableDevelopmentUserContext"] = "true",
                ["Security:DevelopmentTenantId"] = "11111111-1111-1111-1111-111111111111",
                ["Security:DevelopmentUserId"] = "22222222-2222-2222-2222-222222222222"
            };

            configuration.AddInMemoryCollection(values);
        });
    }
}
