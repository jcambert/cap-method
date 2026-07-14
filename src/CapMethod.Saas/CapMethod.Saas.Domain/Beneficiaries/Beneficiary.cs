namespace CapMethod.Saas.Domain.Beneficiaries;

public sealed class Beneficiary
{
    private Beneficiary()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    private Beneficiary(Guid tenantId, string firstName, string lastName, string? email)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid TenantId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string? Email { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

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
