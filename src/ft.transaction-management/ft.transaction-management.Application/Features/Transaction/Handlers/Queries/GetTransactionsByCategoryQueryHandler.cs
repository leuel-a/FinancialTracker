using System;
using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Transaction.Validators;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class
    GetTransactionsByCategoryQueryHandler : IRequestHandler<GetTransactionsByCategoryQuery,
    PaginatedResponse<ReadTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionsByCategoryQueryHandler(IMapper mapper, ITransactionsRepository transactionsRepository,
        ICategoriesRepository categoriesRepository)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<PaginatedResponse<ReadTransactionDto>> Handle(GetTransactionsByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var response = new PaginatedResponse<ReadTransactionDto>();
        var validator = new GetTransactionsByCategoryDtoValidator(_categoriesRepository);
        var validationResult = await validator.ValidateAsync(request.TransactionByCategoryDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation error has occured. Please refer the errors for more direction.";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var pageSize = request.TransactionByCategoryDto!.PageSize ?? 10;
        var currentPage = request.TransactionByCategoryDto.CurrentPage ?? 1;

        var totalCount = await _transactionsRepository.CountTransactionsByCategory(request.TransactionByCategoryDto
            .CategoryId);
        var transactions =
            await _transactionsRepository.GetTransactionByCategory(request.TransactionByCategoryDto.CategoryId,
                pageSize, currentPage);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        response.Succeeded = true;
        response.CurrentPage = currentPage;
        response.TotalPages = totalPages;
        response.TotalCount = totalCount;
        response.PreviousPage = currentPage > 1 ? currentPage - 1 : null;
        response.NextPage = currentPage < totalPages ? currentPage + 1 : null;
        response.Data = _mapper.Map<IReadOnlyList<ReadTransactionDto>>(transactions);
        return response;
    }
}