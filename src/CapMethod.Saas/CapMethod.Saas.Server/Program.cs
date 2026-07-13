using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Infrastructure;
using CapMethod.Saas.Shared.Api;
using CapMethod.Saas.Shared.CapSessions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCapMethodSaasInfrastructure();
builder.Services.AddScoped<CreateCapSessionUseCase>();
builder.Services.AddScoped<GetCapSessionUseCase>();
builder.Services.AddScoped<ListCapSessionsUseCase>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

WebApplication app = builder.Build();

app.UseCors();

app.MapGet("/api/info", () => new ApiInfo(
    "CAP Method SaaS",
    "v3.0-saas",
    "Blazor WebAssembly hosted",
    AzureRequired: false,
    AiRequired: false));

app.MapPost("/api/cap-sessions", async (
    CreateCapSessionRequest request,
    CreateCapSessionUseCase useCase,
    CancellationToken cancellationToken) =>
{
    CreateCapSessionCommand command = new(
        request.TenantId,
        request.BeneficiaryId,
        request.ConsultantId,
        request.EnableAi);

    CreateCapSessionResult result = await useCase.ExecuteAsync(command, cancellationToken);

    CapSessionResponse response = MapCreateResultToResponse(result);

    return Results.Created($"/api/cap-sessions/{response.CapSessionId}", response);
});

app.MapGet("/api/cap-sessions", async (
    Guid tenantId,
    ListCapSessionsUseCase useCase,
    CancellationToken cancellationToken) =>
{
    ListCapSessionsQuery query = new(tenantId);
    IReadOnlyCollection<ListCapSessionResult> results = await useCase.ExecuteAsync(query, cancellationToken);

    CapSessionSummaryResponse[] response = results
        .Select(MapListResultToSummaryResponse)
        .ToArray();

    return Results.Ok(response);
});

app.MapGet("/api/cap-sessions/{capSessionId:guid}", async (
    Guid capSessionId,
    Guid tenantId,
    GetCapSessionUseCase useCase,
    CancellationToken cancellationToken) =>
{
    GetCapSessionQuery query = new(tenantId, capSessionId);
    GetCapSessionResult? result = await useCase.ExecuteAsync(query, cancellationToken);

    if (result is null)
    {
        return Results.NotFound();
    }

    CapSessionResponse response = MapGetResultToResponse(result);

    return Results.Ok(response);
});

app.Run();

static CapSessionResponse MapCreateResultToResponse(CreateCapSessionResult result)
{
    return new CapSessionResponse(
        result.CapSessionId,
        result.TenantId,
        result.BeneficiaryId,
        result.ConsultantId,
        result.Status,
        result.IsAiEnabled,
        result.CreatedAtUtc);
}

static CapSessionResponse MapGetResultToResponse(GetCapSessionResult result)
{
    return new CapSessionResponse(
        result.CapSessionId,
        result.TenantId,
        result.BeneficiaryId,
        result.ConsultantId,
        result.Status,
        result.IsAiEnabled,
        result.CreatedAtUtc);
}

static CapSessionSummaryResponse MapListResultToSummaryResponse(ListCapSessionResult result)
{
    return new CapSessionSummaryResponse(
        result.CapSessionId,
        result.TenantId,
        result.BeneficiaryId,
        result.ConsultantId,
        result.Status,
        result.IsAiEnabled,
        result.CreatedAtUtc);
}
