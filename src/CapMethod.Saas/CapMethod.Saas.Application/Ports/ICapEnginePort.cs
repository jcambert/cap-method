namespace CapMethod.Saas.Application.Ports;

public interface ICapEnginePort
{
    Task<CapEngineResult<CapArtifactRef>> BuildResponseSessionAsync(
        BuildResponseSessionCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<CapArtifactRef>> GenerateAnalysisSnapshotAsync(
        GenerateAnalysisSnapshotCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<CapArtifactRef>> GenerateDeliverablePackageAsync(
        GenerateDeliverablePackageCommand command,
        CancellationToken cancellationToken);
}

public sealed record BuildResponseSessionCommand(
    Guid TenantId,
    Guid CapSessionId,
    Guid BeneficiaryId,
    Guid ConsultantId);

public sealed record GenerateAnalysisSnapshotCommand(
    Guid TenantId,
    Guid CapSessionId,
    CapArtifactRef ResponseSession);

public sealed record GenerateDeliverablePackageCommand(
    Guid TenantId,
    Guid CapSessionId,
    CapArtifactRef FinalSynthesis,
    CapArtifactRef ActionPlan);

public sealed record CapArtifactRef(
    Guid TenantId,
    Guid CapSessionId,
    string ArtifactType,
    string StorageKey,
    DateTimeOffset GeneratedAtUtc);

public sealed record CapEngineResult<T>(
    bool Succeeded,
    T? Value,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings)
{
    public static CapEngineResult<T> Success(T value)
    {
        return new CapEngineResult<T>(true, value, Array.Empty<string>(), Array.Empty<string>());
    }

    public static CapEngineResult<T> Failure(params string[] errors)
    {
        return new CapEngineResult<T>(false, default, errors, Array.Empty<string>());
    }
}
