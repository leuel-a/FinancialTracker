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
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, PaginatedResponse<ReadTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly ITransactionsRepository _transactionsRepository;

    public GetAllTransactionsQueryHandler(ITransactionsRepository transactionsRepository, IMapper mapper)
    {
        _mapper = mapper;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<PaginatedResponse<ReadTransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var response = new PaginatedResponse<ReadTransactionDto>();
        var validator = new GetAllTransactionsDtoValidator();
        var validationResult = await validator.ValidateAsync(request.GetAllTransactionsDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Input validation errors have occured";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var pageSize = request.GetAllTransactionsDto?.PageSize ?? 10;
        var currentPage = request.GetAllTransactionsDto?.CurrentPage ?? 1;

        var transactionsQuery = _transactionsRepository.AsQueryable();

        // This is to dynamically filter the transactions based on the query parameters
        var properties = request.GetAllTransactionsDto?.GetType().GetProperties();
        foreach (var property in properties!)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(request.GetAllTransactionsDto);

            if (propertyName == "Status" && !string.IsNullOrEmpty(propertyValue?.ToString()))
                transactionsQuery = transactionsQuery.Where(t => t.Status == propertyValue.ToString());

            if (propertyName == "Type" && !string.IsNullOrEmpty(propertyValue?.ToString()))
                transactionsQuery = transactionsQuery.Where(t => t.Type == propertyValue.ToString());

            if (propertyName == "MinAmount" && propertyValue != null)
                transactionsQuery = transactionsQuery.Where(t => t.Amount >= (decimal)propertyValue);

            if (propertyName == "MaxAmount" && propertyValue != null)
                transactionsQuery = transactionsQuery.Where(t => t.Amount <= (decimal)propertyValue);

            if (propertyName == "StartDate" && propertyValue != null)
            {
                var startDate = DateTime.ParseExact(propertyValue.ToString()!, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                transactionsQuery = transactionsQuery.Where(t => t.Date >= startDate);
            }

            if (propertyName == "EndDate" && propertyValue != null)
            {
                var endDate = DateTime.ParseExact(propertyValue.ToString()!, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                transactionsQuery = transactionsQuery.Where(t => t.Date <= endDate);
            }
        }

        var totalCount = await _transactionsRepository.CountAsync(transactionsQuery);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        var nextPage = currentPage < totalPages ? currentPage + 1 : (int?)null;
        var previousPage = currentPage > 1 ? currentPage - 1 : (int?)null;

        var transactions = await _transactionsRepository.GetAllTransactionsWithCategory(transactionsQuery, pageSize, currentPage);

        response.Succeeded = true;
        response.PageSize = pageSize;
        response.NextPage = nextPage;
        response.CurrentPage = currentPage;
        response.TotalCount = totalCount;
        response.TotalPages = totalPages;
        response.NextPage = nextPage;
        response.PreviousPage = previousPage;
        response.Data = _mapper.Map<IReadOnlyList<ReadTransactionDto>>(transactions);

        return response;
    }
}