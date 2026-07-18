using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Domain.Workflow;

public sealed class CapWorkflowPlan
{
    private static readonly IReadOnlyCollection<CapWorkflowStep> StandardSteps =
    [
        CapWorkflowStep.Intake,
        CapWorkflowStep.Questionnaires,
        CapWorkflowStep.Responses,
        CapWorkflowStep.StructuredAnalysis,
        CapWorkflowStep.ConsultantReview,
        CapWorkflowStep.Synthesis,
        CapWorkflowStep.Delivery,
        CapWorkflowStep.Archive
    ];

    private static readonly IReadOnlyCollection<CapWorkflowStep> AiSteps =
    [
        CapWorkflowStep.Intake,
        CapWorkflowStep.Questionnaires,
        CapWorkflowStep.Responses,
        CapWorkflowStep.StructuredAnalysis,
        CapWorkflowStep.AiDraft,
        CapWorkflowStep.ConsultantReview,
        CapWorkflowStep.Synthesis,
        CapWorkflowStep.Delivery,
        CapWorkflowStep.Archive
    ];

    private static readonly IReadOnlyDictionary<CapSessionStatus, CapWorkflowStep> StatusToStep =
        new Dictionary<CapSessionStatus, CapWorkflowStep>
        {
            [CapSessionStatus.Draft] = CapWorkflowStep.Intake,
            [CapSessionStatus.QuestionnairesSent] = CapWorkflowStep.Questionnaires,
            [CapSessionStatus.InProgress] = CapWorkflowStep.Questionnaires,
            [CapSessionStatus.ResponsesCompleted] = CapWorkflowStep.Responses,
            [CapSessionStatus.AnalysisGenerated] = CapWorkflowStep.StructuredAnalysis,
            [CapSessionStatus.AiAnalysisDraftGenerated] = CapWorkflowStep.AiDraft,
            [CapSessionStatus.ConsultantReview] = CapWorkflowStep.ConsultantReview,
            [CapSessionStatus.Validated] = CapWorkflowStep.Synthesis,
            [CapSessionStatus.Delivered] = CapWorkflowStep.Delivery,
            [CapSessionStatus.Archived] = CapWorkflowStep.Archive
        };

    private readonly IReadOnlyCollection<CapWorkflowStep> _steps;

    private CapWorkflowPlan(IReadOnlyCollection<CapWorkflowStep> steps)
    {
        _steps = steps;
    }

    public static CapWorkflowPlan ForSession(bool isAiEnabled)
    {
        IReadOnlyCollection<CapWorkflowStep> steps = isAiEnabled ? AiSteps : StandardSteps;

        return new CapWorkflowPlan(steps);
    }

    public CapWorkflowProgress BuildProgress(CapSessionStatus status)
    {
        CapWorkflowStep currentStep = ResolveCurrentStep(status);
        CapWorkflowStepState[] states = _steps
            .Select(step => new CapWorkflowStepState(
                step,
                IsCompleted: step.Order < currentStep.Order,
                IsCurrent: step == currentStep))
            .ToArray();

        return new CapWorkflowProgress(currentStep, states);
    }

    private CapWorkflowStep ResolveCurrentStep(CapSessionStatus status)
    {
        if (!StatusToStep.TryGetValue(status, out CapWorkflowStep? mappedStep))
        {
            throw new InvalidOperationException($"CAP session status '{status}' is not mapped to a workflow step.");
        }

        if (!_steps.Contains(mappedStep) && mappedStep.RequiresAi)
        {
            return CapWorkflowStep.ConsultantReview;
        }

        if (!_steps.Contains(mappedStep))
        {
            throw new InvalidOperationException($"CAP workflow step '{mappedStep.Code}' is not available in the current workflow plan.");
        }

        return mappedStep;
    }
}