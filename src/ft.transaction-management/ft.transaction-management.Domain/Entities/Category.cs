namespace ft.transaction_management.Domain.Entities;

public class Category
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ParentId { get; set; }
}