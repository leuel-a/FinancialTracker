namespace ft.employee_management.Domain.Entities;

public class BaseDomainEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}