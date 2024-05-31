using MediatR;
using System.Linq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Category;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Category.Validators;
using ft.transaction_management.Application.Features.Category.Requests.Commands;

namespace ft.transaction_management.Application.Features.Category.Handlers.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCommandResponse<ReadCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategoriesRepository _categoriesRepository;

    public CreateCategoryCommandHandler(IMapper mapper, ICategoriesRepository categoriesRepository)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
    }
    
    public async Task<CreateCommandResponse<ReadCategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateCommandResponse<ReadCategoryDto>();
        var validator = new CreateCategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(request.CategoryDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation error. Please refer the error messages.";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return response;
        }
        
        var category = _mapper.Map<Domain.Entities.Category>(request.CategoryDto);
        
        // Add the category to the database
        var result = await _categoriesRepository.AddAsync(category);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
        }
        else
        {
            response.Succeeded = true;
            response.Message = "Category created successfully.";
            response.CreatedResource = _mapper.Map<ReadCategoryDto>(category); 
        }
        return response;
    }
}