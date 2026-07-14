using System.Security.Claims;

namespace CapMethod.Saas.Server.Security;

public sealed class HttpCapUserContextAccessor : ICapUserContextAccessor
{
    private const string TenantClaimType = "tenant_id";
    private const string UserClaimType = ClaimTypes.NameIdentifier;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public HttpCapUserContextAccessor(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _environment = environment;
    }

    public CapUserContext GetRequiredContext()
    {
        HttpContext? httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User.Identity?.IsAuthenticated == true)
        {
            Guid tenantId = ReadRequiredGuidClaim(httpContext.User, TenantClaimType);
            Guid userId = ReadRequiredGuidClaim(httpContext.User, UserClaimType);

            return new CapUserContext(
                tenantId,
                userId,
                IsAuthenticated: true,
                IsDevelopmentFallback: false);
        }

        if (_environment.IsDevelopment() && IsDevelopmentFallbackEnabled())
        {
            Guid tenantId = ReadRequiredConfigurationGuid("Security:DevelopmentTenantId");
            Guid userId = ReadRequiredConfigurationGuid("Security:DevelopmentUserId");

            return new CapUserContext(
                tenantId,
                userId,
                IsAuthenticated: false,
                IsDevelopmentFallback: true);
        }

        throw new UnauthorizedAccessException("CAP user context is required.");
    }

    private bool IsDevelopmentFallbackEnabled()
    {
        return bool.TryParse(_configuration["Security:EnableDevelopmentUserContext"], out bool enabled) && enabled;
    }

    private Guid ReadRequiredConfigurationGuid(string key)
    {
        string? value = _configuration[key];

        if (!Guid.TryParse(value, out Guid parsed) || parsed == Guid.Empty)
        {
            throw new InvalidOperationException($"Configuration '{key}' must be a non-empty GUID when development user context is enabled.");
        }

        return parsed;
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
