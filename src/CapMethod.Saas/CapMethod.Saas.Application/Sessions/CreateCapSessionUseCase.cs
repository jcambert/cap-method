using CapMethod.Saas.Application.Beneficiaries;
using CapMethod.Saas.Domain.Beneficiaries;
using CapMethod.Saas.Domain.Sessions;

namespace CapMethod.Saas.Application.Sessions;

public sealed class CreateCapSessionUseCase
{
    private readonly ICapSessionRepository _sessionRepository;
    private readonly IBeneficiaryRepository _beneficiaryRepository;

    public CreateCapSessionUseCase(
        ICapSessionRepository sessionRepository,
        IBeneficiaryRepository beneficiaryRepository)
    {
        _sessionRepository = sessionRepository;
        _beneficiaryRepository = beneficiaryRepository;
    }

    public async Task<CreateCapSessionResult> ExecuteAsync(
        CreateCapSessionCommand command,
        CancellationToken cancellationToken)
    {
        Beneficiary? beneficiary = await _beneficiaryRepository.GetByIdAsync(
            command.TenantId,
            command.BeneficiaryId,
            cancellationToken);

        if (beneficiary is null)
        {
            return CreateCapSessionResult.BeneficiaryNotFound(command.TenantId, command.BeneficiaryId);
        }

        CapSession session = CapSession.Create(
            command.TenantId,
            command.BeneficiaryId,
            command.ConsultantId);

        if (command.EnableAi)
        {
            session.EnableAi();
        }

        await _sessionRepository.AddAsync(session, cancellationToken);

        return CreateCapSessionResult.Created(
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
    Guid? CapSessionId,
    Guid TenantId,
    Guid BeneficiaryId,
    Guid? ConsultantId,
    string? Status,
    bool? IsAiEnabled,
    DateTimeOffset? CreatedAtUtc,
    bool IsCreated,
    bool IsBeneficiaryNotFound)
{
    public static CreateCapSessionResult Created(
        Guid capSessionId,
        Guid tenantId,
        Guid beneficiaryId,
        Guid consultantId,
        string status,
        bool isAiEnabled,
        DateTimeOffset createdAtUtc)
    {
        return new CreateCapSessionResult(
            capSessionId,
            tenantId,
            beneficiaryId,
            consultantId,
            status,
            isAiEnabled,
            createdAtUtc,
            IsCreated: true,
            IsBeneficiaryNotFound: false);
    }

    public static CreateCapSessionResult BeneficiaryNotFound(Guid tenantId, Guid beneficiaryId)
    {
        return new CreateCapSessionResult(
            CapSessionId: null,
            tenantId,
            beneficiaryId,
            ConsultantId: null,
            Status: null,
            IsAiEnabled: null,
            CreatedAtUtc: null,
            IsCreated: false,
            IsBeneficiaryNotFound: true);
    }
}
