using ft.transaction_management.Domain.Common;

namespace ft.transaction_management.Domain.Entities;

public class Category : BaseDomainEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}