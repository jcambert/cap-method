using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Application.Sessions;

public interface ICapSessionRepository
{
    Task AddAsync(CapSession session, CancellationToken cancellationToken);

    Task<CapSession?> GetByIdAsync(Guid tenantId, Guid capSessionId, CancellationToken cancellationToken);
}
