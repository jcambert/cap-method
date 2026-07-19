using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.Synthesis;
using Xunit;

namespace CapMethod.Saas.Server.Tests;

public sealed class ConsultantSynthesisApiTests : IClassFixture<ServerTestApplicationFactory>
{
    private static readonly Guid BeneficiaryId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private readonly ServerTestApplicationFactory _factory;

    public ConsultantSynthesisApiTests(ServerTestApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Synthesis_requires_authentication()
    {
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync($"/api/beneficiaries/{BeneficiaryId}/synthesis");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Consultant_can_get_editable_synthesis_draft()
    {
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();

        using HttpResponseMessage response = await client.GetAsync($"/api/beneficiaries/{BeneficiaryId}/synthesis");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        SynthesisResponse? synthesis = await response.Content.ReadFromJsonAsync<SynthesisResponse>();
        Assert.NotNull(synthesis);
        Assert.Equal(BeneficiaryId, synthesis.BeneficiaryId);
        Assert.False(synthesis.IsValidated);
    }

    [Fact]
    public async Task Consultant_can_save_then_validate_synthesis()
    {
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();
        Guid beneficiaryId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        SaveSynthesisRequest draft = new("Premiere synthese editable.", Validate: false);
        SaveSynthesisRequest final = new("Synthese validee par le consultant.", Validate: true);

        using HttpResponseMessage draftResponse = await client.PutAsJsonAsync($"/api/beneficiaries/{beneficiaryId}/synthesis", draft);
        using HttpResponseMessage finalResponse = await client.PutAsJsonAsync($"/api/beneficiaries/{beneficiaryId}/synthesis", final);

        Assert.Equal(HttpStatusCode.OK, draftResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, finalResponse.StatusCode);
        SynthesisResponse? synthesis = await finalResponse.Content.ReadFromJsonAsync<SynthesisResponse>();
        Assert.NotNull(synthesis);
        Assert.True(synthesis.IsValidated);
        Assert.NotNull(synthesis.ValidatedAtUtc);
        Assert.NotNull(synthesis.ValidatedByUserId);
    }

    [Fact]
    public async Task Empty_synthesis_content_is_rejected_by_endpoint()
    {
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();
        Guid beneficiaryId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        SaveSynthesisRequest empty = new("   ", Validate: false);

        using HttpResponseMessage response = await client.PutAsJsonAsync($"/api/beneficiaries/{beneficiaryId}/synthesis", empty);

        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
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