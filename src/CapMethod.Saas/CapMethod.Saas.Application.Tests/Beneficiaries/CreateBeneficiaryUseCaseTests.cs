using CapMethod.Saas.Application.Beneficiaries;
using CapMethod.Saas.Domain.Beneficiaries;

namespace CapMethod.Saas.Application.Tests.Beneficiaries;

public sealed class CreateBeneficiaryUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_should_create_and_store_beneficiary()
    {
        InMemoryBeneficiaryRepository repository = new();
        CreateBeneficiaryUseCase useCase = new(repository);
        CreateBeneficiaryCommand command = new(Guid.NewGuid(), "Ada", "Lovelace", "ada@example.test");

        CreateBeneficiaryResult result = await useCase.ExecuteAsync(command, CancellationToken.None);
        Beneficiary? storedBeneficiary = await repository.GetByIdAsync(command.TenantId, result.BeneficiaryId, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result.BeneficiaryId);
        Assert.Equal(command.TenantId, result.TenantId);
        Assert.Equal(command.FirstName, result.FirstName);
        Assert.Equal(command.LastName, result.LastName);
        Assert.Equal(command.Email, result.Email);
        Assert.NotNull(storedBeneficiary);
        Assert.Equal(result.BeneficiaryId, storedBeneficiary.Id);
    }

    [Fact]
    public async Task ExecuteAsync_should_trim_names()
    {
        InMemoryBeneficiaryRepository repository = new();
        CreateBeneficiaryUseCase useCase = new(repository);
        CreateBeneficiaryCommand command = new(Guid.NewGuid(), " Ada ", " Lovelace ", null);

        CreateBeneficiaryResult result = await useCase.ExecuteAsync(command, CancellationToken.None);

        Assert.Equal("Ada", result.FirstName);
        Assert.Equal("Lovelace", result.LastName);
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
