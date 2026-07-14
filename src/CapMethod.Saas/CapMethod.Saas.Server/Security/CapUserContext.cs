namespace CapMethod.Saas.Server.Security;

public sealed record CapUserContext(
    Guid TenantId,
    Guid UserId,
    bool IsAuthenticated,
    bool IsDevelopmentFallback);
