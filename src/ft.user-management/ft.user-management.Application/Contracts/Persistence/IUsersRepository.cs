using System.Threading.Tasks;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Application.Contracts.Persistence;

public interface IUsersRepository : IGenericRepository<User>
{
    Task<User?> GetUserByEmail(string email);
    Task<bool> IsUserExists(string email);
}