using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Server.Security;
using Xunit;

namespace CapMethod.Saas.Server.Tests;

public sealed class AuthApiTests : IClassFixture<ServerTestApplicationFactory>
{
    private readonly ServerTestApplicationFactory _factory;

    public AuthApiTests(ServerTestApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Api_info_is_public()
    {
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync("/api/info");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Consultant_context_requires_authentication()
    {
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync("/api/me");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Development_token_allows_consultant_context_access()
    {
        HttpClient client = _factory.CreateClient();
        DevelopmentTokenResponse token = await CreateDevelopmentTokenAsync(client);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

        using HttpResponseMessage response = await client.GetAsync("/api/me");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Malformed_bearer_token_is_rejected()
    {
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "not-a-valid-jwt");

        using HttpResponseMessage response = await client.GetAsync("/api/me");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Beneficiary_token_cannot_open_consultant_context()
    {
        HttpClient client = _factory.CreateClient();
        BeneficiaryAccessTokenResponse token = await CreateBeneficiaryTokenAsync(client);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

        using HttpResponseMessage response = await client.GetAsync("/api/me");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Consultant_token_cannot_open_beneficiary_context()
    {
        HttpClient client = _factory.CreateClient();
        DevelopmentTokenResponse token = await CreateDevelopmentTokenAsync(client);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

        using HttpResponseMessage response = await client.GetAsync("/api/beneficiary/me");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private static async Task<DevelopmentTokenResponse> CreateDevelopmentTokenAsync(HttpClient client)
    {
        using HttpResponseMessage response = await client.PostAsync("/api/dev/token", null);
        response.EnsureSuccessStatusCode();
        DevelopmentTokenResponse? token = await response.Content.ReadFromJsonAsync<DevelopmentTokenResponse>();
        Assert.NotNull(token);
        return token;
    }

    private static async Task<BeneficiaryAccessTokenResponse> CreateBeneficiaryTokenAsync(HttpClient client)
    {
        using HttpResponseMessage response = await client.PostAsJsonAsync("/api/beneficiary/auth/token", new
        {
            Email = "beneficiaire@cap-method.local",
            AccessCode = "beneficiary"
        });
        response.EnsureSuccessStatusCode();
        BeneficiaryAccessTokenResponse? token = await response.Content.ReadFromJsonAsync<BeneficiaryAccessTokenResponse>();
        Assert.NotNull(token);
        return token;
    }
}