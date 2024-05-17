using System.Threading.Tasks;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Application.Contracts.Infrastructure;

public interface ITokenService
{
   Task<string> GenerateToken(ApplicationUser user, string type = "AccessToken");
   int? GetUserIdFromToken(string token);
   bool IsTokenExpired(string token);
}
