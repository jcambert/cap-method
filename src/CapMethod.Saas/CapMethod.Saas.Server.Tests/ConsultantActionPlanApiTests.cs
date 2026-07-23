using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.ActionPlans;
using Xunit;

namespace CapMethod.Saas.Server.Tests;

public sealed class ConsultantActionPlanApiTests : IClassFixture<ServerTestApplicationFactory>
{
    private readonly ServerTestApplicationFactory _factory;

    public ConsultantActionPlanApiTests(ServerTestApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Action_plan_requires_authentication()
    {
        Guid beneficiaryId = Guid.Parse("55555555-5555-5555-5555-555555555551");
        HttpClient client = _factory.CreateClient();

        using HttpResponseMessage response = await client.GetAsync($"/api/beneficiaries/{beneficiaryId}/action-plan");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Consultant_can_get_empty_action_plan_draft()
    {
        Guid beneficiaryId = Guid.Parse("55555555-5555-5555-5555-555555555552");
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();

        using HttpResponseMessage response = await client.GetAsync($"/api/beneficiaries/{beneficiaryId}/action-plan");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        ActionPlanResponse? actionPlan = await response.Content.ReadFromJsonAsync<ActionPlanResponse>();
        Assert.NotNull(actionPlan);
        Assert.Equal(beneficiaryId, actionPlan.BeneficiaryId);
        Assert.False(actionPlan.IsValidated);
        Assert.Empty(actionPlan.Items);
    }

    [Fact]
    public async Task Consultant_can_save_validate_and_complete_action_plan_item()
    {
        Guid beneficiaryId = Guid.Parse("55555555-5555-5555-5555-555555555553");
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();
        SaveActionPlanRequest draft = new(
            [
                new SaveActionPlanItemRequest(
                    null,
                    "Mettre a jour le CV",
                    "Reformuler les experiences avec les acquis du bilan.",
                    "Positionnement",
                    "Haute",
                    DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(14)))
            ],
            Validate: false);

        using HttpResponseMessage draftResponse = await client.PutAsJsonAsync($"/api/beneficiaries/{beneficiaryId}/action-plan", draft);

        Assert.Equal(HttpStatusCode.OK, draftResponse.StatusCode);
        ActionPlanResponse? draftPlan = await draftResponse.Content.ReadFromJsonAsync<ActionPlanResponse>();
        Assert.NotNull(draftPlan);
        ActionPlanItemResponse item = Assert.Single(draftPlan.Items);
        Assert.Equal("Open", item.Status);

        SaveActionPlanRequest final = new(
            draftPlan.Items
                .Select(planItem => new SaveActionPlanItemRequest(
                    planItem.ItemId,
                    planItem.Title,
                    planItem.Description,
                    planItem.Category,
                    planItem.Priority,
                    planItem.DueDate))
                .ToArray(),
            Validate: true);

        using HttpResponseMessage finalResponse = await client.PutAsJsonAsync($"/api/beneficiaries/{beneficiaryId}/action-plan", final);

        Assert.Equal(HttpStatusCode.OK, finalResponse.StatusCode);
        ActionPlanResponse? validatedPlan = await finalResponse.Content.ReadFromJsonAsync<ActionPlanResponse>();
        Assert.NotNull(validatedPlan);
        Assert.True(validatedPlan.IsValidated);
        Assert.NotNull(validatedPlan.ValidatedAtUtc);
        Assert.NotNull(validatedPlan.ValidatedByUserId);

        using HttpResponseMessage completeResponse = await client.PostAsync(
            $"/api/beneficiaries/{beneficiaryId}/action-plan/items/{item.ItemId}/complete",
            null);

        Assert.Equal(HttpStatusCode.OK, completeResponse.StatusCode);
        ActionPlanResponse? completedPlan = await completeResponse.Content.ReadFromJsonAsync<ActionPlanResponse>();
        Assert.NotNull(completedPlan);
        Assert.Equal("Completed", Assert.Single(completedPlan.Items).Status);
    }

    [Fact]
    public async Task Empty_action_plan_cannot_be_validated()
    {
        HttpClient client = await CreateAuthenticatedConsultantClientAsync();
        Guid beneficiaryId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        SaveActionPlanRequest request = new([], Validate: true);

        using HttpResponseMessage response = await client.PutAsJsonAsync($"/api/beneficiaries/{beneficiaryId}/action-plan", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
