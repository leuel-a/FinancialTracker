using System;
using ft.transaction_management.Application.DTOs.CommonDto;

namespace ft.transaction_management.Application.DTOs.CategoryDto;

public class ReadCategoryDto : BaseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}