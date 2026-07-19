using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.Synthesis;

namespace CapMethod.Saas.Server.Synthesis;

public static class EditableSynthesisEndpoints
{
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

        return endpoints;
    }
}
