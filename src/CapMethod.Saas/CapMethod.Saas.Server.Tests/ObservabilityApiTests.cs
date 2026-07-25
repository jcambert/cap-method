using System.Net;
using System.Net.Http.Json;
using CapMethod.Saas.Server.Observability;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CapMethod.Saas.Server.Tests;

public sealed class ObservabilityApiTests : IClassFixture<ServerTestApplicationFactory>
{
    private readonly ServerTestApplicationFactory _factory;

    public ObservabilityApiTests(ServerTestApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Response_contains_generated_correlation_identifier()
    {
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync("/api/info");

        response.EnsureSuccessStatusCode();
        Assert.True(response.Headers.TryGetValues(CorrelationIdMiddleware.HeaderName, out IEnumerable<string>? values));
        Assert.False(string.IsNullOrWhiteSpace(Assert.Single(values)));
    }

    [Fact]
    public async Task Supplied_correlation_identifier_is_preserved()
    {
        HttpClient client = _factory.CreateClient();
        using HttpRequestMessage request = new(HttpMethod.Get, "/api/info");
        request.Headers.Add(CorrelationIdMiddleware.HeaderName, "test-correlation-42");

        using HttpResponseMessage response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        Assert.Equal("test-correlation-42", Assert.Single(response.Headers.GetValues(CorrelationIdMiddleware.HeaderName)));
    }

    [Fact]
    public async Task Unhandled_exception_returns_safe_problem_details()
    {
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync("/api/dev/diagnostics/failure");

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        ProblemDetails? problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal(500, problem.Status);
        Assert.Equal("Une erreur inattendue est survenue.", problem.Title);
        Assert.True(problem.Extensions.ContainsKey("correlationId"));
        Assert.DoesNotContain("Diagnostic failure", problem.Detail, StringComparison.OrdinalIgnoreCase);
    }
}