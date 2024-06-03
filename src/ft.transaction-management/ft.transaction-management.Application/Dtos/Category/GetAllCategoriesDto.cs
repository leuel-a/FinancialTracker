using System;

namespace ft.transaction_management.Application;

public class GetAllCategoriesDto
{
    public int? PageSize { get; set; }
    public int? CurrentPage { get; set; }
}
