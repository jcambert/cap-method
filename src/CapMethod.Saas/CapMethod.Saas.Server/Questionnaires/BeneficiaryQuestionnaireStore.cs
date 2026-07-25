using System.Text.Json;
using CapMethod.Saas.Infrastructure.Persistence;
using CapMethod.Saas.Shared.Questionnaires;

namespace CapMethod.Saas.Server.Questionnaires;

public sealed class BeneficiaryQuestionnaireStore
{
    private const string DocumentType = "questionnaire";
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private static readonly QuestionnaireDefinitionResponse[] Definitions =
    [
        new QuestionnaireDefinitionResponse("career-exploration", "Exploration du parcours", "Identifier les expériences, réussites et attentes professionnelles du bénéficiaire.",
        [
            new QuestionnaireQuestionResponse("career-highlights", "Quelles expériences professionnelles vous ont le plus marqué ?", true, 2000),
            new QuestionnaireQuestionResponse("strengths", "Quelles sont les compétences que vous mobilisez le plus facilement ?", true, 2000),
            new QuestionnaireQuestionResponse("expectations", "Qu’attendez-vous prioritairement de votre bilan de compétences ?", true, 2000)
        ]),
        new QuestionnaireDefinitionResponse("work-values", "Valeurs et environnement de travail", "Clarifier les valeurs, besoins et conditions de travail recherchées.",
        [
            new QuestionnaireQuestionResponse("values", "Quelles valeurs doivent être présentes dans votre futur environnement professionnel ?", true, 2000),
            new QuestionnaireQuestionResponse("conditions", "Quelles conditions de travail favorisent votre engagement ?", true, 2000),
            new QuestionnaireQuestionResponse("constraints", "Quelles contraintes personnelles ou professionnelles faut-il prendre en compte ?", false, 2000)
        ])
    ];

    private readonly IOperationalSnapshotStore _snapshots;

    public BeneficiaryQuestionnaireStore()
        : this(new InMemoryOperationalSnapshotStore())
    {
    }

    public BeneficiaryQuestionnaireStore(IOperationalSnapshotStore snapshots)
    {
        _snapshots = snapshots;
    }

    public IReadOnlyCollection<QuestionnaireDefinitionResponse> ListDefinitions() => Definitions;

    public QuestionnaireDefinitionResponse? FindDefinition(string questionnaireId) => Definitions.FirstOrDefault(definition => string.Equals(definition.QuestionnaireId, questionnaireId, StringComparison.Ordinal));

    public QuestionnaireProgressResponse GetProgress(Guid tenantId, Guid beneficiaryId, string questionnaireId)
    {
        QuestionnaireDefinitionResponse definition = FindDefinition(questionnaireId) ?? throw new KeyNotFoundException($"Questionnaire '{questionnaireId}' was not found.");
        string? payload = _snapshots.Read(tenantId, beneficiaryId, DocumentType, questionnaireId);
        StoredQuestionnaireProgress? stored = payload is null ? null : JsonSerializer.Deserialize<StoredQuestionnaireProgress>(payload, JsonOptions);
        return stored is null
            ? new QuestionnaireProgressResponse(questionnaireId, 0, definition.Questions.Count, false, DateTimeOffset.MinValue, [])
            : MapProgress(definition, stored);
    }

    public IReadOnlyCollection<QuestionnaireProgressResponse> ListSubmittedProgress(Guid tenantId, Guid beneficiaryId) => Definitions
        .Select(definition => GetProgress(tenantId, beneficiaryId, definition.QuestionnaireId))
        .Where(progress => progress.IsSubmitted)
        .OrderBy(progress => progress.QuestionnaireId, StringComparer.Ordinal)
        .ToArray();

    public QuestionnaireProgressResponse Save(Guid tenantId, Guid beneficiaryId, string questionnaireId, SaveQuestionnaireAnswersRequest request)
    {
        QuestionnaireDefinitionResponse definition = FindDefinition(questionnaireId) ?? throw new KeyNotFoundException($"Questionnaire '{questionnaireId}' was not found.");
        Dictionary<string, QuestionnaireQuestionResponse> questionsById = definition.Questions.ToDictionary(question => question.QuestionId, StringComparer.Ordinal);
        Dictionary<string, string> answers = new(StringComparer.Ordinal);

        foreach (QuestionnaireAnswerRequest answer in request.Answers)
        {
            if (!questionsById.TryGetValue(answer.QuestionId, out QuestionnaireQuestionResponse? question))
            {
                throw new ArgumentException($"Question '{answer.QuestionId}' does not belong to questionnaire '{questionnaireId}'.", nameof(request));
            }

            string value = answer.Value?.Trim() ?? string.Empty;
            if (value.Length > question.MaximumLength)
            {
                throw new ArgumentException($"Answer '{answer.QuestionId}' exceeds {question.MaximumLength} characters.", nameof(request));
            }

            answers[answer.QuestionId] = value;
        }

        if (request.Submit)
        {
            QuestionnaireQuestionResponse? missingQuestion = definition.Questions.FirstOrDefault(question => question.IsRequired && (!answers.TryGetValue(question.QuestionId, out string? value) || string.IsNullOrWhiteSpace(value)));
            if (missingQuestion is not null)
            {
                throw new ArgumentException($"Required question '{missingQuestion.QuestionId}' must be answered before submission.", nameof(request));
            }
        }

        StoredQuestionnaireProgress stored = new(answers, request.Submit, DateTimeOffset.UtcNow);
        _snapshots.Write(tenantId, beneficiaryId, DocumentType, questionnaireId, JsonSerializer.Serialize(stored, JsonOptions));
        return MapProgress(definition, stored);
    }

    private static QuestionnaireProgressResponse MapProgress(QuestionnaireDefinitionResponse definition, StoredQuestionnaireProgress stored)
    {
        QuestionnaireAnswerResponse[] answers = stored.Answers.OrderBy(item => item.Key, StringComparer.Ordinal).Select(item => new QuestionnaireAnswerResponse(item.Key, item.Value)).ToArray();
        return new QuestionnaireProgressResponse(definition.QuestionnaireId, stored.Answers.Count(item => !string.IsNullOrWhiteSpace(item.Value)), definition.Questions.Count, stored.IsSubmitted, stored.UpdatedAtUtc, answers);
    }

    public sealed record StoredQuestionnaireProgress(IReadOnlyDictionary<string, string> Answers, bool IsSubmitted, DateTimeOffset UpdatedAtUtc);
}
