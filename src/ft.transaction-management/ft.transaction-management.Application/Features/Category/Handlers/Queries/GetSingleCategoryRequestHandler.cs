using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Category;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Features.Category.Requests.Queries;

namespace ft.transaction_management.Application.Features.Category.Handlers.Queries;

public class GetSingleCategoryRequestHandler : IRequestHandler<GetSingleCategoryRequest, ReadResourceResponse<ReadCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategoriesRepository _categoriesRepository;
    
    public GetSingleCategoryRequestHandler(IMapper mapper, ICategoriesRepository categoriesRepository)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
    }
    
    public async Task<ReadResourceResponse<ReadCategoryDto>> Handle(GetSingleCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadCategoryDto>();
        
        var category = await _categoriesRepository.GetByIdAsync(request.Id);

        if (category == null)
        {
            response.Succeeded = false;
            response.Message = "Category not found";
            
            return response;
        }

        response.Succeeded = true;
        response.Message = "Category found";
        response.Resource = _mapper.Map<ReadCategoryDto>(category);
        
        return response;
    }
}