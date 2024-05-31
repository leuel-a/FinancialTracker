using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Category.Requests.Commands;

public class DeleteCategoryCommand : IRequest<BaseCommandResponse>
{
    public int Id { get; set; }
}