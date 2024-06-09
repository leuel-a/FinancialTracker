using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Transaction.Validators;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;
using ft.transaction_management.Application.Responses;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class GetTransactionsSummaryForSingleDayQueryHandler : IRequestHandler<GetTransactionsSummaryForSingleDayQuery, TransactionSummaryForSingleDayResponse>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionsSummaryForSingleDayQueryHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }
    
    public async Task<TransactionSummaryForSingleDayResponse> Handle(GetTransactionsSummaryForSingleDayQuery request, CancellationToken cancellationToken)
    {
        var response = new TransactionSummaryForSingleDayResponse();
        var validator = new GetTransactionsSummaryDtoValidator();
        var validationResult = await validator.ValidateAsync(request.GetTransactionsSummaryDto, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.ErrorMessage = "Validation error has occured. Check the errors property for more information.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return response;
        }

        var query = _transactionsRepository.AsQueryable();

        var day = request.GetTransactionsSummaryDto.Day;
        var month = request.GetTransactionsSummaryDto.Month;
        var year = request.GetTransactionsSummaryDto.Year;
        
        // Filter transactions by day, month and year
        query = query.Where(t => t.Date.Day == day && t.Date.Month == month && t.Date.Year == year);
        var transactions = await _transactionsRepository.GetAllTransactionsWithCategory(query, 1000, 0);
        
        var incomeTotal = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
        var expenseTotal = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);
        var total = incomeTotal - expenseTotal;

        response.Succeeded = true;
        response.ErrorMessage = "Daily transaction summary has been successfully retrieved.";
        response.TotalAmount = total;
        response.Income = incomeTotal;
        response.Expense = expenseTotal;
        
        return response;
    }
}