using CapMethod.Saas.Server.Analysis;
using CapMethod.Saas.Server.Questionnaires;
using CapMethod.Saas.Server.Synthesis;
using CapMethod.Saas.Shared.Synthesis;

namespace CapMethod.Saas.Compatibility.Tests;

public sealed class EditableSynthesisStoreTests
{
    [Fact]
    public void GetOrCreate_builds_an_editable_draft_from_structured_analysis()
    {
        EditableSynthesisStore store = CreateStore();

        SynthesisResponse result = store.GetOrCreate(Guid.NewGuid(), Guid.NewGuid());

        Assert.False(result.IsValidated);
        Assert.Contains("Synthèse du bilan de compétences", result.Content);
        Assert.Contains("validation humaine", result.Content, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Save_updates_then_validates_with_consultant_trace()
    {
        Guid tenantId = Guid.NewGuid();
        Guid beneficiaryId = Guid.NewGuid();
        Guid consultantId = Guid.NewGuid();
        EditableSynthesisStore store = CreateStore();

        SynthesisResponse draft = store.Save(
            tenantId,
            beneficiaryId,
            consultantId,
            new SaveSynthesisRequest("Contenu relu par le consultant.", Validate: false));
        SynthesisResponse validated = store.Save(
            tenantId,
            beneficiaryId,
            consultantId,
            new SaveSynthesisRequest("Contenu final validé.", Validate: true));

        Assert.False(draft.IsValidated);
        Assert.True(validated.IsValidated);
        Assert.Equal("Contenu final validé.", validated.Content);
        Assert.Equal(consultantId, validated.ValidatedByUserId);
        Assert.NotNull(validated.ValidatedAtUtc);
    }

    [Fact]
    public void Save_rejects_modification_after_human_validation()
    {
        Guid tenantId = Guid.NewGuid();
        Guid beneficiaryId = Guid.NewGuid();
        Guid consultantId = Guid.NewGuid();
        EditableSynthesisStore store = CreateStore();
        store.Save(
            tenantId,
            beneficiaryId,
            consultantId,
            new SaveSynthesisRequest("Version validée.", Validate: true));

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => store.Save(
            tenantId,
            beneficiaryId,
            consultantId,
            new SaveSynthesisRequest("Modification interdite.", Validate: false)));

        Assert.Contains("cannot be modified", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Store_is_isolated_by_tenant_and_beneficiary()
    {
        Guid tenantId = Guid.NewGuid();
        Guid beneficiaryId = Guid.NewGuid();
        EditableSynthesisStore store = CreateStore();
        store.Save(
            tenantId,
            beneficiaryId,
            Guid.NewGuid(),
            new SaveSynthesisRequest("Synthèse privée.", Validate: false));

        SynthesisResponse otherTenant = store.GetOrCreate(Guid.NewGuid(), beneficiaryId);
        SynthesisResponse otherBeneficiary = store.GetOrCreate(tenantId, Guid.NewGuid());

        Assert.DoesNotContain("Synthèse privée.", otherTenant.Content);
        Assert.DoesNotContain("Synthèse privée.", otherBeneficiary.Content);
    }

    private static EditableSynthesisStore CreateStore()
    {
        BeneficiaryQuestionnaireStore questionnaireStore = new();
        StructuredAnalysisService analysisService = new(questionnaireStore);
        return new EditableSynthesisStore(analysisService);
    }
}
