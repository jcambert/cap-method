namespace CapMethod.Saas.Domain.Sessions;

public sealed class CapSession
{
    private CapSession()
    {
        Status = CapSessionStatus.Draft;
    }

    private CapSession(Guid tenantId, Guid beneficiaryId, Guid consultantId)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        BeneficiaryId = beneficiaryId;
        ConsultantId = consultantId;
        Status = CapSessionStatus.Draft;
        IsAiEnabled = false;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid TenantId { get; private set; }

    public Guid BeneficiaryId { get; private set; }

    public Guid ConsultantId { get; private set; }

    public CapSessionStatus Status { get; private set; }

    public bool IsAiEnabled { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public static CapSession Create(Guid tenantId, Guid beneficiaryId, Guid consultantId)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId is required.", nameof(tenantId));
        }

        if (beneficiaryId == Guid.Empty)
        {
            throw new ArgumentException("BeneficiaryId is required.", nameof(beneficiaryId));
        }

        if (consultantId == Guid.Empty)
        {
            throw new ArgumentException("ConsultantId is required.", nameof(consultantId));
        }

        return new CapSession(tenantId, beneficiaryId, consultantId);
    }

    public void EnableAi()
    {
        IsAiEnabled = true;
    }

    public void DisableAi()
    {
        IsAiEnabled = false;
    }

    public void MarkAiDraftGenerated()
    {
        if (!IsAiEnabled)
        {
            throw new InvalidOperationException("AI draft cannot be generated when AI is disabled.");
        }

        Status = CapSessionStatus.AiAnalysisDraftGenerated;
    }

    public void MarkConsultantReview()
    {
        Status = CapSessionStatus.ConsultantReview;
    }

    public void MarkValidated()
    {
        Status = CapSessionStatus.Validated;
    }
}
