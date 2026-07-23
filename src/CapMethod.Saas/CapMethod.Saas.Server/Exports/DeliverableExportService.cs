using System.Text;
using CapMethod.Saas.Shared.ActionPlans;
using CapMethod.Saas.Shared.Synthesis;

namespace CapMethod.Saas.Server.Exports;

public sealed class DeliverableExportService
{
    public DeliverableExport Build(Guid tenantId, Guid beneficiaryId, SynthesisResponse synthesis, ActionPlanResponse actionPlan)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);

        if (synthesis.TenantId != tenantId || synthesis.BeneficiaryId != beneficiaryId)
        {
            throw new InvalidOperationException("The synthesis does not belong to the requested tenant and beneficiary.");
        }

        if (actionPlan.TenantId != tenantId || actionPlan.BeneficiaryId != beneficiaryId)
        {
            throw new InvalidOperationException("The action plan does not belong to the requested tenant and beneficiary.");
        }

        if (!synthesis.IsValidated)
        {
            throw new InvalidOperationException("The synthesis must be validated before export.");
        }

        if (!actionPlan.IsValidated)
        {
            throw new InvalidOperationException("The action plan must be validated before export.");
        }

        if (string.IsNullOrWhiteSpace(synthesis.Content))
        {
            throw new InvalidOperationException("The synthesis content is required before export.");
        }

        if (actionPlan.Items.Count == 0)
        {
            throw new InvalidOperationException("The action plan must contain at least one action before export.");
        }

        DateTimeOffset generatedAtUtc = DateTimeOffset.UtcNow;
        string content = BuildMarkdown(beneficiaryId, generatedAtUtc, synthesis, actionPlan);
        string fileName = $"bilan-competences-{beneficiaryId:N}-{generatedAtUtc:yyyyMMddHHmmss}.md";

        return new DeliverableExport(fileName, "text/markdown; charset=utf-8", Encoding.UTF8.GetBytes(content), generatedAtUtc);
    }

    private static string BuildMarkdown(
        Guid beneficiaryId,
        DateTimeOffset generatedAtUtc,
        SynthesisResponse synthesis,
        ActionPlanResponse actionPlan)
    {
        StringBuilder builder = new();
        builder.AppendLine("# Livrable de bilan de compétences");
        builder.AppendLine();
        builder.AppendLine($"- Bénéficiaire : `{beneficiaryId}`");
        builder.AppendLine($"- Généré le : {generatedAtUtc:O}");
        builder.AppendLine($"- Synthèse validée le : {synthesis.ValidatedAtUtc:O}");
        builder.AppendLine($"- Plan d'action validé le : {actionPlan.ValidatedAtUtc:O}");
        builder.AppendLine();
        builder.AppendLine("---");
        builder.AppendLine();
        builder.AppendLine(synthesis.Content.Trim());
        builder.AppendLine();
        builder.AppendLine("---");
        builder.AppendLine();
        builder.AppendLine("# Plan d'action");
        builder.AppendLine();

        foreach (ActionPlanItemResponse item in actionPlan.Items.OrderBy(item => item.DueDate).ThenBy(item => item.Title))
        {
            builder.AppendLine($"## {item.Title}");
            builder.AppendLine();
            builder.AppendLine($"- Catégorie : {item.Category}");
            builder.AppendLine($"- Priorité : {item.Priority}");
            builder.AppendLine($"- Statut : {item.Status}");
            builder.AppendLine($"- Échéance : {(item.DueDate.HasValue ? item.DueDate.Value.ToString("yyyy-MM-dd") : "Non définie")}");

            if (!string.IsNullOrWhiteSpace(item.Description))
            {
                builder.AppendLine();
                builder.AppendLine(item.Description.Trim());
            }

            builder.AppendLine();
        }

        return builder.ToString().TrimEnd();
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
}

public sealed record DeliverableExport(
    string FileName,
    string ContentType,
    byte[] Content,
    DateTimeOffset GeneratedAtUtc);