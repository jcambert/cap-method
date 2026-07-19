using System.Security.Claims;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.Analysis;

namespace CapMethod.Saas.Server.Analysis;

public static class StructuredAnalysisEndpoints
{
    public static IEndpointRouteBuilder MapStructuredAnalysisEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/beneficiary/analysis", (
            ClaimsPrincipal user,
            StructuredAnalysisService analysisService) =>
        {
            Guid tenantId = ReadRequiredGuidClaim(user, "tenant_id");
            Guid beneficiaryId = ReadRequiredGuidClaim(user, BeneficiaryPortalJwtTokenService.BeneficiaryIdClaimType);
            StructuredAnalysisResponse response = analysisService.Generate(tenantId, beneficiaryId);
            return Results.Ok(response);
        }).RequireAuthorization();

        return endpoints;
    }

    private static Guid ReadRequiredGuidClaim(ClaimsPrincipal principal, string claimType)
    {
        string? value = principal.FindFirstValue(claimType);

        if (!Guid.TryParse(value, out Guid parsed) || parsed == Guid.Empty)
        {
            throw new UnauthorizedAccessException($"Claim '{claimType}' must be a non-empty GUID.");
        }

        return parsed;
    }
}
