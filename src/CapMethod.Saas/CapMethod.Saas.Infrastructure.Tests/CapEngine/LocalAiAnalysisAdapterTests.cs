using CapMethod.Saas.Application.Ports;
using CapMethod.Saas.Infrastructure.CapEngine;
using Xunit;

namespace CapMethod.Saas.Infrastructure.Tests.CapEngine;

public sealed class LocalAiAnalysisAdapterTests
{
    [Fact]
    public async Task GenerateAiAnalysisDraftAsync_should_fail_when_ai_is_disabled()
    {
        LocalAiAnalysisAdapter adapter = new();
        Guid tenantId = Guid.NewGuid();
        Guid sessionId = Guid.NewGuid();
        CapArtifactRef analysisSnapshot = new(
            tenantId,
            sessionId,
            "AnalysisSnapshot",
            "tenant/session/analysis-snapshot.json",
            DateTimeOffset.UtcNow);
        GenerateAiAnalysisDraftCommand command = new(tenantId, sessionId, analysisSnapshot, IsAiEnabled: false);

        CapEngineResult<CapArtifactRef> result = await adapter.GenerateAiAnalysisDraftAsync(command, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Contains("AI is disabled for this session.", result.Errors);
    }

    [Fact]
    public async Task GenerateAiAnalysisDraftAsync_should_return_ai_draft_when_ai_is_enabled()
    {
        LocalAiAnalysisAdapter adapter = new();
        Guid tenantId = Guid.NewGuid();
        Guid sessionId = Guid.NewGuid();
        CapArtifactRef analysisSnapshot = new(
            tenantId,
            sessionId,
            "AnalysisSnapshot",
            "tenant/session/analysis-snapshot.json",
            DateTimeOffset.UtcNow);
        GenerateAiAnalysisDraftCommand command = new(tenantId, sessionId, analysisSnapshot, IsAiEnabled: true);

        CapEngineResult<CapArtifactRef> result = await adapter.GenerateAiAnalysisDraftAsync(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.NotNull(result.Value);
        Assert.Equal("AIAnalysisDraft", result.Value.ArtifactType);
        Assert.Equal(tenantId, result.Value.TenantId);
    }
}
