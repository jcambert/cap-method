using System.Security.Claims;

namespace CapMethod.Saas.Server.Audit;

public sealed class AuditMiddleware
{
    private static readonly HashSet<string> AuditedMethods = new(StringComparer.OrdinalIgnoreCase)
    {
        HttpMethods.Post,
        HttpMethods.Put,
        HttpMethods.Patch,
        HttpMethods.Delete
    };

    private readonly RequestDelegate _next;
    private readonly AuditEventStore _store;
    private readonly ILogger<AuditMiddleware> _logger;

    public AuditMiddleware(RequestDelegate next, AuditEventStore store, ILogger<AuditMiddleware> logger)
    {
        _next = next;
        _store = store;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (!ShouldAudit(context) || !TryReadGuidClaim(context.User, "tenant_id", out Guid tenantId) ||
            !TryReadGuidClaim(context.User, ClaimTypes.NameIdentifier, out Guid userId))
        {
            return;
        }

        AuditEvent auditEvent = new(
            Guid.NewGuid(),
            tenantId,
            userId,
            context.Request.Method.ToUpperInvariant(),
            NormalizeResource(context.Request.Path),
            context.Response.StatusCode,
            context.TraceIdentifier,
            DateTimeOffset.UtcNow);

        _store.Append(auditEvent);
        _logger.LogInformation(
            "Audit event {AuditEventId}: {Action} {Resource} returned {StatusCode} for tenant {TenantId} and user {UserId}",
            auditEvent.EventId,
            auditEvent.Action,
            auditEvent.Resource,
            auditEvent.StatusCode,
            auditEvent.TenantId,
            auditEvent.UserId);
    }

    private static bool ShouldAudit(HttpContext context)
    {
        return context.User.Identity?.IsAuthenticated == true &&
               context.Request.Path.StartsWithSegments("/api") &&
               !context.Request.Path.StartsWithSegments("/api/auth") &&
               !context.Request.Path.StartsWithSegments("/api/beneficiary/auth") &&
               AuditedMethods.Contains(context.Request.Method) &&
               context.Response.StatusCode < StatusCodes.Status500InternalServerError;
    }

    private static string NormalizeResource(PathString path)
    {
        string value = path.Value ?? "/api";
        return value.Length <= 300 ? value : value[..300];
    }

    private static bool TryReadGuidClaim(ClaimsPrincipal principal, string claimType, out Guid value)
    {
        return Guid.TryParse(principal.FindFirstValue(claimType), out value) && value != Guid.Empty;
    }
}