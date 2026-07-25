using CapMethod.Saas.Infrastructure.Persistence;
using Xunit;

namespace CapMethod.Saas.Infrastructure.Tests.Persistence;

public sealed class OperationalSnapshotStoreTests
{
    [Fact]
    public void In_memory_store_persists_and_isolates_snapshots_by_tenant()
    {
        InMemoryOperationalSnapshotStore store = new();
        Guid beneficiaryId = Guid.NewGuid();
        Guid tenantA = Guid.NewGuid();
        Guid tenantB = Guid.NewGuid();

        store.Write(tenantA, beneficiaryId, "synthesis", "default", "{\"value\":\"tenant-a\"}");
        store.Write(tenantB, beneficiaryId, "synthesis", "default", "{\"value\":\"tenant-b\"}");

        Assert.Equal("{\"value\":\"tenant-a\"}", store.Read(tenantA, beneficiaryId, "synthesis"));
        Assert.Equal("{\"value\":\"tenant-b\"}", store.Read(tenantB, beneficiaryId, "synthesis"));
        Assert.Null(store.Read(tenantA, Guid.NewGuid(), "synthesis"));
    }

    [Fact]
    public void Store_keeps_distinct_document_keys()
    {
        InMemoryOperationalSnapshotStore store = new();
        Guid tenantId = Guid.NewGuid();
        Guid beneficiaryId = Guid.NewGuid();

        store.Write(tenantId, beneficiaryId, "questionnaire", "career", "{\"submitted\":true}");
        store.Write(tenantId, beneficiaryId, "questionnaire", "values", "{\"submitted\":false}");

        Assert.Equal("{\"submitted\":true}", store.Read(tenantId, beneficiaryId, "questionnaire", "career"));
        Assert.Equal("{\"submitted\":false}", store.Read(tenantId, beneficiaryId, "questionnaire", "values"));
    }
}
