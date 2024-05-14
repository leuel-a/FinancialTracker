using Microsoft.AspNetCore.Identity;

namespace ft.user_management.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public DateTime DateCreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}