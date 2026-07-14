namespace CapMethod.Saas.Shared.Beneficiaries;

public sealed record CreateBeneficiaryRequest(
    Guid TenantId,
    string FirstName,
    string LastName,
    string? Email);
