namespace CapMethod.Saas.Shared.ActionPlans;

public sealed record ActionPlanItemResponse(
    Guid ItemId,
    string Title,
    string Description,
    string Category,
    string Priority,
    string Status,
    DateOnly? DueDate,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset UpdatedAtUtc,
    DateTimeOffset? CompletedAtUtc);

public sealed record ActionPlanResponse(
    Guid TenantId,
    Guid BeneficiaryId,
    bool IsValidated,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset UpdatedAtUtc,
    DateTimeOffset? ValidatedAtUtc,
    Guid? ValidatedByUserId,
    IReadOnlyCollection<ActionPlanItemResponse> Items);

public sealed record SaveActionPlanItemRequest(
    Guid? ItemId,
    string Title,
    string Description,
    string Category,
    string Priority,
    DateOnly? DueDate);

public sealed record SaveActionPlanRequest(
    IReadOnlyCollection<SaveActionPlanItemRequest> Items,
    bool Validate);
