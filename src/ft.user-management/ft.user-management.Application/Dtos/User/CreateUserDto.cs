using System;
using ft.user_management.Application.Dtos.User.Validators;

namespace ft.user_management.Application.Dtos.User;

public class CreateUserDto: BaseUserDto
{
    public string? Password { get; set; }
}