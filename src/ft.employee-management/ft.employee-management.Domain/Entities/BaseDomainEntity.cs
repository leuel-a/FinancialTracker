using System;

namespace ft.employee_management.Domain.Entities;

public abstract class BaseDomainEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastModified { get; set; }
}