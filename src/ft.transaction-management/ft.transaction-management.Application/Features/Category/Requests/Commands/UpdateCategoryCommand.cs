using ft.transaction_management.Application.Dtos.Category;
using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Category.Requests.Commands;

public class UpdateCategoryCommand : IRequest<BaseCommandResponse>
{
    public UpdateCategoryDto? CategoryDto { get; set; }
}