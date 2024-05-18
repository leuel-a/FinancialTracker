using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ft.user_management.Application.Contracts.Infrastructure;

public interface IUsersService
{
    Task<IdentityResult> AddAsync(ApplicationUser applicationUser, string password);
    Task<ApplicationUser?> GetByIdAsync(int id);
    Task<List<ApplicationUser>> GetAllAsync();
    Task<IdentityResult> UpdateAsync(ApplicationUser applicationUser);
    Task<IdentityResult> DeleteAsync(ApplicationUser user);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<List<string>> GetRoleAsync(ApplicationUser user);
    Task<bool> VerifyPasswordAsync(ApplicationUser applicationUser, string password);
    Task<bool> SaveChangesAsync();
}