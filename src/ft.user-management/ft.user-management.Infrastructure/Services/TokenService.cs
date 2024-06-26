using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
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
        _issuer = configuration.GetSection("Jwt:Issuer").Value!;
        _audience = configuration.GetSection("Jwt:Audience").Value!;
        _accessTokenKey = configuration.GetSection("Jwt:AccessTokenKey").Value!;
        _refreshTokenKey = configuration.GetSection("Jwt:RefreshTokenKey").Value!;
        _accessTokenTtl = configuration.GetSection("Jwt:AccessTokenTtl").Value!;
        _refreshTokenTtl = configuration.GetSection("Jwt:RefreshTokenTtl").Value!;
        _usersService = usersService;
    }

    public async Task<string> GenerateToken(ApplicationUser user, string type = "AccessToken")
    {
        var key = type == "AccessToken" ? _accessTokenKey : _refreshTokenKey;

        if (string.IsNullOrEmpty(key))
            throw new Exception($"Key for {type} is not set in appsettings[.Development].json");

        var expiryDate = type == "AccessToken" ? _accessTokenTtl : _refreshTokenTtl;

        if (string.IsNullOrEmpty(expiryDate))
            throw new Exception($"Expiry date for {type} is not set in appsettings[.Development].json");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var roles = await _usersService.GetRoleAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(issuer: _issuer, audience: _audience, claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(expiryDate)), signingCredentials: credentials);
        return _tokenHandler.WriteToken(token);
    }

    public int? GetUserIdFromToken(string token)
    {
        if (IsTokenExpired(token))
            return null;

        var principal = _tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenKey)),
                ValidateLifetime = true,
                ValidAudience = _audience,
                ValidIssuer = _issuer
            }, out _);

        var userId = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return int.Parse(userId);
    }

    public bool IsTokenExpired(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            if (jwtToken == null)
                return true;

            var expirationDate = jwtToken.ValidTo;
            return expirationDate < DateTime.UtcNow;
        }
        catch (Exception ex)
        {
            return true;
        }
    }

    public async Task<string?> RefreshAccessToken(string accessToken, string refreshToken)
    {
        var principal = GetPrincipalFromExpiredToken(accessToken);
        var userId = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var refreshTokenUserId = GetUserIdFromToken(refreshToken);
        if (refreshTokenUserId == null || userId != refreshTokenUserId.ToString())
            return null;
        
        var user = await _usersService.GetByIdAsync(int.Parse(userId));
        if (user == null)
            return null;

        return await GenerateToken(user);
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

    public bool ValidToken(string accessToken)
    {
        try {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenKey)),
                ValidateLifetime = true,
                ValidAudience = _audience,
                ValidIssuer = _issuer
            };
            _tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out _);
            return true;
        } catch {
            return false;
        }
    }
}