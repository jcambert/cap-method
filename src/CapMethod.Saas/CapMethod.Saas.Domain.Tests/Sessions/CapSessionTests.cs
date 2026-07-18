using CapMethod.Saas.Domain.Sessions;
using CapMethod.Saas.Domain.Workflow;
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

    [Fact]
    public void GetWorkflowProgress_should_start_on_intake_for_new_session()
    {
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        CapWorkflowProgress progress = session.GetWorkflowProgress();

        Assert.Equal(CapWorkflowStep.Intake, progress.CurrentStep);
        Assert.Equal(0, progress.CompletedRequiredStepCount);
        Assert.DoesNotContain(progress.Steps, step => step.Step.RequiresAi);
    }

    [Fact]
    public void GetWorkflowProgress_should_include_ai_step_when_ai_is_enabled()
    {
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        session.EnableAi();

        CapWorkflowProgress progress = session.GetWorkflowProgress();

        Assert.Contains(progress.Steps, step => step.Step == CapWorkflowStep.AiDraft);
    }

    [Fact]
    public void GetWorkflowProgress_should_move_to_structured_analysis_after_analysis_generation()
    {
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        session.MarkQuestionnairesSent();
        session.MarkResponsesCompleted();
        session.MarkAnalysisGenerated();

        CapWorkflowProgress progress = session.GetWorkflowProgress();

        Assert.Equal(CapWorkflowStep.StructuredAnalysis, progress.CurrentStep);
        Assert.True(progress.CompletedRequiredStepCount > 0);
    }

    [Fact]
    public void GetWorkflowProgress_should_move_to_ai_draft_when_ai_draft_is_generated()
    {
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        session.EnableAi();
        session.MarkQuestionnairesSent();
        session.MarkResponsesCompleted();
        session.MarkAnalysisGenerated();
        session.MarkAiDraftGenerated();

        CapWorkflowProgress progress = session.GetWorkflowProgress();

        Assert.Equal(CapWorkflowStep.AiDraft, progress.CurrentStep);
    }

    [Fact]
    public void GetWorkflowProgress_should_move_to_delivery_when_session_is_delivered()
    {
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        session.MarkConsultantReview();
        session.MarkValidated();
        session.MarkDelivered();

        CapWorkflowProgress progress = session.GetWorkflowProgress();

        Assert.Equal(CapWorkflowStep.Delivery, progress.CurrentStep);
    }
}