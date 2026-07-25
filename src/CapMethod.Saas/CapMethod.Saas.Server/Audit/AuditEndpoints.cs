using CapMethod.Saas.Server.Security;

namespace CapMethod.Saas.Server.Audit;

public static class AuditEndpoints
{
    public static IEndpointRouteBuilder MapAuditEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/audit/events", (
            int? limit,
            ICapUserContextAccessor userContextAccessor,
            AuditEventStore store) =>
        {
            CapUserContext userContext = userContextAccessor.GetRequiredContext();
            IReadOnlyCollection<AuditEvent> events = store.List(userContext.TenantId, limit ?? 100);
            return Results.Ok(events);
        }).RequireAuthorization();

        return endpoints;
    }
}