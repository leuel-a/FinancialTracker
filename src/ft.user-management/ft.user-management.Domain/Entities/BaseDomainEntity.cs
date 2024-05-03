using System;

namespace ft.user_management.Domain.Entities;

public abstract class BaseDomainEntity
{
    public int Id { get; set; }
    public DateTime DateCreatedAt { get; set; }
    public DateTime LastModifiedDate { get; set; }
}