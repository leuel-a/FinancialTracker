using System.Collections;
using System.Collections.Generic;
using ft.transaction_management.Domain.Common;

namespace ft.transaction_management.Domain.Entities;

public class Category : BaseDomainEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ParentId { get; set; } // TODO: Figure out what the purpose of this property is
    
    public ICollection<Transaction> Transactions { get; set; } = [];
}