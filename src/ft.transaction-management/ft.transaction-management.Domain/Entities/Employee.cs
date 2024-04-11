using ft.transaction_management.Domain.Common;

namespace ft.transaction_management.Domain.Entities;

public class Employee : BaseDomainEntity
{
    public string FirstName { get; set; } = null!;
    public string MiddleInitial { get; set; } = null!;
    public string LastName  { get; set; } = null!;
}