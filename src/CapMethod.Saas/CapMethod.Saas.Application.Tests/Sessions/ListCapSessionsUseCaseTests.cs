using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Application.Tests.Sessions;

public sealed class ListCapSessionsUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_should_return_sessions_for_requested_tenant()
    {
        InMemoryRepository repository = new();
        ListCapSessionsUseCase useCase = new(repository);
        Guid tenantId = Guid.NewGuid();
        CapSession first = CapSession.Create(tenantId, Guid.NewGuid(), Guid.NewGuid());
        CapSession second = CapSession.Create(tenantId, Guid.NewGuid(), Guid.NewGuid());

        await repository.AddAsync(first, CancellationToken.None);
        await repository.AddAsync(second, CancellationToken.None);

        IReadOnlyCollection<ListCapSessionResult> result = await useCase.ExecuteAsync(
            new ListCapSessionsQuery(tenantId),
            CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.All(result, session => Assert.Equal(tenantId, session.TenantId));
    }

    [Fact]
    public async Task ExecuteAsync_should_not_return_sessions_from_another_tenant()
    {
        InMemoryRepository repository = new();
        ListCapSessionsUseCase useCase = new(repository);
        Guid requestedTenantId = Guid.NewGuid();
        Guid otherTenantId = Guid.NewGuid();

        await repository.AddAsync(CapSession.Create(requestedTenantId, Guid.NewGuid(), Guid.NewGuid()), CancellationToken.None);
        await repository.AddAsync(CapSession.Create(otherTenantId, Guid.NewGuid(), Guid.NewGuid()), CancellationToken.None);

        IReadOnlyCollection<ListCapSessionResult> result = await useCase.ExecuteAsync(
            new ListCapSessionsQuery(requestedTenantId),
            CancellationToken.None);

        Assert.Single(result);
        Assert.Equal(requestedTenantId, result.Single().TenantId);
    }

    [Fact]
    public async Task ExecuteAsync_should_return_empty_collection_when_tenant_has_no_session()
    {
        InMemoryRepository repository = new();
        ListCapSessionsUseCase useCase = new(repository);

        IReadOnlyCollection<ListCapSessionResult> result = await useCase.ExecuteAsync(
            new ListCapSessionsQuery(Guid.NewGuid()),
            CancellationToken.None);

        Assert.Empty(result);
    }

    private sealed class InMemoryRepository : ICapSessionRepository
    {
        private readonly List<CapSession> _sessions = new();

        public Task AddAsync(CapSession session, CancellationToken cancellationToken)
        {
            _sessions.Add(session);
            return Task.CompletedTask;
        }

        public Task<CapSession?> GetByIdAsync(Guid tenantId, Guid capSessionId, CancellationToken cancellationToken)
        {
            CapSession? session = _sessions.SingleOrDefault(candidate =>
                candidate.TenantId == tenantId &&
                candidate.Id == capSessionId);

            return Task.FromResult(session);
        }

        public Task<IReadOnlyCollection<CapSession>> ListByTenantAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            CapSession[] sessions = _sessions
                .Where(session => session.TenantId == tenantId)
                .ToArray();

            return Task.FromResult<IReadOnlyCollection<CapSession>>(sessions);
        }
    }
}
