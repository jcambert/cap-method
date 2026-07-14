using CapMethod.Saas.Application.Beneficiaries;
using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Infrastructure;
using CapMethod.Saas.Shared.Api;
using CapMethod.Saas.Shared.Beneficiaries;
using CapMethod.Saas.Shared.CapSessions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurePersistence(builder);

builder.Services.AddScoped<CreateBeneficiaryUseCase>();
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

app.MapPost("/api/beneficiaries", async (
    CreateBeneficiaryRequest request,
    CreateBeneficiaryUseCase useCase,
    CancellationToken cancellationToken) =>
{
    CreateBeneficiaryCommand command = new(
        request.TenantId,
        request.FirstName,
        request.LastName,
        request.Email);

    CreateBeneficiaryResult result = await useCase.ExecuteAsync(command, cancellationToken);
    BeneficiaryResponse response = MapCreateBeneficiaryResultToResponse(result);

    return Results.Created($"/api/beneficiaries/{response.BeneficiaryId}", response);
});

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

    if (result.IsBeneficiaryNotFound)
    {
        return Results.NotFound();
    }

    CapSessionResponse response = MapCreateSessionResultToResponse(result);

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

static void ConfigurePersistence(WebApplicationBuilder builder)
{
    string provider = builder.Configuration["Persistence:Provider"] ?? "InMemory";

    if (string.Equals(provider, "PostgreSql", StringComparison.OrdinalIgnoreCase))
    {
        string? connectionString = builder.Configuration.GetConnectionString("CapMethodSaas");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:CapMethodSaas is required when Persistence:Provider is PostgreSql.");
        }

        builder.Services.AddCapMethodSaasPostgreSqlInfrastructure(connectionString);
        return;
    }

    if (string.Equals(provider, "InMemory", StringComparison.OrdinalIgnoreCase))
    {
        builder.Services.AddCapMethodSaasInfrastructure();
        return;
    }

    throw new InvalidOperationException($"Unsupported persistence provider '{provider}'. Supported values are 'InMemory' and 'PostgreSql'.");
}

static BeneficiaryResponse MapCreateBeneficiaryResultToResponse(CreateBeneficiaryResult result)
{
    return new BeneficiaryResponse(
        result.BeneficiaryId,
        result.TenantId,
        result.FirstName,
        result.LastName,
        result.Email,
        result.CreatedAtUtc);
}

static CapSessionResponse MapCreateSessionResultToResponse(CreateCapSessionResult result)
{
    if (result.CapSessionId is null ||
        result.ConsultantId is null ||
        result.Status is null ||
        result.IsAiEnabled is null ||
        result.CreatedAtUtc is null)
    {
        throw new InvalidOperationException("A created CAP session result is incomplete.");
    }

    return new CapSessionResponse(
        result.CapSessionId.Value,
        result.TenantId,
        result.BeneficiaryId,
        result.ConsultantId.Value,
        result.Status,
        result.IsAiEnabled.Value,
        result.CreatedAtUtc.Value);
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
