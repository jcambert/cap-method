using System.Security.Claims;
using System.Text;
using CapMethod.Saas.Application.Beneficiaries;
using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Infrastructure;
using CapMethod.Saas.Server.Analysis;
using CapMethod.Saas.Server.Questionnaires;
using CapMethod.Saas.Server.Security;
using CapMethod.Saas.Server.Synthesis;
using CapMethod.Saas.Shared.Api;
using CapMethod.Saas.Shared.Beneficiaries;
using CapMethod.Saas.Shared.CapSessions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
ConfigurePersistence(builder);
ConfigureAuthentication(builder);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICapUserContextAccessor, HttpCapUserContextAccessor>();
builder.Services.AddScoped<DevelopmentJwtTokenService>();
builder.Services.AddScoped<ProductionJwtTokenService>();
builder.Services.AddScoped<BeneficiaryPortalJwtTokenService>();
builder.Services.AddScoped<PasswordHashVerifier>();
builder.Services.AddSingleton<BeneficiaryQuestionnaireStore>();
builder.Services.AddSingleton<StructuredAnalysisService>();
builder.Services.AddSingleton<EditableSynthesisStore>();
builder.Services.Configure<ProductionAuthenticationOptions>(builder.Configuration.GetSection("Authentication:ProductionUser"));
builder.Services.Configure<BeneficiaryPortalAuthenticationOptions>(builder.Configuration.GetSection("Authentication:BeneficiaryPortal"));
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

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();

app.MapGet("/api/info", () => new ApiInfo(
    "CAP Method SaaS",
    "v3.1-saas-production-ready",
    "Blazor WebAssembly hosted",
    AzureRequired: false,
    AiRequired: false));

app.MapPost("/api/auth/token", (
    ProductionLoginRequest request,
    ProductionJwtTokenService tokenService) =>
{
    AccessTokenResponse? response = tokenService.TryCreateToken(request.Email, request.Password);

    if (response is null)
    {
        return Results.Unauthorized();
    }

    return Results.Ok(response);
});

app.MapPost("/api/beneficiary/auth/token", (
    BeneficiaryPortalLoginRequest request,
    BeneficiaryPortalJwtTokenService tokenService) =>
{
    BeneficiaryAccessTokenResponse? response = tokenService.TryCreateToken(request.Email, request.AccessCode);

    if (response is null)
    {
        return Results.Unauthorized();
    }

    return Results.Ok(response);
});

if (app.Environment.IsDevelopment())
{
    app.MapPost("/api/dev/token", (
        IConfiguration configuration,
        DevelopmentJwtTokenService tokenService) =>
    {
        Guid tenantId = ReadRequiredConfigurationGuid(configuration, "Security:DevelopmentTenantId");
        Guid userId = ReadRequiredConfigurationGuid(configuration, "Security:DevelopmentUserId");
        DevelopmentTokenResponse response = tokenService.CreateToken(tenantId, userId);

        return Results.Ok(response);
    });
}

app.MapGet("/api/me", (ICapUserContextAccessor userContextAccessor) =>
{
    CapUserContext userContext = userContextAccessor.GetRequiredContext();

    return Results.Ok(new
    {
        userContext.TenantId,
        userContext.UserId,
        userContext.IsAuthenticated,
        userContext.IsDevelopmentFallback
    });
}).RequireAuthorization();

app.MapGet("/api/beneficiary/me", (ClaimsPrincipal user) =>
{
    Guid tenantId = ReadRequiredGuidClaim(user, "tenant_id");
    Guid beneficiaryId = ReadRequiredGuidClaim(user, BeneficiaryPortalJwtTokenService.BeneficiaryIdClaimType);
    string email = user.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

    if (string.IsNullOrWhiteSpace(email))
    {
        throw new UnauthorizedAccessException("Claim 'email' is required for beneficiary portal context.");
    }

    return Results.Ok(new BeneficiaryPortalContextResponse(
        tenantId,
        beneficiaryId,
        email,
        IsAuthenticated: true));
}).RequireAuthorization();

app.MapBeneficiaryQuestionnaireEndpoints();
app.MapStructuredAnalysisEndpoints();
app.MapEditableSynthesisEndpoints();

