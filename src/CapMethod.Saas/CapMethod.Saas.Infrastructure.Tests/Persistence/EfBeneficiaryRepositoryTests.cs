using CapMethod.Saas.Domain.Beneficiaries;
using CapMethod.Saas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CapMethod.Saas.Infrastructure.Tests.Persistence;

public sealed class EfBeneficiaryRepositoryTests
{
    [Fact]
    public async Task AddAsync_should_store_beneficiary()
    {
        await using CapMethodSaasDbContext dbContext = CreateDbContext();
        EfBeneficiaryRepository repository = new(dbContext);
        Beneficiary beneficiary = Beneficiary.Create(Guid.NewGuid(), "Ada", "Lovelace", "ada@example.test");

        await repository.AddAsync(beneficiary, CancellationToken.None);

        Beneficiary? storedBeneficiary = await repository.GetByIdAsync(
            beneficiary.TenantId,
            beneficiary.Id,
            CancellationToken.None);

        Assert.NotNull(storedBeneficiary);
        Assert.Equal(beneficiary.Id, storedBeneficiary.Id);
        Assert.Equal("Ada", storedBeneficiary.FirstName);
    }

    [Fact]
    public async Task GetByIdAsync_should_return_null_for_cross_tenant_query()
    {
        await using CapMethodSaasDbContext dbContext = CreateDbContext();
        EfBeneficiaryRepository repository = new(dbContext);
        Beneficiary beneficiary = Beneficiary.Create(Guid.NewGuid(), "Ada", "Lovelace", null);

        await repository.AddAsync(beneficiary, CancellationToken.None);

        Beneficiary? storedBeneficiary = await repository.GetByIdAsync(
            Guid.NewGuid(),
            beneficiary.Id,
            CancellationToken.None);

        Assert.Null(storedBeneficiary);
    }

    private static CapMethodSaasDbContext CreateDbContext()
    {
        DbContextOptions<CapMethodSaasDbContext> options = new DbContextOptionsBuilder<CapMethodSaasDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        return new CapMethodSaasDbContext(options);
    }
}
