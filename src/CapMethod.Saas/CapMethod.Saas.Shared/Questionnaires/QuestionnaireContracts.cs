namespace CapMethod.Saas.Shared.Questionnaires;

public sealed record QuestionnaireQuestionResponse(
    string QuestionId,
    string Label,
    bool IsRequired,
    int MaximumLength);

public sealed record QuestionnaireDefinitionResponse(
    string QuestionnaireId,
    string Title,
    string Description,
    IReadOnlyCollection<QuestionnaireQuestionResponse> Questions);

public sealed record QuestionnaireAnswerRequest(
    string QuestionId,
    string Value);

public sealed record SaveQuestionnaireAnswersRequest(
    IReadOnlyCollection<QuestionnaireAnswerRequest> Answers,
    bool Submit);

public sealed record QuestionnaireAnswerResponse(
    string QuestionId,
    string Value);

public sealed record QuestionnaireProgressResponse(
    string QuestionnaireId,
    int AnsweredQuestions,
    int TotalQuestions,
    bool IsSubmitted,
    DateTimeOffset UpdatedAtUtc,
    IReadOnlyCollection<QuestionnaireAnswerResponse> Answers);
