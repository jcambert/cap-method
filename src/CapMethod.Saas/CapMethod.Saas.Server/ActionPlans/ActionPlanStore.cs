using System.Collections.Concurrent;
using CapMethod.Saas.Shared.ActionPlans;

namespace CapMethod.Saas.Server.ActionPlans;

public sealed class ActionPlanStore
{
    private const int MaximumTitleLength = 180;
    private const int MaximumDescriptionLength = 2_000;
    private const int MaximumCategoryLength = 120;
    private const int MaximumPriorityLength = 40;
    private const int MaximumItems = 20;

    private readonly ConcurrentDictionary<ActionPlanKey, StoredActionPlan> _plans = new();

    public ActionPlanResponse GetOrCreate(Guid tenantId, Guid beneficiaryId)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);
        ActionPlanKey key = new(tenantId, beneficiaryId);
        StoredActionPlan plan = _plans.GetOrAdd(key, _ => CreateInitialPlan());
        return Map(key, plan);
    }

    public ActionPlanResponse Save(
        Guid tenantId,
        Guid beneficiaryId,
        Guid consultantUserId,
        SaveActionPlanRequest request)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);

        if (consultantUserId == Guid.Empty)
        {
            throw new ArgumentException("ConsultantUserId is required.", nameof(consultantUserId));
        }

        if (request.Items.Count > MaximumItems)
        {
            throw new ArgumentException($"An action plan cannot contain more than {MaximumItems} items.", nameof(request));
        }

        ActionPlanKey key = new(tenantId, beneficiaryId);
        StoredActionPlan current = _plans.GetOrAdd(key, _ => CreateInitialPlan());

        if (current.IsValidated)
        {
            throw new InvalidOperationException("A validated action plan cannot be modified.");
        }

        DateTimeOffset now = DateTimeOffset.UtcNow;
        StoredActionPlanItem[] items = request.Items
            .Select(item => MapRequestItem(item, now))
            .ToArray();

        if (request.Validate && items.Length == 0)
        {
            throw new ArgumentException("An action plan must contain at least one item before validation.", nameof(request));
        }

        StoredActionPlan updated = current with
        {
            Items = items,
            IsValidated = request.Validate,
            UpdatedAtUtc = now,
            ValidatedAtUtc = request.Validate ? now : null,
            ValidatedByUserId = request.Validate ? consultantUserId : null
        };

        _plans[key] = updated;
        return Map(key, updated);
    }

    public ActionPlanResponse CompleteItem(Guid tenantId, Guid beneficiaryId, Guid itemId)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);

        if (itemId == Guid.Empty)
        {
            throw new ArgumentException("ItemId is required.", nameof(itemId));
        }

        ActionPlanKey key = new(tenantId, beneficiaryId);
        StoredActionPlan current = _plans.GetOrAdd(key, _ => CreateInitialPlan());
        DateTimeOffset now = DateTimeOffset.UtcNow;
        bool found = false;
        StoredActionPlanItem[] items = current.Items
            .Select(item =>
            {
                if (item.ItemId != itemId)
                {
                    return item;
                }

                found = true;
                return item with
                {
                    Status = "Completed",
                    UpdatedAtUtc = now,
                    CompletedAtUtc = now
                };
            })
            .ToArray();

        if (!found)
        {
            throw new KeyNotFoundException($"Action plan item '{itemId}' was not found.");
        }

        StoredActionPlan updated = current with
        {
            Items = items,
            UpdatedAtUtc = now
        };

        _plans[key] = updated;
        return Map(key, updated);
    }

    private static StoredActionPlan CreateInitialPlan()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        return new StoredActionPlan([], false, now, now, null, null);
    }

    private static StoredActionPlanItem MapRequestItem(SaveActionPlanItemRequest request, DateTimeOffset now)
    {
        string title = ValidateText(request.Title, nameof(request.Title), MaximumTitleLength, required: true);
        string description = ValidateText(request.Description, nameof(request.Description), MaximumDescriptionLength, required: false);
        string category = ValidateText(request.Category, nameof(request.Category), MaximumCategoryLength, required: true);
        string priority = ValidateText(request.Priority, nameof(request.Priority), MaximumPriorityLength, required: true);

        return new StoredActionPlanItem(
            request.ItemId.GetValueOrDefault(Guid.NewGuid()),
            title,
            description,
            category,
            priority,
            "Open",
            request.DueDate,
            now,
            now,
            null);
    }

    private static string ValidateText(string? value, string parameterName, int maximumLength, bool required)
    {
        string text = value?.Trim() ?? string.Empty;

        if (required && string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException($"{parameterName} is required.", parameterName);
        }

        if (text.Length > maximumLength)
        {
            throw new ArgumentException($"{parameterName} exceeds {maximumLength} characters.", parameterName);
        }

        return text;
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

    private static ActionPlanResponse Map(ActionPlanKey key, StoredActionPlan plan)
    {
        ActionPlanItemResponse[] items = plan.Items
            .OrderBy(item => item.CreatedAtUtc)
            .Select(MapItem)
            .ToArray();

        return new ActionPlanResponse(
            key.TenantId,
            key.BeneficiaryId,
            plan.IsValidated,
            plan.CreatedAtUtc,
            plan.UpdatedAtUtc,
            plan.ValidatedAtUtc,
            plan.ValidatedByUserId,
            items);
    }

    private static ActionPlanItemResponse MapItem(StoredActionPlanItem item)
    {
        return new ActionPlanItemResponse(
            item.ItemId,
            item.Title,
            item.Description,
            item.Category,
            item.Priority,
            item.Status,
            item.DueDate,
            item.CreatedAtUtc,
            item.UpdatedAtUtc,
            item.CompletedAtUtc);
    }

    private sealed record ActionPlanKey(Guid TenantId, Guid BeneficiaryId);

    private sealed record StoredActionPlan(
        IReadOnlyCollection<StoredActionPlanItem> Items,
        bool IsValidated,
        DateTimeOffset CreatedAtUtc,
        DateTimeOffset UpdatedAtUtc,
        DateTimeOffset? ValidatedAtUtc,
        Guid? ValidatedByUserId);

    private sealed record StoredActionPlanItem(
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
}
