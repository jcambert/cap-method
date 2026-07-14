namespace CapMethod.Saas.Domain.Beneficiaries;

public sealed class Beneficiary
{
    private Beneficiary(Guid tenantId, string firstName, string lastName, string? email)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; }

    public Guid TenantId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string? Email { get; }

    public DateTimeOffset CreatedAtUtc { get; }

    public static Beneficiary Create(Guid tenantId, string firstName, string lastName, string? email)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId is required.", nameof(tenantId));
        }

        string normalizedFirstName = NormalizeRequiredText(firstName, nameof(firstName));
        string normalizedLastName = NormalizeRequiredText(lastName, nameof(lastName));
        string? normalizedEmail = NormalizeOptionalText(email);

        return new Beneficiary(tenantId, normalizedFirstName, normalizedLastName, normalizedEmail);
    }

    private static string NormalizeRequiredText(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{parameterName} is required.", parameterName);
        }

        return value.Trim();
    }

    private static string? NormalizeOptionalText(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return value.Trim();
    }
}
