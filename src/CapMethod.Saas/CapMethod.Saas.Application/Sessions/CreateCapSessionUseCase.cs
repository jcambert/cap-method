using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Application.Sessions;

public sealed class CreateCapSessionUseCase
{
    private readonly ICapSessionRepository _repository;

    public CreateCapSessionUseCase(ICapSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateCapSessionResult> ExecuteAsync(
        CreateCapSessionCommand command,
        CancellationToken cancellationToken)
    {
        CapSession session = CapSession.Create(
            command.TenantId,
            command.BeneficiaryId,
            command.ConsultantId);

        if (command.EnableAi)
        {
            session.EnableAi();
        }

        await _repository.AddAsync(session, cancellationToken);

        return new CreateCapSessionResult(
            session.Id,
            session.TenantId,
            session.BeneficiaryId,
            session.ConsultantId,
            session.Status.Code,
            session.IsAiEnabled,
            session.CreatedAtUtc);
    }
}

public sealed record CreateCapSessionCommand(
    Guid TenantId,
    Guid BeneficiaryId,
    Guid ConsultantId,
    bool EnableAi);

public sealed record CreateCapSessionResult(
    Guid CapSessionId,
    Guid TenantId,
    Guid BeneficiaryId,
    Guid ConsultantId,
    string Status,
    bool IsAiEnabled,
    DateTimeOffset CreatedAtUtc);
