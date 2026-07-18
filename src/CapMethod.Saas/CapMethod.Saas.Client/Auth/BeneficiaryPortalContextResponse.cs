namespace CapMethod.Saas.Client.Auth;

public sealed record BeneficiaryPortalContextResponse(
    Guid TenantId,
    Guid BeneficiaryId,
    string Email,
    bool IsAuthenticated);
