namespace CapMethod.Saas.Application.Sessions;

public sealed class ListCapSessionsUseCase
{
    private readonly ICapSessionRepository _repository;

    public ListCapSessionsUseCase(ICapSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<ListCapSessionResult>> ExecuteAsync(
        ListCapSessionsQuery query,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Domain.Sessions.CapSession> sessions = await _repository.ListByTenantAsync(
            query.TenantId,
            cancellationToken);

        return sessions
            .OrderByDescending(session => session.CreatedAtUtc)
            .Select(session => new ListCapSessionResult(
                session.Id,
                session.TenantId,
                session.BeneficiaryId,
                session.ConsultantId,
                session.Status.Code,
                session.IsAiEnabled,
                session.CreatedAtUtc))
            .ToArray();
    }
}

public sealed record ListCapSessionsQuery(Guid TenantId);

public sealed record ListCapSessionResult(
    Guid CapSessionId,
    Guid TenantId,
    Guid BeneficiaryId,
    Guid ConsultantId,
    string Status,
    bool IsAiEnabled,
    DateTimeOffset CreatedAtUtc);
