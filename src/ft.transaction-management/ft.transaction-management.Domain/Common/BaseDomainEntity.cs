using System;

namespace ft.transaction_management.Domain.Common;

public class BaseDomainEntity
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}