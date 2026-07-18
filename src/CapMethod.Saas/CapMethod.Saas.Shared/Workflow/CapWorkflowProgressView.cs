namespace CapMethod.Saas.Shared.Workflow;

public sealed record CapWorkflowProgressView(
    CapWorkflowStepView CurrentStep,
    IReadOnlyCollection<CapWorkflowStepStateView> Steps,
    int CompletedRequiredStepCount,
    int RequiredStepCount,
    int CompletionPercent);
