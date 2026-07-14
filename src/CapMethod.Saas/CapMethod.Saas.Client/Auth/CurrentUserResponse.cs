namespace CapMethod.Saas.Client.Auth;

public sealed record CurrentUserResponse(
    Guid TenantId,
    Guid UserId,
    bool IsAuthenticated,
    bool IsDevelopmentFallback);
