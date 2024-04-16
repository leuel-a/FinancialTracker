using System.Threading;
using System.Threading.Tasks;

using MediatR;
using AutoMapper;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.DTOs.CategoryDto;
using ft.transaction_management.Application.Features.Category.Requests.Queries;

namespace ft.transaction_management.Application.Features.Category.Handlers.Queries;

public class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest, ReadCategoryDto>
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IMapper _mapper;

    public GetCategoryRequestHandler(ICategoriesRepository categoriesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<ReadCategoryDto> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetAsync(request.Id);

        return _mapper.Map<ReadCategoryDto>(category);
    }
}