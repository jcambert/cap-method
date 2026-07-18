namespace CapMethod.Saas.Domain.Workflow;

public sealed record CapWorkflowStep(
    string Code,
    string Label,
    int Order,
    bool IsRequired,
    bool RequiresAi)
{
    public static readonly CapWorkflowStep Intake = new(
        "Intake",
        "Cadrage du bilan",
        10,
        IsRequired: true,
        RequiresAi: false);

    public static readonly CapWorkflowStep Questionnaires = new(
        "Questionnaires",
        "Questionnaires bénéficiaire",
        20,
        IsRequired: true,
        RequiresAi: false);

    public static readonly CapWorkflowStep Responses = new(
        "Responses",
        "Réponses complétées",
        30,
        IsRequired: true,
        RequiresAi: false);

    public static readonly CapWorkflowStep StructuredAnalysis = new(
        "StructuredAnalysis",
        "Analyse structurée",
        40,
        IsRequired: true,
        RequiresAi: false);

    public static readonly CapWorkflowStep AiDraft = new(
        "AiDraft",
        "Brouillon IA optionnel",
        50,
        IsRequired: false,
        RequiresAi: true);

    public static readonly CapWorkflowStep ConsultantReview = new(
        "ConsultantReview",
        "Revue consultant",
        60,
        IsRequired: true,
        RequiresAi: false);

    public static readonly CapWorkflowStep Synthesis = new(
        "Synthesis",
        "Synthèse validée",
        70,
        IsRequired: true,
        RequiresAi: false);

    public static readonly CapWorkflowStep Delivery = new(
        "Delivery",
        "Livraison des livrables",
        80,
        IsRequired: true,
        RequiresAi: false);

    public static readonly CapWorkflowStep Archive = new(
        "Archive",
        "Archivage",
        90,
        IsRequired: false,
        RequiresAi: false);
}