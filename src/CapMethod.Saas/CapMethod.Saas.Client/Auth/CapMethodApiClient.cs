using System.Net.Http.Headers;
using System.Net.Http.Json;

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
