using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Client.Auth;
using CapMethod.Saas.Shared.Synthesis;

namespace CapMethod.Saas.Client.Synthesis;

public sealed class SynthesisApiClient
{
    private readonly HttpClient _httpClient;
    private readonly BrowserTokenStore _tokenStore;

    public SynthesisApiClient(HttpClient httpClient, BrowserTokenStore tokenStore)
    {
        _httpClient = httpClient;
        _tokenStore = tokenStore;
    }

    public async Task<SynthesisResponse> GetAsync(Guid beneficiaryId, CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, $"api/beneficiaries/{beneficiaryId}/synthesis");
        await AddAuthorizationHeaderAsync(request);
        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        SynthesisResponse? synthesis = await response.Content.ReadFromJsonAsync<SynthesisResponse>(cancellationToken);
        return synthesis ?? throw new InvalidOperationException("The synthesis response is empty.");
    }

    public async Task<SynthesisResponse> SaveAsync(
        Guid beneficiaryId,
        string content,
        bool validate,
        CancellationToken cancellationToken)
    {
        SaveSynthesisRequest payload = new(content, validate);
        using HttpRequestMessage request = new(HttpMethod.Put, $"api/beneficiaries/{beneficiaryId}/synthesis")
        {
            Content = JsonContent.Create(payload)
        };
        await AddAuthorizationHeaderAsync(request);
        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        SynthesisResponse? synthesis = await response.Content.ReadFromJsonAsync<SynthesisResponse>(cancellationToken);
        return synthesis ?? throw new InvalidOperationException("The synthesis save response is empty.");
    }

    private async Task AddAuthorizationHeaderAsync(HttpRequestMessage request)
    {
        string? token = await _tokenStore.GetAccessTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException("No consultant access token is currently stored.");
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
