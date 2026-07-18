namespace CapMethod.Saas.Shared.Workflow;

public static class CapWorkflowProgressProjector
{
    private static readonly CapWorkflowStepView Intake = new(
        "Intake",
        "Cadrage du bilan",
        10,
        IsRequired: true,
        RequiresAi: false);

    private static readonly CapWorkflowStepView Questionnaires = new(
        "Questionnaires",
        "Questionnaires bénéficiaire",
        20,
        IsRequired: true,
        RequiresAi: false);

    private static readonly CapWorkflowStepView Responses = new(
        "Responses",
        "Réponses complétées",
        30,
        IsRequired: true,
        RequiresAi: false);

    private static readonly CapWorkflowStepView StructuredAnalysis = new(
        "StructuredAnalysis",
        "Analyse structurée",
        40,
        IsRequired: true,
        RequiresAi: false);

    private static readonly CapWorkflowStepView AiDraft = new(
        "AiDraft",
        "Brouillon IA optionnel",
        50,
        IsRequired: false,
        RequiresAi: true);

    private static readonly CapWorkflowStepView ConsultantReview = new(
        "ConsultantReview",
        "Revue consultant",
        60,
        IsRequired: true,
        RequiresAi: false);

    private static readonly CapWorkflowStepView Synthesis = new(
        "Synthesis",
        "Synthèse validée",
        70,
        IsRequired: true,
        RequiresAi: false);

    private static readonly CapWorkflowStepView Delivery = new(
        "Delivery",
        "Livraison des livrables",
        80,
        IsRequired: true,
        RequiresAi: false);

    private static readonly CapWorkflowStepView Archive = new(
        "Archive",
        "Archivage",
        90,
        IsRequired: false,
        RequiresAi: false);

    private static readonly IReadOnlyCollection<CapWorkflowStepView> StandardSteps =
    [
        Intake,
        Questionnaires,
        Responses,
        StructuredAnalysis,
        ConsultantReview,
        Synthesis,
        Delivery,
        Archive
    ];

    private static readonly IReadOnlyCollection<CapWorkflowStepView> AiSteps =
    [
        Intake,
        Questionnaires,
        Responses,
        StructuredAnalysis,
        AiDraft,
        ConsultantReview,
        Synthesis,
        Delivery,
        Archive
    ];

    private static readonly IReadOnlyDictionary<string, CapWorkflowStepView> StatusToStep =
        new Dictionary<string, CapWorkflowStepView>(StringComparer.Ordinal)
        {
            ["Draft"] = Intake,
            ["QuestionnairesSent"] = Questionnaires,
            ["InProgress"] = Questionnaires,
            ["ResponsesCompleted"] = Responses,
            ["AnalysisGenerated"] = StructuredAnalysis,
            ["AIAnalysisDraftGenerated"] = AiDraft,
            ["ConsultantReview"] = ConsultantReview,
            ["Validated"] = Synthesis,
            ["Delivered"] = Delivery,
            ["Archived"] = Archive
        };

    public static CapWorkflowProgressView Create(string status, bool isAiEnabled)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            throw new ArgumentException("Status is required.", nameof(status));
        }

        IReadOnlyCollection<CapWorkflowStepView> steps = isAiEnabled ? AiSteps : StandardSteps;
        CapWorkflowStepView currentStep = ResolveCurrentStep(status, steps);
        CapWorkflowStepStateView[] states = steps
            .Select(step => new CapWorkflowStepStateView(
                step,
                IsCompleted: step.Order < currentStep.Order,
                IsCurrent: step == currentStep))
            .ToArray();

        int completedRequiredStepCount = states.Count(step => step.Step.IsRequired && step.IsCompleted);
        int requiredStepCount = states.Count(step => step.Step.IsRequired);
        int completionPercent = CalculateCompletionPercent(completedRequiredStepCount, requiredStepCount);

        return new CapWorkflowProgressView(
            currentStep,
            states,
            completedRequiredStepCount,
            requiredStepCount,
            completionPercent);
    }

    private static CapWorkflowStepView ResolveCurrentStep(
        string status,
        IReadOnlyCollection<CapWorkflowStepView> steps)
    {
        if (!StatusToStep.TryGetValue(status, out CapWorkflowStepView? mappedStep))
        {
            return Intake;
        }

        if (!steps.Contains(mappedStep) && mappedStep.RequiresAi)
        {
            return ConsultantReview;
        }

        if (!steps.Contains(mappedStep))
        {
            return Intake;
        }

        return mappedStep;
    }

    private static int CalculateCompletionPercent(int completedRequiredStepCount, int requiredStepCount)
    {
        if (requiredStepCount == 0)
        {
            return 0;
        }

        decimal completionRate = decimal.Divide(completedRequiredStepCount, requiredStepCount);

        return (int)Math.Round(completionRate * 100m, MidpointRounding.AwayFromZero);
    }
}
