using CapMethod.Saas.Server.ActionPlans;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.ActionPlans;
using CapMethod.Saas.Shared.Synthesis;

namespace CapMethod.Saas.Server.Synthesis;

public static class EditableSynthesisEndpoints
{
    private static readonly ActionPlanStore ActionPlans = new();

    public static IEndpointRouteBuilder MapEditableSynthesisEndpoints(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("/api/beneficiaries/{beneficiaryId:guid}/synthesis");
        group.RequireAuthorization();

        group.MapGet("", (
            Guid beneficiaryId,
            ICapUserContextAccessor userContextAccessor,
            EditableSynthesisStore store) =>
        {
            CapUserContext userContext = userContextAccessor.GetRequiredContext();
            SynthesisResponse response = store.GetOrCreate(userContext.TenantId, beneficiaryId);
            return Results.Ok(response);
        });

        group.MapPut("", (
            Guid beneficiaryId,
            SaveSynthesisRequest request,
            ICapUserContextAccessor userContextAccessor,
            EditableSynthesisStore store) =>
        {
            CapUserContext userContext = userContextAccessor.GetRequiredContext();
            SynthesisResponse response = store.Save(
                userContext.TenantId,
                beneficiaryId,
                userContext.UserId,
                request);
            return Results.Ok(response);
        });

        MapActionPlanEndpoints(endpoints);

        return endpoints;
    }

    private static void MapActionPlanEndpoints(IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("/api/beneficiaries/{beneficiaryId:guid}/action-plan");
        group.RequireAuthorization();

        group.MapGet("", (
            Guid beneficiaryId,
            ICapUserContextAccessor userContextAccessor) =>
        {
            CapUserContext userContext = userContextAccessor.GetRequiredContext();
            ActionPlanResponse response = ActionPlans.GetOrCreate(userContext.TenantId, beneficiaryId);
            return Results.Ok(response);
        });

        group.MapPut("", (
            Guid beneficiaryId,
            SaveActionPlanRequest request,
            ICapUserContextAccessor userContextAccessor) =>
        {
            CapUserContext userContext = userContextAccessor.GetRequiredContext();
            ActionPlanResponse response = ActionPlans.Save(
                userContext.TenantId,
                beneficiaryId,
                userContext.UserId,
                request);
            return Results.Ok(response);
        });

        group.MapPost("items/{itemId:guid}/complete", (
            Guid beneficiaryId,
            Guid itemId,
            ICapUserContextAccessor userContextAccessor) =>
        {
            CapUserContext userContext = userContextAccessor.GetRequiredContext();
            ActionPlanResponse response = ActionPlans.CompleteItem(userContext.TenantId, beneficiaryId, itemId);
            return Results.Ok(response);
        });
    }
}
