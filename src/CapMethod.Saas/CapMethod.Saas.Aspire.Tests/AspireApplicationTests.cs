using System.Net;
using Aspire.Hosting.Testing;

namespace CapMethod.Saas.Aspire.Tests;

public sealed class AspireApplicationTests
{
    [Fact]
    public async Task AppHost_starts_postgres_and_exposes_healthy_hosted_application()
    {
        string[] args =
        [
            "Parameters:jwt-signing-key=CAP_METHOD_TEST_SIGNING_KEY_0123456789_ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        ];

        IDistributedApplicationTestingBuilder builder =
            await DistributedApplicationTestingBuilder.CreateAsync<Projects.CapMethod_Saas_AppHost>(args);

        await using DistributedApplication application = await builder.BuildAsync();
        await application.StartAsync();

        await application.ResourceNotifications
            .WaitForResourceHealthyAsync("capmethod-saas")
            .WaitAsync(TimeSpan.FromMinutes(3));

        HttpClient client = application.CreateHttpClient("capmethod-saas");

        using HttpResponseMessage healthResponse = await client.GetAsync("/health");
        using HttpResponseMessage infoResponse = await client.GetAsync("/api/info");
        using HttpResponseMessage clientResponse = await client.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, healthResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, infoResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, clientResponse.StatusCode);
    }
}