using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Application.Sessions;

public sealed class GetCapSessionUseCase
{
    private readonly ICapSessionRepository _repository;

    public GetCapSessionUseCase(ICapSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetCapSessionResult?> ExecuteAsync(
        GetCapSessionQuery query,
        CancellationToken cancellationToken)
    {
        CapSession? session = await _repository.GetByIdAsync(
            query.TenantId,
            query.CapSessionId,
            cancellationToken);

        if (session is null)
        {
            return null;
        }

        return new GetCapSessionResult(
            session.Id,
            session.TenantId,
            session.BeneficiaryId,
            session.ConsultantId,
            session.Status.Code,
            session.IsAiEnabled,
            session.CreatedAtUtc);
    }
}

public sealed record GetCapSessionQuery(
    Guid TenantId,
    Guid CapSessionId);

public sealed record GetCapSessionResult(
    Guid CapSessionId,
    Guid TenantId,
    Guid BeneficiaryId,
    Guid ConsultantId,
    string Status,
    bool IsAiEnabled,
    DateTimeOffset CreatedAtUtc);
