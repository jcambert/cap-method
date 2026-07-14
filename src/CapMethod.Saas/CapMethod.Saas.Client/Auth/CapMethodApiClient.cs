using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Shared.Beneficiaries;
using CapMethod.Saas.Shared.CapSessions;

namespace CapMethod.Saas.Client.Auth;

public sealed class CapMethodApiClient
{
    private readonly HttpClient _httpClient;
    private readonly BrowserTokenStore _tokenStore;

    public CapMethodApiClient(HttpClient httpClient, BrowserTokenStore tokenStore)
    {
        _httpClient = httpClient;
        _tokenStore = tokenStore;
    }

    public async Task<DevelopmentTokenResponse> CreateDevelopmentTokenAsync(CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await _httpClient.PostAsync("api/dev/token", content: null, cancellationToken);
        response.EnsureSuccessStatusCode();

        DevelopmentTokenResponse? token = await response.Content.ReadFromJsonAsync<DevelopmentTokenResponse>(cancellationToken);

        if (token is null || string.IsNullOrWhiteSpace(token.AccessToken))
        {
            throw new InvalidOperationException("The development token response is empty.");
        }

        await _tokenStore.SaveAccessTokenAsync(token.AccessToken);

        return token;
    }

    public async Task<CurrentUserResponse> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "api/me");
        await AddAuthorizationHeaderAsync(request);

        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        CurrentUserResponse? user = await response.Content.ReadFromJsonAsync<CurrentUserResponse>(cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException("The current user response is empty.");
        }

        return user;
    }

    public async Task<BeneficiaryResponse> CreateBeneficiaryAsync(
        string firstName,
        string lastName,
        string? email,
        CancellationToken cancellationToken)
    {
        CreateBeneficiaryRequest payload = new(
            TenantId: Guid.Empty,
            firstName,
            lastName,
            email);

        using HttpRequestMessage request = new(HttpMethod.Post, "api/beneficiaries")
        {
            Content = JsonContent.Create(payload)
        };

        await AddAuthorizationHeaderAsync(request);

        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        BeneficiaryResponse? beneficiary = await response.Content.ReadFromJsonAsync<BeneficiaryResponse>(cancellationToken);

        if (beneficiary is null)
        {
            throw new InvalidOperationException("The created beneficiary response is empty.");
        }

        return beneficiary;
    }

    public async Task<CapSessionResponse> CreateCapSessionAsync(
        Guid beneficiaryId,
        bool enableAi,
        CancellationToken cancellationToken)
    {
        if (beneficiaryId == Guid.Empty)
        {
            throw new ArgumentException("BeneficiaryId is required.", nameof(beneficiaryId));
        }

        CreateCapSessionRequest payload = new(
            TenantId: Guid.Empty,
            BeneficiaryId: beneficiaryId,
            ConsultantId: Guid.Empty,
            EnableAi: enableAi);

        using HttpRequestMessage request = new(HttpMethod.Post, "api/cap-sessions")
        {
            Content = JsonContent.Create(payload)
        };

        await AddAuthorizationHeaderAsync(request);

        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        CapSessionResponse? session = await response.Content.ReadFromJsonAsync<CapSessionResponse>(cancellationToken);

        if (session is null)
        {
            throw new InvalidOperationException("The created CAP session response is empty.");
        }

        return session;
    }

    public Task LogoutAsync()
    {
        return _tokenStore.ClearAsync();
    }

    private async Task AddAuthorizationHeaderAsync(HttpRequestMessage request)
    {
        string? token = await _tokenStore.GetAccessTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException("No access token is currently stored.");
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
