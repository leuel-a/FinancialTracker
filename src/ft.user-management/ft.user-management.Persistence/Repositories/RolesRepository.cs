using Microsoft.EntityFrameworkCore;
using ft.user_management.Domain.Entities;
using ft.user_management.Application.Contracts.Persistence;

namespace ft.user_management.Persistence.Repositories;

public class RolesRepository: GenericRepository<Role>, IRolesRepository
{
    private readonly UserManagementDbContext _dbContext;
    
    public RolesRepository(UserManagementDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ICollection<User>> GetUsersForRole(int id)
    {
        var users = await _dbContext.UserRoles
            .Where(ur => ur.RoleId == id)
            .Select(ur => ur.User)
            .ToListAsync();
        return users;
    }
}