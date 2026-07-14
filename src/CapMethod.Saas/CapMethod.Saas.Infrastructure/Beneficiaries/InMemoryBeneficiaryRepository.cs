using System.Collections.Concurrent;
using CapMethod.Saas.Application.Beneficiaries;
using CapMethod.Saas.Domain.Beneficiaries;

namespace CapMethod.Saas.Infrastructure.Beneficiaries;

public sealed class InMemoryBeneficiaryRepository : IBeneficiaryRepository
{
    private readonly ConcurrentDictionary<string, Beneficiary> _beneficiaries = new();

    public Task AddAsync(Beneficiary beneficiary, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _beneficiaries[BuildKey(beneficiary.TenantId, beneficiary.Id)] = beneficiary;
        return Task.CompletedTask;
    }

    public Task<Beneficiary?> GetByIdAsync(Guid tenantId, Guid beneficiaryId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _beneficiaries.TryGetValue(BuildKey(tenantId, beneficiaryId), out Beneficiary? beneficiary);
        return Task.FromResult(beneficiary);
    }

    private static string BuildKey(Guid tenantId, Guid beneficiaryId)
    {
        return $"{tenantId:N}:{beneficiaryId:N}";
    }
}
