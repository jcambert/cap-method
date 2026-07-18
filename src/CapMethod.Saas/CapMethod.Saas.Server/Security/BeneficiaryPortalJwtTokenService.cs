using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CapMethod.Saas.Server.Security;

public sealed class BeneficiaryPortalJwtTokenService
{
    public const string BeneficiaryIdClaimType = "beneficiary_id";

    private readonly JwtOptions _jwtOptions;
    private readonly BeneficiaryPortalAuthenticationOptions _portalOptions;
    private readonly PasswordHashVerifier _passwordHashVerifier;

    public BeneficiaryPortalJwtTokenService(
        IOptions<JwtOptions> jwtOptions,
        IOptions<BeneficiaryPortalAuthenticationOptions> portalOptions,
        PasswordHashVerifier passwordHashVerifier)
    {
        _jwtOptions = jwtOptions.Value;
        _portalOptions = portalOptions.Value;
        _passwordHashVerifier = passwordHashVerifier;
        _jwtOptions.Validate();
        _portalOptions.Validate();
    }

    public BeneficiaryAccessTokenResponse? TryCreateToken(string email, string accessCode)
    {
        if (!string.Equals(email, _portalOptions.Email, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (!_passwordHashVerifier.VerifySha256(accessCode, _portalOptions.AccessCodeSha256))
        {
            return null;
        }

        return CreateToken();
    }

    private BeneficiaryAccessTokenResponse CreateToken()
    {
        DateTimeOffset expiresAtUtc = DateTimeOffset.UtcNow.AddHours(8);
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, _portalOptions.BeneficiaryId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, _portalOptions.BeneficiaryId.ToString()),
            new Claim("tenant_id", _portalOptions.TenantId.ToString()),
            new Claim(BeneficiaryIdClaimType, _portalOptions.BeneficiaryId.ToString()),
            new Claim(ClaimTypes.Email, _portalOptions.Email)
        ];

        JwtSecurityToken token = new(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAtUtc.UtcDateTime,
            signingCredentials: credentials);

        string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new BeneficiaryAccessTokenResponse(
            accessToken,
            "Bearer",
            expiresAtUtc,
            _portalOptions.TenantId,
            _portalOptions.BeneficiaryId,
            _portalOptions.Email);
    }
}
