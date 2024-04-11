using System;

namespace ft.transaction_management.Domain.Common;

public class BaseDomainEntity
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastModified { get; set; }
}