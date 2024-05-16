using Microsoft.AspNetCore.Identity;

namespace ft.user_management.Domain.Entities;

public class ApplicationRole: IdentityRole<int>
{
    public string? Description { get; set; }
}