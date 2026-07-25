using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.ActionPlans;
using CapMethod.Saas.Shared.Synthesis;
using Xunit;

namespace CapMethod.Saas.Server.Tests;

public sealed class ConsultantDeliverableExportApiTests : IClassFixture<ServerTestApplicationFactory>
{
    private readonly ServerTestApplicationFactory _factory;

    public ConsultantDeliverableExportApiTests(ServerTestApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Deliverable_export_requires_authentication()
    {
        Guid beneficiaryId = Guid.Parse("77777777-7777-7777-7777-777777777771");
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync($"/api/beneficiaries/{beneficiaryId}/deliverables/bilan.md");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Draft_sections_cannot_be_exported()
    {
        Guid beneficiaryId = Guid.Parse("77777777-7777-7777-7777-777777777772");
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();

        using HttpResponseMessage response = await client.GetAsync($"/api/beneficiaries/{beneficiaryId}/deliverables/bilan.md");

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Validated_synthesis_and_action_plan_are_exported_as_markdown()
    {
        Guid beneficiaryId = Guid.Parse("77777777-7777-7777-7777-777777777773");
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();

        SaveSynthesisRequest synthesisRequest = new(
            "# Synthèse du bilan de compétences\n\n## Conclusion\nProjet professionnel validé.",
            Validate: true);
        using HttpResponseMessage synthesisResponse = await client.PutAsJsonAsync(
            $"/api/beneficiaries/{beneficiaryId}/synthesis",
            synthesisRequest);
        synthesisResponse.EnsureSuccessStatusCode();

        SaveActionPlanRequest actionPlanRequest = new(
            [
                new SaveActionPlanItemRequest(
                    null,
                    "Mettre à jour le CV",
                    "Valoriser les compétences confirmées pendant le bilan.",
                    "Positionnement",
                    "Haute",
                    DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(15)))
            ],
            Validate: true);
        using HttpResponseMessage actionPlanResponse = await client.PutAsJsonAsync(
            $"/api/beneficiaries/{beneficiaryId}/action-plan",
            actionPlanRequest);
        actionPlanResponse.EnsureSuccessStatusCode();

        using HttpResponseMessage exportResponse = await client.GetAsync(
            $"/api/beneficiaries/{beneficiaryId}/deliverables/bilan.md");

        Assert.Equal(HttpStatusCode.OK, exportResponse.StatusCode);
        Assert.Equal("text/markdown", exportResponse.Content.Headers.ContentType?.MediaType);
        Assert.NotNull(exportResponse.Content.Headers.ContentDisposition);
        string content = await exportResponse.Content.ReadAsStringAsync();
        Assert.Contains("# Livrable de bilan de compétences", content, StringComparison.Ordinal);
        Assert.Contains("Projet professionnel validé.", content, StringComparison.Ordinal);
        Assert.Contains("# Plan d'action", content, StringComparison.Ordinal);
        Assert.Contains("Mettre à jour le CV", content, StringComparison.Ordinal);
    }

    private async Task<HttpClient> CreateAuthenticatedConsultantClientAsync()
    {
        HttpClient client = _factory.CreateClient();
        using HttpResponseMessage tokenResponse = await client.PostAsync("/api/dev/token", null);
        tokenResponse.EnsureSuccessStatusCode();
        DevelopmentTokenResponse? token = await tokenResponse.Content.ReadFromJsonAsync<DevelopmentTokenResponse>();
        Assert.NotNull(token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);
        return client;
    }
}