using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Application.Tests.Sessions;

public sealed class GetCapSessionUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_should_return_existing_session_for_same_tenant()
    {
        InMemoryRepository repository = new();
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        await repository.AddAsync(session, CancellationToken.None);
        GetCapSessionUseCase useCase = new(repository);
        GetCapSessionQuery query = new(session.TenantId, session.Id);

        GetCapSessionResult? result = await useCase.ExecuteAsync(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(session.Id, result.CapSessionId);
        Assert.Equal(session.TenantId, result.TenantId);
        Assert.Equal(session.BeneficiaryId, result.BeneficiaryId);
        Assert.Equal(session.ConsultantId, result.ConsultantId);
        Assert.Equal("Draft", result.Status);
        Assert.False(result.IsAiEnabled);
    }

    [Fact]
    public async Task ExecuteAsync_should_return_null_when_session_does_not_exist()
    {
        InMemoryRepository repository = new();
        GetCapSessionUseCase useCase = new(repository);
        GetCapSessionQuery query = new(Guid.NewGuid(), Guid.NewGuid());

        GetCapSessionResult? result = await useCase.ExecuteAsync(query, CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task ExecuteAsync_should_return_null_when_tenant_does_not_match()
    {
        InMemoryRepository repository = new();
        CapSession session = CapSession.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        await repository.AddAsync(session, CancellationToken.None);
        GetCapSessionUseCase useCase = new(repository);
        GetCapSessionQuery query = new(Guid.NewGuid(), session.Id);

        GetCapSessionResult? result = await useCase.ExecuteAsync(query, CancellationToken.None);

        Assert.Null(result);
    }

    private sealed class InMemoryRepository : ICapSessionRepository
    {
        private readonly Dictionary<Guid, CapSession> _sessions = new();

        public Task AddAsync(CapSession session, CancellationToken cancellationToken)
        {
            _sessions[session.Id] = session;
            return Task.CompletedTask;
        }

        public Task<CapSession?> GetByIdAsync(Guid tenantId, Guid capSessionId, CancellationToken cancellationToken)
        {
            _sessions.TryGetValue(capSessionId, out CapSession? session);

            if (session is not null && session.TenantId != tenantId)
            {
                return Task.FromResult<CapSession?>(null);
            }

            return Task.FromResult(session);
        }
    }
}
