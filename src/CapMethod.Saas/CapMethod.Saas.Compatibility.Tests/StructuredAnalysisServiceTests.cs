using CapMethod.Saas.Server.Analysis;
using CapMethod.Saas.Server.Questionnaires;
using CapMethod.Saas.Shared.Analysis;
using CapMethod.Saas.Shared.Questionnaires;

namespace CapMethod.Saas.Compatibility.Tests;

public sealed class StructuredAnalysisServiceTests
{
    [Fact]
    public void Generate_returns_empty_analysis_when_no_questionnaire_is_submitted()
    {
        BeneficiaryQuestionnaireStore store = new();
        StructuredAnalysisService service = new(store);

        StructuredAnalysisResponse result = service.Generate(Guid.NewGuid(), Guid.NewGuid());

        Assert.Equal(0, result.SubmittedQuestionnaires);
        Assert.Equal(0, result.TotalAnswers);
        Assert.Equal(0, result.CompletionScore);
        Assert.Empty(result.Keywords);
        Assert.Equal(3, result.Indicators.Count);
    }

    [Fact]
    public void Generate_builds_indicators_and_keywords_from_submitted_answers()
    {
        Guid tenantId = Guid.NewGuid();
        Guid beneficiaryId = Guid.NewGuid();
        BeneficiaryQuestionnaireStore store = new();
        store.Save(
            tenantId,
            beneficiaryId,
            "career-exploration",
            new SaveQuestionnaireAnswersRequest(
                [
                    new QuestionnaireAnswerRequest("career-highlights", "Pilotage projet transformation équipe"),
                    new QuestionnaireAnswerRequest("strengths", "Communication pilotage accompagnement"),
                    new QuestionnaireAnswerRequest("expectations", "Clarifier projet évolution professionnelle")
                ],
                Submit: true));
        StructuredAnalysisService service = new(store);

        StructuredAnalysisResponse result = service.Generate(tenantId, beneficiaryId);

        Assert.Equal(1, result.SubmittedQuestionnaires);
        Assert.Equal(3, result.TotalAnswers);
        Assert.Equal(50, result.CompletionScore);
        Assert.Contains("pilotage", result.Keywords);
        Assert.Contains(result.Indicators, indicator => indicator.IndicatorId == "response-depth");
    }

    [Fact]
    public void Generate_is_isolated_by_tenant_and_beneficiary()
    {
        Guid tenantId = Guid.NewGuid();
        Guid beneficiaryId = Guid.NewGuid();
        BeneficiaryQuestionnaireStore store = new();
        store.Save(
            tenantId,
            beneficiaryId,
            "career-exploration",
            new SaveQuestionnaireAnswersRequest(
                [
                    new QuestionnaireAnswerRequest("career-highlights", "Expérience significative"),
                    new QuestionnaireAnswerRequest("strengths", "Organisation"),
                    new QuestionnaireAnswerRequest("expectations", "Évolution")
                ],
                Submit: true));
        StructuredAnalysisService service = new(store);

        StructuredAnalysisResponse otherBeneficiary = service.Generate(tenantId, Guid.NewGuid());
        StructuredAnalysisResponse otherTenant = service.Generate(Guid.NewGuid(), beneficiaryId);

        Assert.Equal(0, otherBeneficiary.SubmittedQuestionnaires);
        Assert.Equal(0, otherTenant.SubmittedQuestionnaires);
    }
}
