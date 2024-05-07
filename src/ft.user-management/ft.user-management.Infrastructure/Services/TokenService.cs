using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ft.user_management.Domain.Entities;
using Microsoft.Extensions.Configuration;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = _configuration["Jwt:Key"];
        _issuer = _configuration["Jwt:Issuer"];
        _audience = _configuration["Jwt:Audience"];
    }
    
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
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
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );
        return tokenHandler.WriteToken(token);
    }
}