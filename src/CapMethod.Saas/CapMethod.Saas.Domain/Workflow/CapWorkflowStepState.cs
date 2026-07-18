namespace CapMethod.Saas.Domain.Workflow;

public sealed record CapWorkflowStepState(
    CapWorkflowStep Step,
    bool IsCompleted,
    bool IsCurrent);