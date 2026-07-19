using CapMethod.Saas.Server.Questionnaires;
using CapMethod.Saas.Shared.Analysis;
using CapMethod.Saas.Shared.Questionnaires;

namespace CapMethod.Saas.Server.Analysis;

public sealed class StructuredAnalysisService
{
    private static readonly HashSet<string> StopWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "avec", "dans", "pour", "plus", "moins", "mais", "donc", "ainsi", "cette", "comme",
        "sont", "être", "avoir", "tout", "tous", "toutes", "mon", "mes", "que", "qui", "une",
        "des", "les", "aux", "sur", "par", "pas", "très", "leur", "leurs", "nous", "vous"
    };

    private readonly BeneficiaryQuestionnaireStore _store;

    public StructuredAnalysisService(BeneficiaryQuestionnaireStore store)
    {
        _store = store;
    }

    public StructuredAnalysisResponse Generate(Guid tenantId, Guid beneficiaryId)
    {
        IReadOnlyCollection<QuestionnaireProgressResponse> submitted = _store.ListSubmittedProgress(tenantId, beneficiaryId);
        QuestionnaireAnswerResponse[] answers = submitted
            .SelectMany(progress => progress.Answers)
            .Where(answer => !string.IsNullOrWhiteSpace(answer.Value))
            .ToArray();

        int totalCharacters = answers.Sum(answer => answer.Value.Length);
        int definitionCount = _store.ListDefinitions().Count;
        int completionScore = definitionCount == 0
            ? 0
            : (int)Math.Round(submitted.Count * 100d / definitionCount, MidpointRounding.AwayFromZero);

        string[] keywords = ExtractKeywords(answers);
        StructuredAnalysisIndicatorResponse[] indicators =
        [
            new StructuredAnalysisIndicatorResponse(
                "completion",
                "Complétude des questionnaires",
                completionScore,
                100,
                $"{submitted.Count} questionnaire(s) soumis sur {definitionCount}."),
            new StructuredAnalysisIndicatorResponse(
                "response-depth",
                "Profondeur des réponses",
                Math.Min(100, totalCharacters / 30),
                100,
                $"{totalCharacters} caractères exploitables dans {answers.Length} réponse(s)."),
            new StructuredAnalysisIndicatorResponse(
                "topic-diversity",
                "Diversité des thèmes",
                Math.Min(100, keywords.Length * 10),
                100,
                $"{keywords.Length} mot(s)-clé(s) significatif(s) identifié(s).")
        ];

        return new StructuredAnalysisResponse(
            tenantId,
            beneficiaryId,
            submitted.Count,
            answers.Length,
            totalCharacters,
            completionScore,
            keywords,
            indicators,
            DateTimeOffset.UtcNow);
    }

    private static string[] ExtractKeywords(IEnumerable<QuestionnaireAnswerResponse> answers)
    {
        return answers
            .SelectMany(answer => answer.Value.Split(
                [' ', '\t', '\r', '\n', ',', '.', ';', ':', '!', '?', '(', ')', '\'', '’', '"', '-', '/'],
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Select(word => word.ToLowerInvariant())
            .Where(word => word.Length >= 4 && !StopWords.Contains(word))
            .GroupBy(word => word, StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key, StringComparer.Ordinal)
            .Take(10)
            .Select(group => group.Key)
            .ToArray();
    }
}
