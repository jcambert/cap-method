using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Domain.Sessions;
using Microsoft.EntityFrameworkCore;

namespace CapMethod.Saas.Infrastructure.Persistence;

public sealed class EfCapSessionRepository : ICapSessionRepository
{
    private readonly CapMethodSaasDbContext _dbContext;

    public EfCapSessionRepository(CapMethodSaasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(CapSession session, CancellationToken cancellationToken)
    {
        await _dbContext.CapSessions.AddAsync(session, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<CapSession?> GetByIdAsync(Guid tenantId, Guid capSessionId, CancellationToken cancellationToken)
    {
        return _dbContext.CapSessions
            .SingleOrDefaultAsync(
                session => session.TenantId == tenantId && session.Id == capSessionId,
                cancellationToken);
    }

    public async Task<IReadOnlyCollection<CapSession>> ListByTenantAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        CapSession[] sessions = await _dbContext.CapSessions
            .Where(session => session.TenantId == tenantId)
            .OrderByDescending(session => session.CreatedAtUtc)
            .ToArrayAsync(cancellationToken);

        return sessions;
    }
}
