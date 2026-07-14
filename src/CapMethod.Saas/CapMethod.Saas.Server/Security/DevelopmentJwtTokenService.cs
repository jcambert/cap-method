using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CapMethod.Saas.Server.Security;

public sealed class DevelopmentJwtTokenService
{
    private readonly JwtOptions _options;

    public DevelopmentJwtTokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
        _options.Validate();
    }

    public DevelopmentTokenResponse CreateToken(Guid tenantId, Guid userId)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId is required.", nameof(tenantId));
        }

        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId is required.", nameof(userId));
        }

        DateTimeOffset expiresAtUtc = DateTimeOffset.UtcNow.AddHours(8);
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_options.SigningKey));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim("tenant_id", tenantId.ToString())
        ];

        JwtSecurityToken token = new(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAtUtc.UtcDateTime,
            signingCredentials: credentials);

        string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new DevelopmentTokenResponse(
            accessToken,
            "Bearer",
            expiresAtUtc,
            tenantId,
            userId);
    }
}
