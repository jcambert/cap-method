using CapMethod.Saas.Application.Beneficiaries;
using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Domain.Beneficiaries;
using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Application.Tests.Sessions;

public sealed class CreateCapSessionUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_should_create_session_without_ai_by_default_when_beneficiary_exists()
    {
        InMemorySessionRepository sessionRepository = new();
        InMemoryBeneficiaryRepository beneficiaryRepository = new();
        Beneficiary beneficiary = Beneficiary.Create(Guid.NewGuid(), "Ada", "Lovelace", "ada@example.test");
        await beneficiaryRepository.AddAsync(beneficiary, CancellationToken.None);
        CreateCapSessionUseCase useCase = new(sessionRepository, beneficiaryRepository);
        CreateCapSessionCommand command = new(beneficiary.TenantId, beneficiary.Id, Guid.NewGuid(), EnableAi: false);

        CreateCapSessionResult result = await useCase.ExecuteAsync(command, CancellationToken.None);

        Assert.True(result.IsCreated);
        Assert.False(result.IsBeneficiaryNotFound);
        Assert.Equal(command.TenantId, result.TenantId);
        Assert.Equal(command.BeneficiaryId, result.BeneficiaryId);
        Assert.Equal(command.ConsultantId, result.ConsultantId);
        Assert.Equal("Draft", result.Status);
        Assert.False(result.IsAiEnabled);
        Assert.NotEqual(Guid.Empty, result.CapSessionId);
    }

    [Fact]
    public async Task ExecuteAsync_should_store_created_session_when_beneficiary_exists()
    {
        InMemorySessionRepository sessionRepository = new();
        InMemoryBeneficiaryRepository beneficiaryRepository = new();
        Beneficiary beneficiary = Beneficiary.Create(Guid.NewGuid(), "Ada", "Lovelace", null);
        await beneficiaryRepository.AddAsync(beneficiary, CancellationToken.None);
        CreateCapSessionUseCase useCase = new(sessionRepository, beneficiaryRepository);
        CreateCapSessionCommand command = new(beneficiary.TenantId, beneficiary.Id, Guid.NewGuid(), EnableAi: false);

        CreateCapSessionResult result = await useCase.ExecuteAsync(command, CancellationToken.None);
        Assert.NotNull(result.CapSessionId);
        CapSession? storedSession = await sessionRepository.GetByIdAsync(command.TenantId, result.CapSessionId.Value, CancellationToken.None);

        Assert.NotNull(storedSession);
        Assert.Equal(result.CapSessionId, storedSession.Id);
    }

    [Fact]
    public async Task ExecuteAsync_should_enable_ai_only_when_requested()
    {
        InMemorySessionRepository sessionRepository = new();
        InMemoryBeneficiaryRepository beneficiaryRepository = new();
        Beneficiary beneficiary = Beneficiary.Create(Guid.NewGuid(), "Ada", "Lovelace", null);
        await beneficiaryRepository.AddAsync(beneficiary, CancellationToken.None);
        CreateCapSessionUseCase useCase = new(sessionRepository, beneficiaryRepository);
        CreateCapSessionCommand command = new(beneficiary.TenantId, beneficiary.Id, Guid.NewGuid(), EnableAi: true);

        CreateCapSessionResult result = await useCase.ExecuteAsync(command, CancellationToken.None);

        Assert.True(result.IsAiEnabled);
    }

    [Fact]
    public async Task ExecuteAsync_should_not_create_session_when_beneficiary_does_not_exist()
    {
        InMemorySessionRepository sessionRepository = new();
        InMemoryBeneficiaryRepository beneficiaryRepository = new();
        CreateCapSessionUseCase useCase = new(sessionRepository, beneficiaryRepository);
        CreateCapSessionCommand command = new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), EnableAi: false);

        CreateCapSessionResult result = await useCase.ExecuteAsync(command, CancellationToken.None);

        Assert.False(result.IsCreated);
        Assert.True(result.IsBeneficiaryNotFound);
        Assert.Null(result.CapSessionId);
        Assert.Null(result.Status);
        Assert.Empty(await sessionRepository.ListByTenantAsync(command.TenantId, CancellationToken.None));
    }

    private sealed class InMemorySessionRepository : ICapSessionRepository
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

        public Task<IReadOnlyCollection<CapSession>> ListByTenantAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            CapSession[] sessions = _sessions.Values
                .Where(session => session.TenantId == tenantId)
                .ToArray();

            return Task.FromResult<IReadOnlyCollection<CapSession>>(sessions);
        }
    }

    private sealed class InMemoryBeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly Dictionary<Guid, Beneficiary> _beneficiaries = new();

        public Task AddAsync(Beneficiary beneficiary, CancellationToken cancellationToken)
        {
            _beneficiaries[beneficiary.Id] = beneficiary;
            return Task.CompletedTask;
        }

        public Task<Beneficiary?> GetByIdAsync(Guid tenantId, Guid beneficiaryId, CancellationToken cancellationToken)
        {
            _beneficiaries.TryGetValue(beneficiaryId, out Beneficiary? beneficiary);

            if (beneficiary is not null && beneficiary.TenantId != tenantId)
            {
                return Task.FromResult<Beneficiary?>(null);
            }

            return Task.FromResult(beneficiary);
        }
    }
}
