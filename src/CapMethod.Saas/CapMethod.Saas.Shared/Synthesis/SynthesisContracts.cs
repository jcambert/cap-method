namespace CapMethod.Saas.Shared.Synthesis;

public sealed record SynthesisResponse(
    Guid TenantId,
    Guid BeneficiaryId,
    string Content,
    bool IsValidated,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset UpdatedAtUtc,
    DateTimeOffset? ValidatedAtUtc,
    Guid? ValidatedByUserId);

public sealed record SaveSynthesisRequest(
    string Content,
    bool Validate);
