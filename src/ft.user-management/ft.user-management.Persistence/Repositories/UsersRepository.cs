using Microsoft.EntityFrameworkCore;
using ft.user_management.Domain.Entities;
using ft.user_management.Application.Contracts.Persistence;

namespace ft.user_management.Persistence.Repositories;

public class UsersRepository: GenericRepository<User>, IUsersRepository
{
    private readonly UserManagementDbContext _dbContext;
    public UsersRepository(UserManagementDbContext dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> IsUserExists(string email)
    {
        var user = await GetUserByEmail(email);
        return user == null;
    }
}