
using System;
using Microsoft.AspNetCore.Identity;

namespace ft.user_management.Domain.Entites;

public class User : IdentityUser
{
    public DateTime DateOfBirth { get; set; }
    public DateTime DateCreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}