using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ft.user_management.Application.Contracts.Infrastructure;

public interface IRolesService
{
    Task<List<ApplicationRole>> GetAllRoles();
    Task<ApplicationRole?> GetRoleById(int id);
    Task<IdentityResult> AddRoleAsync(ApplicationRole role);
    Task UpdateRoleAsync(ApplicationRole role);
    Task DeleteRoleAsync(ApplicationRole role);
}