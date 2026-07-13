using System.Collections.Concurrent;
using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Infrastructure.Sessions;

public sealed class InMemoryCapSessionRepository : ICapSessionRepository
{
    private readonly ConcurrentDictionary<string, CapSession> _sessions = new();

    public Task AddAsync(CapSession session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _sessions[BuildKey(session.TenantId, session.Id)] = session;
        return Task.CompletedTask;
    }

    public Task<CapSession?> GetByIdAsync(Guid tenantId, Guid capSessionId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _sessions.TryGetValue(BuildKey(tenantId, capSessionId), out CapSession? session);
        return Task.FromResult(session);
    }

    public Task<IReadOnlyCollection<CapSession>> ListByTenantAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        CapSession[] sessions = _sessions.Values
            .Where(session => session.TenantId == tenantId)
            .OrderByDescending(session => session.CreatedAtUtc)
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<CapSession>>(sessions);
    }

    private static string BuildKey(Guid tenantId, Guid capSessionId)
    {
        return $"{tenantId:N}:{capSessionId:N}";
    }
}
