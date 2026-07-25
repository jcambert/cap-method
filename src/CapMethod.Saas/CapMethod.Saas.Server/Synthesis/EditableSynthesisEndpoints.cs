using CapMethod.Saas.Infrastructure.Persistence;
using CapMethod.Saas.Server.ActionPlans;
using CapMethod.Saas.Server.Exports;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.ActionPlans;
using CapMethod.Saas.Shared.Synthesis;

namespace CapMethod.Saas.Server.Synthesis;

public static class EditableSynthesisEndpoints
{
    private static readonly DeliverableExportService Exports = new();

    public static IEndpointRouteBuilder MapEditableSynthesisEndpoints(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder synthesisGroup = endpoints.MapGroup("/api/beneficiaries/{beneficiaryId:guid}/synthesis");
        synthesisGroup.RequireAuthorization();
        synthesisGroup.MapGet("", (Guid beneficiaryId, ICapUserContextAccessor accessor, EditableSynthesisStore store) =>
        {
            CapUserContext context = accessor.GetRequiredContext();
            return Results.Ok(store.GetOrCreate(context.TenantId, beneficiaryId));
        });
        synthesisGroup.MapPut("", (Guid beneficiaryId, SaveSynthesisRequest request, ICapUserContextAccessor accessor, EditableSynthesisStore store) =>
        {
            CapUserContext context = accessor.GetRequiredContext();
            return Results.Ok(store.Save(context.TenantId, beneficiaryId, context.UserId, request));
        });

        MapActionPlanEndpoints(endpoints);
        MapDeliverableExportEndpoints(endpoints);
        return endpoints;
    }

    private static void MapActionPlanEndpoints(IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("/api/beneficiaries/{beneficiaryId:guid}/action-plan");
        group.RequireAuthorization();
        group.MapGet("", (Guid beneficiaryId, ICapUserContextAccessor accessor, IOperationalSnapshotStore snapshots) =>
        {
            CapUserContext context = accessor.GetRequiredContext();
            return Results.Ok(new ActionPlanStore(snapshots).GetOrCreate(context.TenantId, beneficiaryId));
        });
        group.MapPut("", (Guid beneficiaryId, SaveActionPlanRequest request, ICapUserContextAccessor accessor, IOperationalSnapshotStore snapshots) =>
        {
            try
            {
                CapUserContext context = accessor.GetRequiredContext();
                return (IResult)Results.Ok(new ActionPlanStore(snapshots).Save(context.TenantId, beneficiaryId, context.UserId, request));
            }
            catch (ArgumentException exception) { return Results.BadRequest(new { error = exception.Message }); }
            catch (InvalidOperationException exception) { return Results.BadRequest(new { error = exception.Message }); }
        });
        group.MapPost("items/{itemId:guid}/complete", (Guid beneficiaryId, Guid itemId, ICapUserContextAccessor accessor, IOperationalSnapshotStore snapshots) =>
        {
            try
            {
                CapUserContext context = accessor.GetRequiredContext();
                return (IResult)Results.Ok(new ActionPlanStore(snapshots).CompleteItem(context.TenantId, beneficiaryId, itemId));
            }
            catch (ArgumentException exception) { return Results.BadRequest(new { error = exception.Message }); }
            catch (KeyNotFoundException exception) { return Results.NotFound(new { error = exception.Message }); }
        });
    }

    private static void MapDeliverableExportEndpoints(IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("/api/beneficiaries/{beneficiaryId:guid}/deliverables");
        group.RequireAuthorization();
        group.MapGet("bilan.md", (Guid beneficiaryId, ICapUserContextAccessor accessor, EditableSynthesisStore synthesisStore, IOperationalSnapshotStore snapshots) =>
        {
            try
            {
                CapUserContext context = accessor.GetRequiredContext();
                SynthesisResponse synthesis = synthesisStore.GetOrCreate(context.TenantId, beneficiaryId);
                ActionPlanResponse actionPlan = new ActionPlanStore(snapshots).GetOrCreate(context.TenantId, beneficiaryId);
                DeliverableExport export = Exports.Build(context.TenantId, beneficiaryId, synthesis, actionPlan);
                return (IResult)Results.File(export.Content, export.ContentType, export.FileName);
            }
            catch (ArgumentException exception) { return Results.BadRequest(new { error = exception.Message }); }
            catch (InvalidOperationException exception) { return Results.Conflict(new { error = exception.Message }); }
        });
    }
}
