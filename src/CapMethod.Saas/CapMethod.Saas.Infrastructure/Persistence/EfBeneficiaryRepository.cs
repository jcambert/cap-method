using CapMethod.Saas.Application.Beneficiaries;
using CapMethod.Saas.Domain.Beneficiaries;
using Microsoft.EntityFrameworkCore;

namespace CapMethod.Saas.Infrastructure.Persistence;

public sealed class EfBeneficiaryRepository : IBeneficiaryRepository
{
    private readonly CapMethodSaasDbContext _dbContext;

    public EfBeneficiaryRepository(CapMethodSaasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Beneficiary beneficiary, CancellationToken cancellationToken)
    {
        await _dbContext.Beneficiaries.AddAsync(beneficiary, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Beneficiary?> GetByIdAsync(Guid tenantId, Guid beneficiaryId, CancellationToken cancellationToken)
    {
        return _dbContext.Beneficiaries
            .SingleOrDefaultAsync(
                beneficiary => beneficiary.TenantId == tenantId && beneficiary.Id == beneficiaryId,
                cancellationToken);
    }
}
