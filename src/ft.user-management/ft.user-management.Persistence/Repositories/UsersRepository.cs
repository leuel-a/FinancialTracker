using Microsoft.EntityFrameworkCore;
using ft.user_management.Domain.Entities;
using ft.user_management.Application.Contracts.Persistence;

namespace ft.user_management.Persistence.Repositories;

public class UsersRepository(UserManagementDbContext dbContext) : GenericRepository<User>(dbContext), IUsersRepository
{
    public async Task<User?> GetUserByEmail(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> IsUserExists(string email)
    {
        var user = await GetUserByEmail(email);
        return user == null;
    }
}