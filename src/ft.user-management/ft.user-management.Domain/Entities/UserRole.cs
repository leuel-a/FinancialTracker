namespace ft.user_management.Domain.Entities;

public class UserRole
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    // These are navigation properties
    public User User { get; set; }
    public Role Role { get; set; }
}