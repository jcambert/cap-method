namespace CapMethod.Saas.Server.Security;

public sealed class ProductionAuthenticationOptions
{
    public string Email { get; init; } = string.Empty;

    public string PasswordSha256 { get; init; } = string.Empty;

    public Guid TenantId { get; init; }

    public Guid UserId { get; init; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            throw new InvalidOperationException("Authentication:ProductionUser:Email is required.");
        }

        if (string.IsNullOrWhiteSpace(PasswordSha256) || PasswordSha256.Length != 64)
        {
            throw new InvalidOperationException("Authentication:ProductionUser:PasswordSha256 must contain a SHA-256 hexadecimal hash.");
        }

        if (TenantId == Guid.Empty)
        {
            throw new InvalidOperationException("Authentication:ProductionUser:TenantId is required.");
        }

        if (UserId == Guid.Empty)
        {
            throw new InvalidOperationException("Authentication:ProductionUser:UserId is required.");
        }
    }
}
