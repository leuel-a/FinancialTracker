using System;

using MediatR;

using ft.transaction_management.Application.DTOs.CategoryDto;

namespace ft.transaction_management.Application.Features.Category.Requests.Queries;

public class GetCategoryRequest : IRequest<ReadCategoryDto>
{
    public Guid Id { get; set; }
}