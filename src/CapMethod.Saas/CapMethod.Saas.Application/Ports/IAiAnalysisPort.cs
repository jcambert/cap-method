namespace CapMethod.Saas.Application.Ports;

public interface IAiAnalysisPort
{
    Task<CapEngineResult<CapArtifactRef>> GenerateAiAnalysisDraftAsync(
        GenerateAiAnalysisDraftCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<CapArtifactRef>> GenerateAiAnalysisManifestAsync(
        GenerateAiAnalysisManifestCommand command,
        CancellationToken cancellationToken);
}

public sealed record GenerateAiAnalysisDraftCommand(
    Guid TenantId,
    Guid CapSessionId,
    CapArtifactRef AnalysisSnapshot,
    bool IsAiEnabled);

public sealed record GenerateAiAnalysisManifestCommand(
    Guid TenantId,
    Guid CapSessionId,
    CapArtifactRef AnalysisSnapshot,
    CapArtifactRef AiAnalysisDraft);
