namespace CapMethod.Saas.Client.Auth;

public sealed record BeneficiaryAccessTokenResponse(
    string AccessToken,
    string TokenType,
    DateTimeOffset ExpiresAtUtc,
    Guid TenantId,
    Guid BeneficiaryId,
    string Email);
