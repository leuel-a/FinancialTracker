using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Category;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Features.Category.Requests.Queries;
using System;

namespace ft.transaction_management.Application.Features.Category.Handlers.Queries;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginatedResponse<ReadCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategoriesRepository _categoriesRepository;

    public GetAllCategoriesQueryHandler(IMapper mapper, ICategoriesRepository categoriesRepository)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<PaginatedResponse<ReadCategoryDto>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        var response = new PaginatedResponse<ReadCategoryDto>();
        var validator = new GetAllCategoriesDtoValidator();
        var validationResult = await validator.ValidateAsync(query.GetAllCategoriesDto, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation failed please refer the errors";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var total = await _categoriesRepository.CountAsync();
        var pageSize = query.GetAllCategoriesDto.PageSize ?? 10;
        var currentPage = query.GetAllCategoriesDto.CurrentPage ?? 1;
        var totalPages = (int)Math.Ceiling((double)total / pageSize);

        var categories = await _categoriesRepository.GetAllAsyncPaginated(currentPage, pageSize);

        // Get the categories
        // var categories = await _categoriesRepository.GetAllAsync();
        // var categories = await _categoriesRepository.GetAllAsyncPaginated();
        // response.Succeeded = true;
        // response.Resources = _mapper.Map<IReadOnlyList<ReadCategoryDto>>(categories);
        return response;
    }
}