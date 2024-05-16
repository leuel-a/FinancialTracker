using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Infrastructure.Services;

public class UsersService : IUsersService
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UsersService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<ApplicationUser?> AddAsync(ApplicationUser applicationUser, string password)
    {
        var result = await _userManager.CreateAsync(applicationUser, password);
        return result.Succeeded == true ? applicationUser : null;
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
        if (result.Succeeded == true)
            return applicationUser;
        return null;
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

    public async Task<bool> VerifyPasswordAsync(ApplicationUser applicationUser, string password)
    {
        return await _userManager.CheckPasswordAsync(applicationUser, password);
    }
}