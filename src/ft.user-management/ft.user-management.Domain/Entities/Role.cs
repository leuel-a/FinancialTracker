using System.Collections.Generic;

namespace ft.user_management.Domain.Entities;

public class Role : BaseDomainEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
}