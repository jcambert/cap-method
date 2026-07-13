using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Application.Tests.Sessions;

public sealed class CreateCapSessionUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_should_create_session_without_ai_by_default()
    {
        InMemoryRepository repository = new();
        CreateCapSessionUseCase useCase = new(repository);
        CreateCapSessionCommand command = new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), EnableAi: false);

        CreateCapSessionResult result = await useCase.ExecuteAsync(command, CancellationToken.None);

        Assert.Equal(command.TenantId, result.TenantId);
        Assert.Equal(command.BeneficiaryId, result.BeneficiaryId);
        Assert.Equal(command.ConsultantId, result.ConsultantId);
        Assert.Equal("Draft", result.Status);
        Assert.False(result.IsAiEnabled);
        Assert.NotEqual(Guid.Empty, result.CapSessionId);
    }

    [Fact]
    public async Task ExecuteAsync_should_store_created_session()
    {
        InMemoryRepository repository = new();
        CreateCapSessionUseCase useCase = new(repository);
        CreateCapSessionCommand command = new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), EnableAi: false);

        CreateCapSessionResult result = await useCase.ExecuteAsync(command, CancellationToken.None);
        CapSession? storedSession = await repository.GetByIdAsync(command.TenantId, result.CapSessionId, CancellationToken.None);

        Assert.NotNull(storedSession);
        Assert.Equal(result.CapSessionId, storedSession.Id);
    }

    [Fact]
    public async Task ExecuteAsync_should_enable_ai_only_when_requested()
    {
        InMemoryRepository repository = new();
        CreateCapSessionUseCase useCase = new(repository);
        CreateCapSessionCommand command = new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), EnableAi: true);

        CreateCapSessionResult result = await useCase.ExecuteAsync(command, CancellationToken.None);

        Assert.True(result.IsAiEnabled);
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
