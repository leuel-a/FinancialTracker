using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Features.Category.Requests.Commands;

namespace ft.transaction_management.Application.Features.Category.Handlers.Commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseCommandResponse>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public DeleteCategoryCommandHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<BaseCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var category = await _categoriesRepository.GetByIdAsync(request.Id);

        if (category == null)
        {
            response.Succeeded = false;
            response.Message = "Category not found";
            return response;
        }

        var result = await _categoriesRepository.DeleteAsync(category);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
        }
        else
        {
            response.Succeeded = true;
            response.Message = "Category deleted successfully.";
        }

        return response;
    }
}