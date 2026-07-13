namespace CapMethod.Saas.Domain.Sessions;

public sealed record CapSessionStatus(string Code)
{
    public static readonly CapSessionStatus Draft = new("Draft");

    public static readonly CapSessionStatus QuestionnairesSent = new("QuestionnairesSent");

    public static readonly CapSessionStatus InProgress = new("InProgress");

    public static readonly CapSessionStatus ResponsesCompleted = new("ResponsesCompleted");

    public static readonly CapSessionStatus AnalysisGenerated = new("AnalysisGenerated");

    public static readonly CapSessionStatus AiAnalysisDraftGenerated = new("AIAnalysisDraftGenerated");

    public static readonly CapSessionStatus ConsultantReview = new("ConsultantReview");

    public static readonly CapSessionStatus Validated = new("Validated");

    public static readonly CapSessionStatus Delivered = new("Delivered");

    public static readonly CapSessionStatus Archived = new("Archived");

    public override string ToString()
    {
        return Code;
    }
}
