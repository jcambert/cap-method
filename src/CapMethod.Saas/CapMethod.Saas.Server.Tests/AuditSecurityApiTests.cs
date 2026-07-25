using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Server.Audit;
using CapMethod.Saas.Server.Security;
using Xunit;

namespace CapMethod.Saas.Server.Tests;

public sealed class AuditSecurityApiTests : IClassFixture<ServerTestApplicationFactory>
{
    private readonly ServerTestApplicationFactory _factory;

    public AuditSecurityApiTests(ServerTestApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Public_response_contains_defensive_security_headers()
    {
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync("/api/info");

        response.EnsureSuccessStatusCode();
        Assert.Equal("nosniff", Assert.Single(response.Headers.GetValues("X-Content-Type-Options")));
        Assert.Equal("DENY", Assert.Single(response.Headers.GetValues("X-Frame-Options")));
        Assert.Contains("frame-ancestors 'none'", Assert.Single(response.Headers.GetValues("Content-Security-Policy")));
    }

    [Fact]
    public async Task Audit_history_requires_authentication()
    {
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync("/api/audit/events");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Successful_mutation_is_recorded_without_request_payload()
    {
        HttpClient client = _factory.CreateClient();
        DevelopmentTokenResponse token = await CreateDevelopmentTokenAsync(client);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

        using HttpResponseMessage mutation = await client.PostAsJsonAsync("/api/beneficiaries", new
        {
            FirstName = "Audit",
            LastName = "Test",
            Email = "audit.test@example.invalid"
        });
        mutation.EnsureSuccessStatusCode();

        AuditEvent[]? events = await client.GetFromJsonAsync<AuditEvent[]>("/api/audit/events?limit=20");

        Assert.NotNull(events);
        AuditEvent auditEvent = Assert.Single(events, item =>
            item.Action == "POST" && item.Resource == "/api/beneficiaries");
        Assert.Equal((int)HttpStatusCode.Created, auditEvent.StatusCode);
        Assert.False(string.IsNullOrWhiteSpace(auditEvent.CorrelationId));
        Assert.DoesNotContain("audit.test", auditEvent.Resource, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Audit_store_is_isolated_by_tenant()
    {
        AuditEventStore store = new();
        Guid tenantId = Guid.NewGuid();
        store.Append(new AuditEvent(Guid.NewGuid(), tenantId, Guid.NewGuid(), "PUT", "/api/resource", 200, "corr", DateTimeOffset.UtcNow));

        Assert.Single(store.List(tenantId));
        Assert.Empty(store.List(Guid.NewGuid()));
    }

    private static async Task<DevelopmentTokenResponse> CreateDevelopmentTokenAsync(HttpClient client)
    {
        using HttpResponseMessage response = await client.PostAsync("/api/dev/token", null);
        response.EnsureSuccessStatusCode();
        DevelopmentTokenResponse? token = await response.Content.ReadFromJsonAsync<DevelopmentTokenResponse>();
        Assert.NotNull(token);
        return token;
    }
}