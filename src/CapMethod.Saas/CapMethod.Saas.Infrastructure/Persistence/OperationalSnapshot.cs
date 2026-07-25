namespace CapMethod.Saas.Infrastructure.Persistence;

public sealed class OperationalSnapshot
{
    public Guid TenantId { get; set; }

    public Guid BeneficiaryId { get; set; }

    public string DocumentType { get; set; } = string.Empty;

    public string DocumentKey { get; set; } = string.Empty;

    public string PayloadJson { get; set; } = string.Empty;

    public DateTimeOffset UpdatedAtUtc { get; set; }
}
