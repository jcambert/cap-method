namespace CapMethod.Saas.Server.Security;

public sealed record BeneficiaryPortalContextResponse(
    Guid TenantId,
    Guid BeneficiaryId,
    string Email,
    bool IsAuthenticated);
