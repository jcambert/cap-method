namespace CapMethod.Saas.Server.Security;

public sealed record DevelopmentTokenResponse(
    string AccessToken,
    string TokenType,
    DateTimeOffset ExpiresAtUtc,
    Guid TenantId,
    Guid UserId);
