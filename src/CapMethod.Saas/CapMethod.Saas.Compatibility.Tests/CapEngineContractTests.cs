using CapMethod.Saas.Application.Ports;

namespace CapMethod.Saas.Compatibility.Tests;

public sealed class CapEngineContractTests
{
    [Fact]
    public void V1_contract_should_keep_response_session_artifact_type()
    {
        CapArtifactRef artifact = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "ResponseSession",
            "tenant/session/response-session.json",
            DateTimeOffset.UtcNow);

        Assert.Equal("ResponseSession", artifact.ArtifactType);
    }

    [Fact]
    public void V1_contract_should_keep_analysis_snapshot_artifact_type()
    {
        CapArtifactRef artifact = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "AnalysisSnapshot",
            "tenant/session/analysis-snapshot.json",
            DateTimeOffset.UtcNow);

        Assert.Equal("AnalysisSnapshot", artifact.ArtifactType);
    }

    [Fact]
    public void V2_contract_should_keep_ai_analysis_draft_artifact_type()
    {
        CapArtifactRef artifact = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "AIAnalysisDraft",
            "tenant/session/ai-analysis-draft.md",
            DateTimeOffset.UtcNow);

        Assert.Equal("AIAnalysisDraft", artifact.ArtifactType);
    }

    [Fact]
    public void V2_contract_should_keep_ai_analysis_manifest_artifact_type()
    {
        CapArtifactRef artifact = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "AIAnalysisManifest",
            "tenant/session/ai-analysis-manifest.json",
            DateTimeOffset.UtcNow);

        Assert.Equal("AIAnalysisManifest", artifact.ArtifactType);
    }
}
