using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Client.Auth;
using CapMethod.Saas.Shared.Questionnaires;

namespace CapMethod.Saas.Client.Questionnaires;

public sealed class BeneficiaryQuestionnaireApiClient
{
    private readonly HttpClient _httpClient;
    private readonly BrowserTokenStore _tokenStore;

    public BeneficiaryQuestionnaireApiClient(HttpClient httpClient, BrowserTokenStore tokenStore)
    {
        _httpClient = httpClient;
        _tokenStore = tokenStore;
    }

    public async Task<IReadOnlyCollection<QuestionnaireDefinitionResponse>> ListAsync(CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "api/beneficiary/questionnaires");
        await AddAuthorizationHeaderAsync(request);
        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        QuestionnaireDefinitionResponse[]? questionnaires = await response.Content.ReadFromJsonAsync<QuestionnaireDefinitionResponse[]>(cancellationToken);
        return questionnaires ?? [];
    }

    public async Task<QuestionnaireProgressResponse> GetProgressAsync(
        string questionnaireId,
        CancellationToken cancellationToken)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, $"api/beneficiary/questionnaires/{questionnaireId}/progress");
        await AddAuthorizationHeaderAsync(request);
        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        QuestionnaireProgressResponse? progress = await response.Content.ReadFromJsonAsync<QuestionnaireProgressResponse>(cancellationToken);
        return progress ?? throw new InvalidOperationException("The questionnaire progress response is empty.");
    }

    public async Task<QuestionnaireProgressResponse> SaveAsync(
        string questionnaireId,
        IReadOnlyCollection<QuestionnaireAnswerRequest> answers,
        bool submit,
        CancellationToken cancellationToken)
    {
        SaveQuestionnaireAnswersRequest payload = new(answers, submit);
        using HttpRequestMessage request = new(HttpMethod.Put, $"api/beneficiary/questionnaires/{questionnaireId}/answers")
        {
            Content = JsonContent.Create(payload)
        };
        await AddAuthorizationHeaderAsync(request);
        using HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        QuestionnaireProgressResponse? progress = await response.Content.ReadFromJsonAsync<QuestionnaireProgressResponse>(cancellationToken);
        return progress ?? throw new InvalidOperationException("The questionnaire save response is empty.");
    }

    private async Task AddAuthorizationHeaderAsync(HttpRequestMessage request)
    {
        string? token = await _tokenStore.GetAccessTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException("No beneficiary access token is currently stored.");
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
