using System;
using ft.transaction_management.Domain.Common;

namespace ft.transaction_management.Domain.Entities;

public class Employee : BaseDomainEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime JoinedDate { get; set; }
    public bool IsActive { get; set; }
}