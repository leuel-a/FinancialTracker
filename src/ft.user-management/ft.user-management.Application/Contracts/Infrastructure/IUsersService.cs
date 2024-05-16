using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Application.Contracts.Infrastructure;

public interface IUsersService
{
    Task<ApplicationUser?> AddAsync(ApplicationUser applicationUser, string password);
    Task<ApplicationUser?> GetByIdAsync(int id);
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser?> UpdateAsync(ApplicationUser applicationUser);
    Task<bool> DeleteAsync(int id);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<bool> VerifyPasswordAsync(ApplicationUser applicationUser, string password);
}