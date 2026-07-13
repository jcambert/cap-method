namespace CapMethod.Saas.Domain.Deliverables;

public sealed class DeliverablePackage
{
    private DeliverablePackage(Guid tenantId, Guid capSessionId, bool isValidated, bool containsAiDraft)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        CapSessionId = capSessionId;
        IsValidated = isValidated;
        ContainsAiDraft = containsAiDraft;
    }

    public Guid Id { get; }

    public Guid TenantId { get; }

    public Guid CapSessionId { get; }

    public bool IsValidated { get; private set; }

    public bool ContainsAiDraft { get; }

    public bool CanBeDeliveredToBeneficiary => IsValidated && !ContainsAiDraft;

    public static DeliverablePackage CreateDraft(Guid tenantId, Guid capSessionId, bool containsAiDraft)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId is required.", nameof(tenantId));
        }

        if (capSessionId == Guid.Empty)
        {
            throw new ArgumentException("CapSessionId is required.", nameof(capSessionId));
        }

        return new DeliverablePackage(tenantId, capSessionId, false, containsAiDraft);
    }

    public void ValidateForDelivery()
    {
        if (ContainsAiDraft)
        {
            throw new InvalidOperationException("AI drafts cannot be delivered to beneficiaries.");
        }

        IsValidated = true;
    }
}
