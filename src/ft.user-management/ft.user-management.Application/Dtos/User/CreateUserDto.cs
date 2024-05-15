using System;

namespace ft.user_management.Application.Dtos.User;

public class CreateUserDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Password { get; set; }
}