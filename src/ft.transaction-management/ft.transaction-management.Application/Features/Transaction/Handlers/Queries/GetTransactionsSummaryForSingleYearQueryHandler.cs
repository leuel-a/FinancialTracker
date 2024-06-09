using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Transaction.Validators;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;
using ft.transaction_management.Application.Models;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class GetTransactionsSummaryForSingleYearQueryHandler : IRequestHandler<GetTransactionsSummaryForSingleYearQuery,
    GetTransactionsByMonthForYearResponse>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionsSummaryForSingleYearQueryHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task<GetTransactionsByMonthForYearResponse> Handle(GetTransactionsSummaryForSingleYearQuery request,
        CancellationToken cancellationToken)
    {
        var response = new GetTransactionsByMonthForYearResponse();
        var validator = new GetTransactionsSummaryDtoValidator();
        var validationResult =
            await validator.ValidateAsync(request.GetTransactionsByMonthForYearDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation errors have occured. Refer to the errors property for more details.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }
        
        for (var i = 0; i < 12; i++)
        {
            var incomeTotal = await _transactionsRepository.GetIncomeTotalForMonth(i + 1,
                request.GetTransactionsByMonthForYearDto!.Year);
            var expenseTotal = await _transactionsRepository.GetExpenseTotalForMonth(i + 1,
                request.GetTransactionsByMonthForYearDto!.Year);
            
            response.MonthlySummary.Add(new MonthlyTransactionSummary
            {
                Month = i,
                Income = incomeTotal,
                Expense = expenseTotal,
                Total = incomeTotal - expenseTotal
            });
        }

        response.Succeeded = true;
        response.Message = "Monthly transaction summary has been successfully retrieved.";
        
        return response;
    }
}