using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Domain.Entities;

namespace ft.user_management.Application.Contracts.Persistence;

public interface IRolesRepository : IGenericRepository<Role>
{
    Task<ICollection<User>> GetUsersForRole(int id);
}