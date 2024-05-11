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
    private readonly string _accessTokenKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly IConfiguration _configuration;
    private readonly string _refreshTokenKey;
    private readonly string _accessTokenTTL;
    private readonly string _refreshTokenTTL;
    private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _accessTokenKey = _configuration["Jwt:AccessTokenKey"];
        _refreshTokenKey = _configuration["Jwt:RefreshTokenKey"];
        _issuer = _configuration["Jwt:Issuer"];
        _audience = _configuration["Jwt:Audience"];
        _accessTokenTTL = _configuration["Jwt:AccessTokenTTL"];
        _refreshTokenTTL = _configuration["Jwt:RefreshTokenTTL"];
    }

    public string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Here is where the roles will be stored as claims and the Identity will take of the rest
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email!),
        };

        // TODO: make the expires from a configuration file
        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddMinutes(int.Parse(_accessTokenTTL)),
            signingCredentials: credentials
        );
        return _tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddMinutes(int.Parse(_refreshTokenTTL)),
            signingCredentials: credentials
        );
        return _tokenHandler.WriteToken(token);
    }
}