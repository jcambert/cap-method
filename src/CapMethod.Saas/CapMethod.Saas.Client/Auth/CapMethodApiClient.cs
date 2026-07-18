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

    public async Task<AccessTokenResponse> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        ProductionLoginRequest payload = new(email, password);

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/auth/token", payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        AccessTokenResponse? token = await response.Content.ReadFromJsonAsync<AccessTokenResponse>(cancellationToken);

        if (token is null || string.IsNullOrWhiteSpace(token.AccessToken))
        {
            throw new InvalidOperationException("The access token response is empty.");
        }

        await _tokenStore.SaveAccessTokenAsync(token.AccessToken);

        return token;
    }

    public async Task<BeneficiaryAccessTokenResponse> LoginBeneficiaryPortalAsync(
        string email,
        string accessCode,
        CancellationToken cancellationToken)
    {
        BeneficiaryPortalLoginRequest payload = new(email, accessCode);

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/beneficiary/auth/token", payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        BeneficiaryAccessTokenResponse? token = await response.Content.ReadFromJsonAsync<BeneficiaryAccessTokenResponse>(cancellationToken);

        if (token is null || string.IsNullOrWhiteSpace(token.AccessToken))
        {
            throw new InvalidOperationException("The beneficiary portal token response is empty.");
        }

        await _tokenStore.SaveAccessTokenAsync(token.AccessToken);

        return token;
    }

    public async Task<AccessTokenResponse> CreateDevelopmentTokenAsync(CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await _httpClient.PostAsync("api/dev/token", content: null, cancellationToken);
        response.EnsureSuccessStatusCode();

        AccessTokenResponse? token = await response.Content.ReadFromJsonAsync<AccessTokenResponse>(cancellationToken);

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

    public async Task<BeneficiaryPortalContextResponse> GetBeneficiaryPortalContextAsync(CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "api/beneficiary/me");
        await AddAuthorizationHeaderAsync(request);

        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        BeneficiaryPortalContextResponse? context = await response.Content.ReadFromJsonAsync<BeneficiaryPortalContextResponse>(cancellationToken);

        if (context is null)
        {
            throw new InvalidOperationException("The beneficiary portal context response is empty.");
        }

        return context;
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

    public async Task<IReadOnlyCollection<CapSessionSummaryResponse>> ListCapSessionsAsync(CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "api/cap-sessions");
        await AddAuthorizationHeaderAsync(request);

        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        CapSessionSummaryResponse[]? sessions = await response.Content.ReadFromJsonAsync<CapSessionSummaryResponse[]>(cancellationToken);

        return sessions ?? [];
    }

    public async Task<CapSessionResponse> GetCapSessionAsync(Guid capSessionId, CancellationToken cancellationToken)
    {
        if (capSessionId == Guid.Empty)
        {
            throw new ArgumentException("CapSessionId is required.", nameof(capSessionId));
        }

        using HttpRequestMessage request = new(HttpMethod.Get, $"api/cap-sessions/{capSessionId}");
        await AddAuthorizationHeaderAsync(request);

        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        CapSessionResponse? session = await response.Content.ReadFromJsonAsync<CapSessionResponse>(cancellationToken);

        if (session is null)
        {
            throw new InvalidOperationException("The CAP session detail response is empty.");
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
