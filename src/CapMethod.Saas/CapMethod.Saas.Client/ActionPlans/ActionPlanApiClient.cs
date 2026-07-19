using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Client.Auth;
using CapMethod.Saas.Shared.ActionPlans;

namespace CapMethod.Saas.Client.ActionPlans;

public sealed class ActionPlanApiClient
{
    private readonly HttpClient _httpClient;
    private readonly BrowserTokenStore _tokenStore;

    public ActionPlanApiClient(HttpClient httpClient, BrowserTokenStore tokenStore)
    {
        _httpClient = httpClient;
        _tokenStore = tokenStore;
    }

    public async Task<ActionPlanResponse> GetAsync(Guid beneficiaryId, CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, $"api/beneficiaries/{beneficiaryId}/action-plan");
        await AddAuthorizationHeaderAsync(request);
        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        ActionPlanResponse? actionPlan = await response.Content.ReadFromJsonAsync<ActionPlanResponse>(cancellationToken);
        return actionPlan ?? throw new InvalidOperationException("The action plan response is empty.");
    }

    public async Task<ActionPlanResponse> SaveAsync(
        Guid beneficiaryId,
        IReadOnlyCollection<SaveActionPlanItemRequest> items,
        bool validate,
        CancellationToken cancellationToken)
    {
        SaveActionPlanRequest payload = new(items, validate);
        using HttpRequestMessage request = new(HttpMethod.Put, $"api/beneficiaries/{beneficiaryId}/action-plan")
        {
            Content = JsonContent.Create(payload)
        };
        await AddAuthorizationHeaderAsync(request);
        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        ActionPlanResponse? actionPlan = await response.Content.ReadFromJsonAsync<ActionPlanResponse>(cancellationToken);
        return actionPlan ?? throw new InvalidOperationException("The action plan save response is empty.");
    }

    public async Task<ActionPlanResponse> CompleteItemAsync(
        Guid beneficiaryId,
        Guid itemId,
        CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new(HttpMethod.Post, $"api/beneficiaries/{beneficiaryId}/action-plan/items/{itemId}/complete");
        await AddAuthorizationHeaderAsync(request);
        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        ActionPlanResponse? actionPlan = await response.Content.ReadFromJsonAsync<ActionPlanResponse>(cancellationToken);
        return actionPlan ?? throw new InvalidOperationException("The action plan completion response is empty.");
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
