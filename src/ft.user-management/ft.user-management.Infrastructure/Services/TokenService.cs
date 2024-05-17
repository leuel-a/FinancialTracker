using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ft.user_management.Domain.Entities;
using Microsoft.Extensions.Configuration;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _accessTokenKey;
    private readonly string _refreshTokenKey;
    private readonly string _accessTokenTtl;
    private readonly string _refreshTokenTtl;
    private readonly IUsersService _usersService;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public TokenService(IConfiguration configuration, IUsersService usersService)
    {
        _issuer = configuration.GetSection("JwtSettings:Issuer").Value!;
        _audience = configuration.GetSection("JwtSettings:Audience").Value!;
        _accessTokenKey = configuration.GetSection("JwtSettings:AccessTokenKey").Value!;
        _refreshTokenKey = configuration.GetSection("JwtSettings:RefreshTokenKey").Value!;
        _accessTokenTtl = configuration.GetSection("JwtSettings:AccessTokenTtl").Value!;
        _refreshTokenTtl = configuration.GetSection("JwtSettings:RefreshTokenTtl").Value!;
        _usersService = usersService;
    }

    public async Task<string> GenerateToken(ApplicationUser user, string type = "AccessToken")
    {
        var key = type == "AccessToken" ? _accessTokenKey : _refreshTokenKey;
        var expiryDate = type == "AccessToken" ? _accessTokenTtl : _refreshTokenTtl;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var roles = await _usersService.GetRoleAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(issuer: _issuer, audience: _audience, claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(expiryDate)), signingCredentials: credentials);
        return _tokenHandler.WriteToken(token);
    }

    public int? GetUserIdFromToken(string token)
    {
        if (IsTokenExpired(token) == true)
            return null;

        var principal = _tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateAudience = true, ValidateIssuer = true, ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenKey)),
                ValidateLifetime = true
            }, out var securityToken);

        var userId = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return int.Parse(userId);
    }

    public bool IsTokenExpired(string token)
    {
        var principal = GetPrincipalFromExpiredToken(token);

        var expirationDateUnix =
            long.Parse(principal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp).Value);
        var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationDateUnix);

        return expirationDate.UtcDateTime < DateTime.UtcNow;
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenKey)),
            ValidateLifetime = false
        };

        var principal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}