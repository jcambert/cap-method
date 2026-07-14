using CapMethod.Saas.Domain.Beneficiaries;

namespace CapMethod.Saas.Application.Beneficiaries;

public sealed class CreateBeneficiaryUseCase
{
    private readonly IBeneficiaryRepository _repository;

    public CreateBeneficiaryUseCase(IBeneficiaryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateBeneficiaryResult> ExecuteAsync(
        CreateBeneficiaryCommand command,
        CancellationToken cancellationToken)
    {
        Beneficiary beneficiary = Beneficiary.Create(
            command.TenantId,
            command.FirstName,
            command.LastName,
            command.Email);

        await _repository.AddAsync(beneficiary, cancellationToken);

        return new CreateBeneficiaryResult(
            beneficiary.Id,
            beneficiary.TenantId,
            beneficiary.FirstName,
            beneficiary.LastName,
            beneficiary.Email,
            beneficiary.CreatedAtUtc);
    }
}

public sealed record CreateBeneficiaryCommand(
    Guid TenantId,
    string FirstName,
    string LastName,
    string? Email);

public sealed record CreateBeneficiaryResult(
    Guid BeneficiaryId,
    Guid TenantId,
    string FirstName,
    string LastName,
    string? Email,
    DateTimeOffset CreatedAtUtc);
