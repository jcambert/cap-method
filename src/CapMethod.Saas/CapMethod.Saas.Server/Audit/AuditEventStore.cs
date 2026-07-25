using System.Collections.Concurrent;

namespace CapMethod.Saas.Server.Audit;

public sealed record AuditEvent(
    Guid EventId,
    Guid TenantId,
    Guid UserId,
    string Action,
    string Resource,
    int StatusCode,
    string CorrelationId,
    DateTimeOffset OccurredAtUtc);

public sealed class AuditEventStore
{
    private const int MaximumEventsPerTenant = 500;
    private readonly ConcurrentDictionary<Guid, ConcurrentQueue<AuditEvent>> _eventsByTenant = new();

    public void Append(AuditEvent auditEvent)
    {
        ConcurrentQueue<AuditEvent> events = _eventsByTenant.GetOrAdd(
            auditEvent.TenantId,
            static _ => new ConcurrentQueue<AuditEvent>());

        events.Enqueue(auditEvent);
        while (events.Count > MaximumEventsPerTenant)
        {
            events.TryDequeue(out _);
        }
    }

    public IReadOnlyCollection<AuditEvent> List(Guid tenantId, int maximumCount = 100)
    {
        int safeCount = Math.Clamp(maximumCount, 1, 500);
        return _eventsByTenant.TryGetValue(tenantId, out ConcurrentQueue<AuditEvent>? events)
            ? events.Reverse().Take(safeCount).ToArray()
            : [];
    }
}