app.MapPost("/api/beneficiaries", async (
    CreateBeneficiaryRequest request,
    ICapUserContextAccessor userContextAccessor,
    CreateBeneficiaryUseCase useCase,
    CancellationToken cancellationToken) =>
{
    CapUserContext userContext = userContextAccessor.GetRequiredContext();
    CreateBeneficiaryCommand command = new(
        userContext.TenantId,
        request.FirstName,
        request.LastName,
        request.Email);

    CreateBeneficiaryResult result = await useCase.ExecuteAsync(command, cancellationToken);
    BeneficiaryResponse response = MapCreateBeneficiaryResultToResponse(result);

    return Results.Created($"/api/beneficiaries/{response.BeneficiaryId}", response);
}).RequireAuthorization();

app.MapPost("/api/cap-sessions", async (
    CreateCapSessionRequest request,
    ICapUserContextAccessor userContextAccessor,
    CreateCapSessionUseCase useCase,
    CancellationToken cancellationToken) =>
{
    CapUserContext userContext = userContextAccessor.GetRequiredContext();
    CreateCapSessionCommand command = new(
        userContext.TenantId,
        request.BeneficiaryId,
        userContext.UserId,
        request.EnableAi);

    CreateCapSessionResult result = await useCase.ExecuteAsync(command, cancellationToken);

    if (result.IsBeneficiaryNotFound)
    {
        return Results.NotFound();
    }

    CapSessionResponse response = MapCreateSessionResultToResponse(result);

    return Results.Created($"/api/cap-sessions/{response.CapSessionId}", response);
}).RequireAuthorization();

app.MapGet("/api/cap-sessions", async (
    ICapUserContextAccessor userContextAccessor,
    ListCapSessionsUseCase useCase,
    CancellationToken cancellationToken) =>
{
    CapUserContext userContext = userContextAccessor.GetRequiredContext();
    ListCapSessionsQuery query = new(userContext.TenantId);
    IReadOnlyCollection<ListCapSessionResult> results = await useCase.ExecuteAsync(query, cancellationToken);

    CapSessionSummaryResponse[] response = results
        .Select(MapListResultToSummaryResponse)
        .ToArray();

    return Results.Ok(response);
}).RequireAuthorization();

app.MapGet("/api/cap-sessions/{capSessionId:guid}", async (
    Guid capSessionId,
    ICapUserContextAccessor userContextAccessor,
    GetCapSessionUseCase useCase,
    CancellationToken cancellationToken) =>
{
    CapUserContext userContext = userContextAccessor.GetRequiredContext();
    GetCapSessionQuery query = new(userContext.TenantId, capSessionId);
    GetCapSessionResult? result = await useCase.ExecuteAsync(query, cancellationToken);

    if (result is null)
    {
        return Results.NotFound();
    }

    CapSessionResponse response = MapGetResultToResponse(result);

    return Results.Ok(response);
}).RequireAuthorization();

app.MapFallbackToFile("index.html");
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

static void ConfigureAuthentication(WebApplicationBuilder builder)
{
    JwtOptions jwtOptions = builder.Configuration.GetSection("Authentication:Jwt").Get<JwtOptions>() ?? new JwtOptions();
    jwtOptions.Validate();

    if (!builder.Environment.IsDevelopment() && IsUnsafeDefaultSigningKey(jwtOptions.SigningKey))
    {
        throw new InvalidOperationException("Authentication:Jwt:SigningKey must be overridden outside Development.");
    }

    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Authentication:Jwt"));
    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };
        });
}

static bool IsUnsafeDefaultSigningKey(string signingKey)
{
    return string.Equals(
        signingKey,
        "CHANGE_ME_WITH_A_SECURE_32_CHARACTERS_MINIMUM_SECRET",
        StringComparison.Ordinal);
}

static Guid ReadRequiredConfigurationGuid(IConfiguration configuration, string key)
{
    string? value = configuration[key];

    if (!Guid.TryParse(value, out Guid parsed) || parsed == Guid.Empty)
    {
        throw new InvalidOperationException($"Configuration '{key}' must be a non-empty GUID.");
    }

    return parsed;
}

static Guid ReadRequiredGuidClaim(ClaimsPrincipal principal, string claimType)
{
    string? value = principal.FindFirstValue(claimType);

    if (!Guid.TryParse(value, out Guid parsed) || parsed == Guid.Empty)
    {
        throw new UnauthorizedAccessException($"Claim '{claimType}' must be a non-empty GUID.");
    }

    return parsed;
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

public partial class Program;
