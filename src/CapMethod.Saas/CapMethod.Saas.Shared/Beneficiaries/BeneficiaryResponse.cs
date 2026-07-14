namespace CapMethod.Saas.Shared.Beneficiaries;

public sealed record BeneficiaryResponse(
    Guid BeneficiaryId,
    Guid TenantId,
    string FirstName,
    string LastName,
    string? Email,
    DateTimeOffset CreatedAtUtc);
