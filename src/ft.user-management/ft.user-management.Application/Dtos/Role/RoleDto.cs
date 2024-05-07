using ft.user_management.Application.Dtos.Users;

namespace ft.user_management.Application.Dtos.Role;

public class RoleDto : BaseDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}