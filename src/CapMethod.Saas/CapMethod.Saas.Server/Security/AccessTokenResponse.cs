namespace CapMethod.Saas.Server.Security;

public sealed record AccessTokenResponse(
    string AccessToken,
    string TokenType,
    DateTimeOffset ExpiresAtUtc,
    Guid TenantId,
    Guid UserId);
