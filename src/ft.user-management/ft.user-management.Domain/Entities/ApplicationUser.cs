using System;
using Microsoft.AspNetCore.Identity;

namespace ft.user_management.Domain.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateCreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}