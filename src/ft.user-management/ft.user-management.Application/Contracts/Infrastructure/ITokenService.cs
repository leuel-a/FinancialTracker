using ft.user_management.Domain.Entities;

namespace ft.user_management.Application.Contracts.Infrastructure;

public interface ITokenService
{
    public string GenerateToken(User user);
}