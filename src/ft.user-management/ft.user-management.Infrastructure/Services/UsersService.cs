using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Infrastructure.Services;

public class UsersService : IUsersService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UserManagementDbContext _dbContext;
    public UsersService(UserManager<ApplicationUser> userManager, UserManagementDbContext dbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    public async Task<IdentityResult> AddAsync(ApplicationUser applicationUser, string password)
    {
        return await _userManager.CreateAsync(applicationUser, password);
    }

    public async Task<ApplicationUser?> GetByIdAsync(int id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<List<ApplicationUser>> GetAllAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<ApplicationUser?> UpdateAsync(ApplicationUser applicationUser)
    {
        var result = await _userManager.UpdateAsync(applicationUser);
        return result.Succeeded == true ? applicationUser : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            return false;
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded == true;
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<List<string>> GetRoleAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList();
    }

    public async Task<bool> VerifyPasswordAsync(ApplicationUser applicationUser, string password)
    {
        return await _userManager.CheckPasswordAsync(applicationUser, password);
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }
}