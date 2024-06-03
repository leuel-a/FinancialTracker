using System;
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

        var totalCount = await _categoriesRepository.CountAsync();
        var pageSize = query.GetAllCategoriesDto.PageSize ?? 10;
        var currentPage = query.GetAllCategoriesDto.CurrentPage ?? 1;
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var categories = await _categoriesRepository.GetAllAsyncPaginated(currentPage, pageSize);

        // TODO: Find a better way to do this
        response.Succeeded = true;
        response.CurrentPage = currentPage;
        response.TotalPages = totalPages;
        response.TotalCount = totalCount;
        response.PreviousPage = currentPage > 1 ? currentPage - 1 : null;
        response.NextPage = currentPage < totalPages ? currentPage + 1 : null;
        response.Data = _mapper.Map<IReadOnlyList<ReadCategoryDto>>(categories);
        return response;
    }
}