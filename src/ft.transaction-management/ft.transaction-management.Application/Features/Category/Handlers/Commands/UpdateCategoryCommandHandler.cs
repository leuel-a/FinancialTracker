using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Category.Validators;
using ft.transaction_management.Application.Features.Category.Requests.Commands;

namespace ft.transaction_management.Application.Features.Category.Handlers.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly ICategoriesRepository _categoriesRepository;

    public UpdateCategoryCommandHandler(IMapper mapper, ICategoriesRepository categoriesRepository)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<BaseCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateCategoryDtoValidator(_categoriesRepository);
        var validationResult = await validator.ValidateAsync(request.CategoryDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation error has occured. Please refer the errors.";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var category = await _categoriesRepository.GetByIdAsync(request.CategoryDto!.Id);
        _mapper.Map(request.CategoryDto, category);

        var result = await _categoriesRepository.UpdateAsync(category);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
        }
        else
        {
            response.Succeeded = true;
            response.Message = "Category updated successfully.";
        }

        return response;
    }
}