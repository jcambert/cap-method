namespace CapMethod.Saas.Shared.Workflow;

public sealed record CapWorkflowStepStateView(
    CapWorkflowStepView Step,
    bool IsCompleted,
    bool IsCurrent);
