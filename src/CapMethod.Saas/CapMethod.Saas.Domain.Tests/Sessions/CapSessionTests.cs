using CapMethod.Saas.Domain.Sessions;
using Xunit;

namespace CapMethod.Saas.Domain.Tests.Sessions;

public sealed class CapSessionTests
{
    [Fact]
    public void Create_should_create_session_without_ai()
    {
        Guid tenantId = Guid.NewGuid();
        Guid beneficiaryId = Guid.NewGuid();
        Guid consultantId = Guid.NewGuid();

        CapSession session = CapSession.Create(tenantId, beneficiaryId, consultantId);

        Assert.Equal(tenantId, session.TenantId);
        Assert.Equal(beneficiaryId, session.BeneficiaryId);
        Assert.Equal(consultantId, session.ConsultantId);
        Assert.Equal(CapSessionStatus.Draft, session.Status);
        Assert.False(session.IsAiEnabled);
    }

    [Fact]
    public void EnableAi_should_enable_optional_ai()
    {
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        session.EnableAi();

        Assert.True(session.IsAiEnabled);
    }

    [Fact]
    public void MarkAiDraftGenerated_should_fail_when_ai_is_disabled()
    {
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(session.MarkAiDraftGenerated);

        Assert.Equal("AI draft cannot be generated when AI is disabled.", exception.Message);
    }

    [Fact]
    public void MarkAiDraftGenerated_should_update_status_when_ai_is_enabled()
    {
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        session.EnableAi();
        session.MarkAiDraftGenerated();

        Assert.Equal(CapSessionStatus.AiAnalysisDraftGenerated, session.Status);
    }
}
