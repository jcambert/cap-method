namespace CapMethod.Saas.Domain.Workflow;

public sealed record CapWorkflowProgress(
    CapWorkflowStep CurrentStep,
    IReadOnlyCollection<CapWorkflowStepState> Steps)
{
    public int CompletedRequiredStepCount
    {
        get
        {
            return Steps.Count(step => step.Step.IsRequired && step.IsCompleted);
        }
    }

    public int RequiredStepCount
    {
        get
        {
            return Steps.Count(step => step.Step.IsRequired);
        }
    }

    public decimal CompletionRate
    {
        get
        {
            if (RequiredStepCount == 0)
            {
                return 0m;
            }

            return decimal.Divide(CompletedRequiredStepCount, RequiredStepCount);
        }
    }
}