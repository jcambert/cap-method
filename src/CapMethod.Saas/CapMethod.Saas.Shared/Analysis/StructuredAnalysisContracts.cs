namespace CapMethod.Saas.Shared.Analysis;

public sealed record StructuredAnalysisIndicatorResponse(
    string IndicatorId,
    string Label,
    int Score,
    int MaximumScore,
    string Evidence);

public sealed record StructuredAnalysisResponse(
    Guid TenantId,
    Guid BeneficiaryId,
    int SubmittedQuestionnaires,
    int TotalAnswers,
    int TotalCharacters,
    int CompletionScore,
    IReadOnlyCollection<string> Keywords,
    IReadOnlyCollection<StructuredAnalysisIndicatorResponse> Indicators,
    DateTimeOffset GeneratedAtUtc);
