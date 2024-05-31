using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Category;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Features.Category.Requests.Queries;

namespace ft.transaction_management.Application.Features.Category.Handlers.Queries;

public class GetAllCategoriesRequestHandler : IRequestHandler<GetAllCategoriesRequest, ReadResourceResponse<ReadCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategoriesRepository _categoriesRepository;
    
    public GetAllCategoriesRequestHandler(IMapper mapper, ICategoriesRepository categoriesRepository)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
    }
    
    public async Task<ReadResourceResponse<ReadCategoryDto>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadCategoryDto>();
        
        // Get the categories
        var categories = await _categoriesRepository.GetAllAsync();
        response.Succeeded = true;
        response.Resources = _mapper.Map<IReadOnlyList<ReadCategoryDto>>(categories);
        
        return response;
    }
}