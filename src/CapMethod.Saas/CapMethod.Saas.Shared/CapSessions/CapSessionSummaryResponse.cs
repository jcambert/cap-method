namespace CapMethod.Saas.Shared.CapSessions;

public sealed record CapSessionSummaryResponse(
    Guid CapSessionId,
    Guid TenantId,
    Guid BeneficiaryId,
    Guid ConsultantId,
    string Status,
    bool IsAiEnabled,
    DateTimeOffset CreatedAtUtc);
