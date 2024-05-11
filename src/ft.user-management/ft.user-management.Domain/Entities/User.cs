using System.Collections.Generic;

namespace ft.user_management.Domain.Entities;

public class User : BaseDomainEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    
}