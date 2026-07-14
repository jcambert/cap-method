using CapMethod.Saas.Domain.Sessions;
using CapMethod.Saas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CapMethod.Saas.Infrastructure.Tests.Persistence;

public sealed class EfCapSessionRepositoryTests
{
    [Fact]
    public async Task AddAsync_should_store_session()
    {
        await using CapMethodSaasDbContext dbContext = CreateDbContext();
        EfCapSessionRepository repository = new(dbContext);
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        await repository.AddAsync(session, CancellationToken.None);

        CapSession? storedSession = await repository.GetByIdAsync(
            session.TenantId,
            session.Id,
            CancellationToken.None);

        Assert.NotNull(storedSession);
        Assert.Equal(session.Id, storedSession.Id);
        Assert.Equal("Draft", storedSession.Status.Code);
    }

    [Fact]
    public async Task GetByIdAsync_should_return_null_for_cross_tenant_query()
    {
        await using CapMethodSaasDbContext dbContext = CreateDbContext();
        EfCapSessionRepository repository = new(dbContext);
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        await repository.AddAsync(session, CancellationToken.None);

        CapSession? storedSession = await repository.GetByIdAsync(
            Guid.NewGuid(),
            session.Id,
            CancellationToken.None);

        Assert.Null(storedSession);
    }

    [Fact]
    public async Task ListByTenantAsync_should_return_only_requested_tenant_sessions()
    {
        await using CapMethodSaasDbContext dbContext = CreateDbContext();
        EfCapSessionRepository repository = new(dbContext);
        Guid tenantId = Guid.NewGuid();
        CapSession firstSession = CapSession.Create(tenantId, Guid.NewGuid(), Guid.NewGuid());
        CapSession secondSession = CapSession.Create(tenantId, Guid.NewGuid(), Guid.NewGuid());
        CapSession otherTenantSession = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        await repository.AddAsync(firstSession, CancellationToken.None);
        await repository.AddAsync(secondSession, CancellationToken.None);
        await repository.AddAsync(otherTenantSession, CancellationToken.None);

        IReadOnlyCollection<CapSession> sessions = await repository.ListByTenantAsync(tenantId, CancellationToken.None);

        Assert.Equal(2, sessions.Count);
        Assert.All(sessions, session => Assert.Equal(tenantId, session.TenantId));
    }

    private static CapMethodSaasDbContext CreateDbContext()
    {
        DbContextOptions<CapMethodSaasDbContext> options = new DbContextOptionsBuilder<CapMethodSaasDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        return new CapMethodSaasDbContext(options);
    }
}
