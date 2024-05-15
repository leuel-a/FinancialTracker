using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ft.user_management.Domain.Entites;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.Infrastructure.Services;

public class UsersService : IUsersService
{
    private readonly UserManager<User> _userManager;
    
    public UsersService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<User?> AddAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded == true)
            return user;
        return null;
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded == true)
            return user;
        return null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            return false;
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded == true)
            return true;
        return false;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
}