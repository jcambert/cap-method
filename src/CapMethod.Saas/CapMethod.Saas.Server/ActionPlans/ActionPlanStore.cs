using System.Text.Json;
using CapMethod.Saas.Infrastructure.Persistence;
using CapMethod.Saas.Shared.ActionPlans;

namespace CapMethod.Saas.Server.ActionPlans;

public sealed class ActionPlanStore
{
    private const int MaximumTitleLength = 180;
    private const int MaximumDescriptionLength = 2_000;
    private const int MaximumCategoryLength = 120;
    private const int MaximumPriorityLength = 40;
    private const int MaximumItems = 20;
    private const string DocumentType = "action-plan";
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly IOperationalSnapshotStore _snapshots;

    public ActionPlanStore(IOperationalSnapshotStore snapshots)
    {
        _snapshots = snapshots;
    }

    public ActionPlanResponse GetOrCreate(Guid tenantId, Guid beneficiaryId)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);
        StoredActionPlan? plan = Read(tenantId, beneficiaryId);
        if (plan is null)
        {
            plan = CreateInitialPlan();
            Write(tenantId, beneficiaryId, plan);
        }

        return Map(tenantId, beneficiaryId, plan);
    }

    public ActionPlanResponse Save(Guid tenantId, Guid beneficiaryId, Guid consultantUserId, SaveActionPlanRequest request)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);
        if (consultantUserId == Guid.Empty) throw new ArgumentException("ConsultantUserId is required.", nameof(consultantUserId));
        if (request.Items.Count > MaximumItems) throw new ArgumentException($"An action plan cannot contain more than {MaximumItems} items.", nameof(request));

        StoredActionPlan current = Read(tenantId, beneficiaryId) ?? CreateInitialPlan();
        if (current.IsValidated) throw new InvalidOperationException("A validated action plan cannot be modified.");

        DateTimeOffset now = DateTimeOffset.UtcNow;
        StoredActionPlanItem[] items = request.Items.Select(item => MapRequestItem(item, now)).ToArray();
        if (request.Validate && items.Length == 0) throw new ArgumentException("An action plan must contain at least one item before validation.", nameof(request));

        StoredActionPlan updated = current with
        {
            Items = items,
            IsValidated = request.Validate,
            UpdatedAtUtc = now,
            ValidatedAtUtc = request.Validate ? now : null,
            ValidatedByUserId = request.Validate ? consultantUserId : null
        };
        Write(tenantId, beneficiaryId, updated);
        return Map(tenantId, beneficiaryId, updated);
    }

    public ActionPlanResponse CompleteItem(Guid tenantId, Guid beneficiaryId, Guid itemId)
    {
        ValidateIdentifiers(tenantId, beneficiaryId);
        if (itemId == Guid.Empty) throw new ArgumentException("ItemId is required.", nameof(itemId));

        StoredActionPlan current = Read(tenantId, beneficiaryId) ?? CreateInitialPlan();
        DateTimeOffset now = DateTimeOffset.UtcNow;
        bool found = false;
        StoredActionPlanItem[] items = current.Items.Select(item =>
        {
            if (item.ItemId != itemId) return item;
            found = true;
            return item with { Status = "Completed", UpdatedAtUtc = now, CompletedAtUtc = now };
        }).ToArray();

        if (!found) throw new KeyNotFoundException($"Action plan item '{itemId}' was not found.");
        StoredActionPlan updated = current with { Items = items, UpdatedAtUtc = now };
        Write(tenantId, beneficiaryId, updated);
        return Map(tenantId, beneficiaryId, updated);
    }

    private StoredActionPlan? Read(Guid tenantId, Guid beneficiaryId)
    {
        string? payload = _snapshots.Read(tenantId, beneficiaryId, DocumentType);
        return payload is null ? null : JsonSerializer.Deserialize<StoredActionPlan>(payload, JsonOptions);
    }

    private void Write(Guid tenantId, Guid beneficiaryId, StoredActionPlan plan) =>
        _snapshots.Write(tenantId, beneficiaryId, DocumentType, "default", JsonSerializer.Serialize(plan, JsonOptions));

    private static StoredActionPlan CreateInitialPlan()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        return new StoredActionPlan([], false, now, now, null, null);
    }

    private static StoredActionPlanItem MapRequestItem(SaveActionPlanItemRequest request, DateTimeOffset now) => new(
        request.ItemId.GetValueOrDefault(Guid.NewGuid()),
        ValidateText(request.Title, nameof(request.Title), MaximumTitleLength, true),
        ValidateText(request.Description, nameof(request.Description), MaximumDescriptionLength, false),
        ValidateText(request.Category, nameof(request.Category), MaximumCategoryLength, true),
        ValidateText(request.Priority, nameof(request.Priority), MaximumPriorityLength, true),
        "Open",
        request.DueDate,
        now,
        now,
        null);

    private static string ValidateText(string? value, string parameterName, int maximumLength, bool required)
    {
        string text = value?.Trim() ?? string.Empty;
        if (required && string.IsNullOrWhiteSpace(text)) throw new ArgumentException($"{parameterName} is required.", parameterName);
        if (text.Length > maximumLength) throw new ArgumentException($"{parameterName} exceeds {maximumLength} characters.", parameterName);
        return text;
    }

    private static void ValidateIdentifiers(Guid tenantId, Guid beneficiaryId)
    {
        if (tenantId == Guid.Empty) throw new ArgumentException("TenantId is required.", nameof(tenantId));
        if (beneficiaryId == Guid.Empty) throw new ArgumentException("BeneficiaryId is required.", nameof(beneficiaryId));
    }

    private static ActionPlanResponse Map(Guid tenantId, Guid beneficiaryId, StoredActionPlan plan) => new(
        tenantId,
        beneficiaryId,
        plan.IsValidated,
        plan.CreatedAtUtc,
        plan.UpdatedAtUtc,
        plan.ValidatedAtUtc,
        plan.ValidatedByUserId,
        plan.Items.OrderBy(item => item.CreatedAtUtc).Select(MapItem).ToArray());

    private static ActionPlanItemResponse MapItem(StoredActionPlanItem item) => new(item.ItemId, item.Title, item.Description, item.Category, item.Priority, item.Status, item.DueDate, item.CreatedAtUtc, item.UpdatedAtUtc, item.CompletedAtUtc);

    public sealed record StoredActionPlan(IReadOnlyCollection<StoredActionPlanItem> Items, bool IsValidated, DateTimeOffset CreatedAtUtc, DateTimeOffset UpdatedAtUtc, DateTimeOffset? ValidatedAtUtc, Guid? ValidatedByUserId);
    public sealed record StoredActionPlanItem(Guid ItemId, string Title, string Description, string Category, string Priority, string Status, DateOnly? DueDate, DateTimeOffset CreatedAtUtc, DateTimeOffset UpdatedAtUtc, DateTimeOffset? CompletedAtUtc);
}
