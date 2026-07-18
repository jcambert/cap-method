using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CapMethod.Saas.Server.Security;

public sealed class ProductionJwtTokenService
{
    private readonly JwtOptions _jwtOptions;
    private readonly ProductionAuthenticationOptions _authenticationOptions;
    private readonly PasswordHashVerifier _passwordHashVerifier;

    public ProductionJwtTokenService(
        IOptions<JwtOptions> jwtOptions,
        IOptions<ProductionAuthenticationOptions> authenticationOptions,
        PasswordHashVerifier passwordHashVerifier)
    {
        _jwtOptions = jwtOptions.Value;
        _authenticationOptions = authenticationOptions.Value;
        _passwordHashVerifier = passwordHashVerifier;
        _jwtOptions.Validate();
        _authenticationOptions.Validate();
    }

    public AccessTokenResponse? TryCreateToken(string email, string password)
    {
        if (!string.Equals(email, _authenticationOptions.Email, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!_passwordHashVerifier.VerifySha256(password, _authenticationOptions.PasswordSha256))
        {
            return null;
        }

        return CreateToken(_authenticationOptions.TenantId, _authenticationOptions.UserId);
    }

    private AccessTokenResponse CreateToken(Guid tenantId, Guid userId)
    {
        DateTimeOffset expiresAtUtc = DateTimeOffset.UtcNow.AddHours(8);
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim("tenant_id", tenantId.ToString()),
            new Claim(ClaimTypes.Email, _authenticationOptions.Email)
        ];

        JwtSecurityToken token = new(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAtUtc.UtcDateTime,
            signingCredentials: credentials);

        string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new AccessTokenResponse(
            accessToken,
            "Bearer",
            expiresAtUtc,
            tenantId,
            userId);
    }
}
