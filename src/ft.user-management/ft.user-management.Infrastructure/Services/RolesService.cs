using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ft.user_management.Infrastructure.Services;

public class RolesService : IRolesService
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RolesService(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<List<ApplicationRole>> GetAllRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles;
    }

    public async Task<ApplicationRole?> GetRoleById(int id)
    {
        return await _roleManager.Roles.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IdentityResult> AddRoleAsync(ApplicationRole role)
    {
        return await _roleManager.CreateAsync(role);
    }

    public async Task<IdentityResult> UpdateRoleAsync(ApplicationRole role)
    {
        return await _roleManager.UpdateAsync(role);
    }

    public async Task<IdentityResult> DeleteRoleAsync(ApplicationRole role)
    {
        return await _roleManager.DeleteAsync(role);
    }
}