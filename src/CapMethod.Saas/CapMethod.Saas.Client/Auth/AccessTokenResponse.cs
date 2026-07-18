namespace CapMethod.Saas.Client.Auth;

public sealed record AccessTokenResponse(
    string AccessToken,
    string TokenType,
    DateTimeOffset ExpiresAtUtc,
    Guid TenantId,
    Guid UserId);
