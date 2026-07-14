namespace CapMethod.Saas.Client.Auth;

public sealed record DevelopmentTokenResponse(
    string AccessToken,
    string TokenType,
    DateTimeOffset ExpiresAtUtc,
    Guid TenantId,
    Guid UserId);
