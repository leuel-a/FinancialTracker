using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.DTOs.CategoryDto;
using ft.transaction_management.Application.Features.Category.Requests.Queries;

namespace ft.transaction_management.Application.Features.Category.Handlers.Queries;

public class
    GetCategoryWithTransactionsRequestHandler : IRequestHandler<GetCategoryWithTransactionsRequest,
    ReadCategoryWithTransactionsDto>
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IMapper _mapper;

    public GetCategoryWithTransactionsRequestHandler(ICategoriesRepository categoriesRepository, IMapper mapper)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<ReadCategoryWithTransactionsDto> Handle(GetCategoryWithTransactionsRequest request,
        CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetCategoryWithTransactionsAsync(request.Id);
        return _mapper.Map<ReadCategoryWithTransactionsDto>(category);
    }
}