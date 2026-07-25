using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace CapMethod.Saas.Infrastructure.Persistence;

public interface IOperationalSnapshotStore
{
    string? Read(Guid tenantId, Guid beneficiaryId, string documentType, string documentKey = "default");

    void Write(Guid tenantId, Guid beneficiaryId, string documentType, string documentKey, string payloadJson);
}

public sealed class InMemoryOperationalSnapshotStore : IOperationalSnapshotStore
{
    private readonly ConcurrentDictionary<SnapshotKey, string> _items = new();

    public string? Read(Guid tenantId, Guid beneficiaryId, string documentType, string documentKey = "default")
    {
        _items.TryGetValue(new SnapshotKey(tenantId, beneficiaryId, documentType, documentKey), out string? payload);
        return payload;
    }

    public void Write(Guid tenantId, Guid beneficiaryId, string documentType, string documentKey, string payloadJson)
    {
        _items[new SnapshotKey(tenantId, beneficiaryId, documentType, documentKey)] = payloadJson;
    }

    private sealed record SnapshotKey(Guid TenantId, Guid BeneficiaryId, string DocumentType, string DocumentKey);
}

public sealed class EfOperationalSnapshotStore : IOperationalSnapshotStore
{
    private readonly IDbContextFactory<CapMethodSaasDbContext> _dbContextFactory;

    public EfOperationalSnapshotStore(IDbContextFactory<CapMethodSaasDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public string? Read(Guid tenantId, Guid beneficiaryId, string documentType, string documentKey = "default")
    {
        using CapMethodSaasDbContext dbContext = _dbContextFactory.CreateDbContext();
        return dbContext.OperationalSnapshots
            .AsNoTracking()
            .Where(item => item.TenantId == tenantId &&
                           item.BeneficiaryId == beneficiaryId &&
                           item.DocumentType == documentType &&
                           item.DocumentKey == documentKey)
            .Select(item => item.PayloadJson)
            .SingleOrDefault();
    }

    public void Write(Guid tenantId, Guid beneficiaryId, string documentType, string documentKey, string payloadJson)
    {
        using CapMethodSaasDbContext dbContext = _dbContextFactory.CreateDbContext();
        OperationalSnapshot? existing = dbContext.OperationalSnapshots.SingleOrDefault(item =>
            item.TenantId == tenantId &&
            item.BeneficiaryId == beneficiaryId &&
            item.DocumentType == documentType &&
            item.DocumentKey == documentKey);

        if (existing is null)
        {
            dbContext.OperationalSnapshots.Add(new OperationalSnapshot
            {
                TenantId = tenantId,
                BeneficiaryId = beneficiaryId,
                DocumentType = documentType,
                DocumentKey = documentKey,
                PayloadJson = payloadJson,
                UpdatedAtUtc = DateTimeOffset.UtcNow
            });
        }
        else
        {
            existing.PayloadJson = payloadJson;
            existing.UpdatedAtUtc = DateTimeOffset.UtcNow;
        }

        dbContext.SaveChanges();
    }
}
