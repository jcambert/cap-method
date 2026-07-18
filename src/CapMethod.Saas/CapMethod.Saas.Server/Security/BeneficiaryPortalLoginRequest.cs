namespace CapMethod.Saas.Server.Security;

public sealed record BeneficiaryPortalLoginRequest(
    string Email,
    string AccessCode);
