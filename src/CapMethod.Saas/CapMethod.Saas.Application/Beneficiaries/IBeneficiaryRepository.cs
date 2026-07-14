using CapMethod.Saas.Domain.Beneficiaries;

namespace CapMethod.Saas.Application.Beneficiaries;

public interface IBeneficiaryRepository
{
    Task AddAsync(Beneficiary beneficiary, CancellationToken cancellationToken);

    Task<Beneficiary?> GetByIdAsync(Guid tenantId, Guid beneficiaryId, CancellationToken cancellationToken);
}
