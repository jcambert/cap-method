namespace CapMethod.Saas.Server.Security;

public sealed class BeneficiaryPortalAuthenticationOptions
{
    public string Email { get; init; } = string.Empty;

    public string AccessCodeSha256 { get; init; } = string.Empty;

    public Guid TenantId { get; init; }

    public Guid BeneficiaryId { get; init; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            throw new InvalidOperationException("Authentication:BeneficiaryPortal:Email is required.");
        }

        if (string.IsNullOrWhiteSpace(AccessCodeSha256))
        {
            throw new InvalidOperationException("Authentication:BeneficiaryPortal:AccessCodeSha256 is required.");
        }

        if (TenantId == Guid.Empty)
        {
            throw new InvalidOperationException("Authentication:BeneficiaryPortal:TenantId is required.");
        }

        if (BeneficiaryId == Guid.Empty)
        {
            throw new InvalidOperationException("Authentication:BeneficiaryPortal:BeneficiaryId is required.");
        }
    }
}
