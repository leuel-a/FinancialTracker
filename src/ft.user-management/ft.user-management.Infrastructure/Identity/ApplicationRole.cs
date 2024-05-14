using Microsoft.AspNetCore.Identity;

namespace ft.user_management.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public string Description { get; set; }
}