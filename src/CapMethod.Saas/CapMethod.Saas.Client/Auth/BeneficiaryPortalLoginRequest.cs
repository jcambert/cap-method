namespace CapMethod.Saas.Client.Auth;

public sealed record BeneficiaryPortalLoginRequest(
    string Email,
    string AccessCode);
