using ft.transaction_management.Application.Dtos.Category;
using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Category.Requests.Queries;

public class GetAllCategoriesRequest : IRequest<ReadResourceResponse<ReadCategoryDto>>
{
}