using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Domain.Entites;

namespace ft.user_management.Application.Contracts.Infrastructure;

public interface IUsersService
{
    Task<User?> AddAsync(User user, string password);
    Task<User?> GetByIdAsync(string id);
    Task<List<User>> GetAllAsync();
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(string id);
    Task<User?> GetUserByEmailAsync(string email);
}