using CapMethod.Saas.Server.ActionPlans;
using CapMethod.Saas.Server.Exports;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.ActionPlans;
using CapMethod.Saas.Shared.Synthesis;

namespace CapMethod.Saas.Server.Synthesis;

public static class EditableSynthesisEndpoints
{
    private static readonly ActionPlanStore ActionPlans = new();
    private static readonly DeliverableExportService Exports = new();

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
        MapDeliverableExportEndpoints(endpoints);

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
            try
            {
                CapUserContext userContext = userContextAccessor.GetRequiredContext();
                ActionPlanResponse response = ActionPlans.Save(
                    userContext.TenantId,
                    beneficiaryId,
                    userContext.UserId,
                    request);
                return (IResult)Results.Ok(response);
            }
            catch (ArgumentException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }
            catch (InvalidOperationException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }
        });

        group.MapPost("items/{itemId:guid}/complete", (
            Guid beneficiaryId,
            Guid itemId,
            ICapUserContextAccessor userContextAccessor) =>
        {
            try
            {
                CapUserContext userContext = userContextAccessor.GetRequiredContext();
                ActionPlanResponse response = ActionPlans.CompleteItem(userContext.TenantId, beneficiaryId, itemId);
                return (IResult)Results.Ok(response);
            }
            catch (ArgumentException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }
            catch (KeyNotFoundException exception)
            {
                return Results.NotFound(new { error = exception.Message });
            }
        });
    }

    private static void MapDeliverableExportEndpoints(IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("/api/beneficiaries/{beneficiaryId:guid}/deliverables");
        group.RequireAuthorization();

        group.MapGet("bilan.md", (
            Guid beneficiaryId,
            ICapUserContextAccessor userContextAccessor,
            EditableSynthesisStore synthesisStore) =>
        {
            try
            {
                CapUserContext userContext = userContextAccessor.GetRequiredContext();
                SynthesisResponse synthesis = synthesisStore.GetOrCreate(userContext.TenantId, beneficiaryId);
                ActionPlanResponse actionPlan = ActionPlans.GetOrCreate(userContext.TenantId, beneficiaryId);
                DeliverableExport export = Exports.Build(userContext.TenantId, beneficiaryId, synthesis, actionPlan);

                return (IResult)Results.File(export.Content, export.ContentType, export.FileName);
            }
            catch (ArgumentException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }
            catch (InvalidOperationException exception)
            {
                return Results.Conflict(new { error = exception.Message });
            }
        });
    }
}