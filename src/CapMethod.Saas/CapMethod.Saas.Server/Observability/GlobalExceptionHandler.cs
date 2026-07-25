using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CapMethod.Saas.Server.Observability;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        string correlationId = httpContext.TraceIdentifier;
        _logger.LogError(
            exception,
            "Unhandled request failure. CorrelationId: {CorrelationId}, Path: {RequestPath}",
            correlationId,
            httpContext.Request.Path.Value);

        ProblemDetails problem = new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Une erreur inattendue est survenue.",
            Detail = "La demande n'a pas pu être traitée. Utilisez l'identifiant de corrélation pour le diagnostic.",
            Type = "https://httpstatuses.com/500",
            Instance = httpContext.Request.Path
        };
        problem.Extensions["correlationId"] = correlationId;

        httpContext.Response.StatusCode = problem.Status.Value;
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problem,
            Exception = exception
        });
    }
}