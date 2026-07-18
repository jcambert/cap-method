using System.Security.Claims;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Shared.Questionnaires;

namespace CapMethod.Saas.Server.Questionnaires;

public static class BeneficiaryQuestionnaireEndpoints
{
    public static IEndpointRouteBuilder MapBeneficiaryQuestionnaireEndpoints(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints.MapGroup("/api/beneficiary/questionnaires")
            .RequireAuthorization();

        group.MapGet("/", (ClaimsPrincipal user, BeneficiaryQuestionnaireStore store) =>
        {
            ReadBeneficiaryContext(user);
            return Results.Ok(store.ListDefinitions());
        });

        group.MapGet("/{questionnaireId}", (
            string questionnaireId,
            ClaimsPrincipal user,
            BeneficiaryQuestionnaireStore store) =>
        {
            ReadBeneficiaryContext(user);
            QuestionnaireDefinitionResponse? definition = store.FindDefinition(questionnaireId);
            return definition is null ? Results.NotFound() : Results.Ok(definition);
        });

        group.MapGet("/{questionnaireId}/progress", (
            string questionnaireId,
            ClaimsPrincipal user,
            BeneficiaryQuestionnaireStore store) =>
        {
            BeneficiaryContext context = ReadBeneficiaryContext(user);

            try
            {
                QuestionnaireProgressResponse progress = store.GetProgress(
                    context.TenantId,
                    context.BeneficiaryId,
                    questionnaireId);
                return Results.Ok(progress);
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
        });

        group.MapPut("/{questionnaireId}/answers", (
            string questionnaireId,
            SaveQuestionnaireAnswersRequest request,
            ClaimsPrincipal user,
            BeneficiaryQuestionnaireStore store) =>
        {
            BeneficiaryContext context = ReadBeneficiaryContext(user);

            try
            {
                QuestionnaireProgressResponse progress = store.Save(
                    context.TenantId,
                    context.BeneficiaryId,
                    questionnaireId,
                    request);
                return Results.Ok(progress);
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
            catch (ArgumentException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }
        });

        return endpoints;
    }

    private static BeneficiaryContext ReadBeneficiaryContext(ClaimsPrincipal user)
    {
        Guid tenantId = ReadRequiredGuidClaim(user, "tenant_id");
        Guid beneficiaryId = ReadRequiredGuidClaim(user, BeneficiaryPortalJwtTokenService.BeneficiaryIdClaimType);
        return new BeneficiaryContext(tenantId, beneficiaryId);
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

    private sealed record BeneficiaryContext(Guid TenantId, Guid BeneficiaryId);
}
