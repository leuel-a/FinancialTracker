using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Models;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Transaction.Validators;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Queries;

public class GetTransactionsSummaryForSingleMonthQueryHandler : IRequestHandler<
    GetTransactionsSummaryForSingleMonthQuery, GetTransactionsSummaryForSingleMonthResponse>
{
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionsSummaryForSingleMonthQueryHandler(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task<GetTransactionsSummaryForSingleMonthResponse> Handle(
        GetTransactionsSummaryForSingleMonthQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTransactionsSummaryForSingleMonthResponse();
        var validator = new GetTransactionsSummaryDtoValidator();
        var validationResult =
            await validator.ValidateAsync(request.GetTransactionsSummaryDto, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation errors have occured. Refer the errors property for more information.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return response;
        }

        var incomeTotal = await _transactionsRepository.GetIncomeTotalForMonth(
            request.GetTransactionsSummaryDto.Month!.Value, request.GetTransactionsSummaryDto.Year);
        var expenseTotal = await _transactionsRepository.GetExpenseTotalForMonth(
            request.GetTransactionsSummaryDto.Month.Value, request.GetTransactionsSummaryDto.Year);

        response.Succeeded = true;
        response.Message = "Monthly transaction summary has been successfully retrieved.";
        response.Summary = new MonthlyTransactionSummary
        {
            Month = request.GetTransactionsSummaryDto.Month.Value,
            Income = incomeTotal,
            Expense = expenseTotal,
            Total = incomeTotal - expenseTotal
        };
        return response;
    }
}