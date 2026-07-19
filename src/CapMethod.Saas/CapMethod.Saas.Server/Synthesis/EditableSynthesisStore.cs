using System.Collections.Concurrent;
using System.Text;
using CapMethod.Saas.Server.Analysis;
using CapMethod.Saas.Shared.Analysis;
using CapMethod.Saas.Shared.Synthesis;

namespace CapMethod.Saas.Server.Synthesis;

public sealed class EditableSynthesisStore
{
    private const int MaximumContentLength = 30_000;
    private readonly ConcurrentDictionary<SynthesisKey, StoredSynthesis> _items = new();
    private readonly StructuredAnalysisService _analysisService;

    public EditableSynthesisStore(StructuredAnalysisService analysisService)
    {
        _analysisService = analysisService;
    }

    public SynthesisResponse GetOrCreate(Guid tenantId, Guid beneficiaryId)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);
        SynthesisKey key = new(tenantId, beneficiaryId);
        StoredSynthesis stored = _items.GetOrAdd(key, _ => CreateInitialSynthesis(tenantId, beneficiaryId));
        return Map(key, stored);
    }

    public SynthesisResponse Save(
        Guid tenantId,
        Guid beneficiaryId,
        Guid consultantUserId,
        SaveSynthesisRequest request)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);

        if (consultantUserId == Guid.Empty)
        {
            throw new ArgumentException("ConsultantUserId is required.", nameof(consultantUserId));
        }

        string content = request.Content?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Synthesis content is required.", nameof(request));
        }

        if (content.Length > MaximumContentLength)
        {
            throw new ArgumentException($"Synthesis content exceeds {MaximumContentLength} characters.", nameof(request));
        }

        SynthesisKey key = new(tenantId, beneficiaryId);
        StoredSynthesis current = _items.GetOrAdd(key, _ => CreateInitialSynthesis(tenantId, beneficiaryId));

        if (current.IsValidated)
        {
            throw new InvalidOperationException("A validated synthesis cannot be modified.");
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        StoredSynthesis updated = current with
        {
            Content = content,
            IsValidated = request.Validate,
            UpdatedAtUtc = now,
            ValidatedAtUtc = request.Validate ? now : null,
            ValidatedByUserId = request.Validate ? consultantUserId : null
        };
        _items[key] = updated;
        return Map(key, updated);
    }

    private StoredSynthesis CreateInitialSynthesis(Guid tenantId, Guid beneficiaryId)
    {
        StructuredAnalysisResponse analysis = _analysisService.Generate(tenantId, beneficiaryId);
        DateTimeOffset now = DateTimeOffset.UtcNow;
        return new StoredSynthesis(BuildInitialContent(analysis), false, now, now, null, null);
    }

    private static string BuildInitialContent(StructuredAnalysisResponse analysis)
    {
        StringBuilder builder = new();
        builder.AppendLine("# Synthèse du bilan de compétences");
        builder.AppendLine();
        builder.AppendLine("## Éléments issus des questionnaires");
        builder.AppendLine($"- Questionnaires soumis : {analysis.SubmittedQuestionnaires}");
        builder.AppendLine($"- Réponses exploitables : {analysis.TotalAnswers}");
        builder.AppendLine($"- Complétude : {analysis.CompletionScore} %");
        builder.AppendLine();
        builder.AppendLine("## Thèmes dominants à valider avec le bénéficiaire");

        if (analysis.Keywords.Count == 0)
        {
            builder.AppendLine("Aucun thème dominant n'a encore été identifié.");
        }
        else
        {
            foreach (string keyword in analysis.Keywords)
            {
                builder.AppendLine($"- {keyword}");
            }
        }

        builder.AppendLine();
        builder.AppendLine("## Analyse du consultant");
        builder.AppendLine("À compléter et reformuler après entretien avec le bénéficiaire.");
        builder.AppendLine();
        builder.AppendLine("## Projet professionnel et points de vigilance");
        builder.AppendLine("À compléter par le consultant.");
        builder.AppendLine();
        builder.AppendLine("## Conclusion");
        builder.AppendLine("Ce document reste un brouillon tant que la validation humaine n'est pas enregistrée.");
        return builder.ToString().Trim();
    }

    private static void ValidateIdentifiers(Guid tenantId, Guid beneficiaryId)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId is required.", nameof(tenantId));
        }

        if (beneficiaryId == Guid.Empty)
        {
            throw new ArgumentException("BeneficiaryId is required.", nameof(beneficiaryId));
        }
    }

    private static SynthesisResponse Map(SynthesisKey key, StoredSynthesis stored)
    {
        return new SynthesisResponse(
            key.TenantId,
            key.BeneficiaryId,
            stored.Content,
            stored.IsValidated,
            stored.CreatedAtUtc,
            stored.UpdatedAtUtc,
            stored.ValidatedAtUtc,
            stored.ValidatedByUserId);
    }

    private sealed record SynthesisKey(Guid TenantId, Guid BeneficiaryId);

    private sealed record StoredSynthesis(
        string Content,
        bool IsValidated,
        DateTimeOffset CreatedAtUtc,
        DateTimeOffset UpdatedAtUtc,
        DateTimeOffset? ValidatedAtUtc,
        Guid? ValidatedByUserId);
}
