namespace CapMethod.Saas.Shared.Workflow;

public sealed record CapWorkflowStepView(
    string Code,
    string Label,
    int Order,
    bool IsRequired,
    bool RequiresAi);
