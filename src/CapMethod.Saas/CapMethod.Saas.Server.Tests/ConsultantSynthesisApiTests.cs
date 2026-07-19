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
        SaveSynthesisRequest draft = new("Premiere synthese editable.", Validate: false);
        SaveSynthesisRequest final = new("Synthese validee par le consultant.", Validate: true);

        using HttpResponseMessage draftResponse = await client.PutAsJsonAsync($"/api/beneficiaries/{BeneficiaryId}/synthesis", draft);
        using HttpResponseMessage finalResponse = await client.PutAsJsonAsync($"/api/beneficiaries/{BeneficiaryId}/synthesis", final);

        Assert.Equal(HttpStatusCode.OK, draftResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, finalResponse.StatusCode);
        SynthesisResponse? synthesis = await finalResponse.Content.ReadFromJsonAsync<SynthesisResponse>();
        Assert.NotNull(synthesis);
        Assert.True(synthesis.IsValidated);
        Assert.NotNull(synthesis.ValidatedAtUtc);
        Assert.NotNull(synthesis.ValidatedByUserId);
    }

    [Fact]
    public async Task Validated_synthesis_cannot_be_edited_again()
    {
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();
        Guid beneficiaryId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        SaveSynthesisRequest final = new("Validation definitive.", Validate: true);
        SaveSynthesisRequest update = new("Modification interdite.", Validate: false);

        using HttpResponseMessage finalResponse = await client.PutAsJsonAsync($"/api/beneficiaries/{beneficiaryId}/synthesis", final);
        using HttpResponseMessage updateResponse = await client.PutAsJsonAsync($"/api/beneficiaries/{beneficiaryId}/synthesis", update);

        Assert.Equal(HttpStatusCode.OK, finalResponse.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);
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