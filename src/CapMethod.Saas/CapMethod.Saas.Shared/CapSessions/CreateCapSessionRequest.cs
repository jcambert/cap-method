namespace CapMethod.Saas.Shared.CapSessions;

public sealed record CreateCapSessionRequest(
    Guid TenantId,
    Guid BeneficiaryId,
    Guid ConsultantId,
    bool EnableAi);
