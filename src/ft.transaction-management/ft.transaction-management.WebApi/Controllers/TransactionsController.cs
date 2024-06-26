using MediatR;
using Microsoft.AspNetCore.Mvc;
using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;
using ft.transaction_management.Application.Features.Transaction.Requests.Commands;
using ft.transaction_management.Application;

namespace ft.transaction_management.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all transactions
    /// </summary>
    /// <param name="currentPage">The current page for the query. Defaults to 1. </param>
    /// <param name="pageSize">The page size for the query. Defaults to 10. </param>
    /// <returns>An <see cref="IActionResult"/> containing the hypermedia paginated response.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllTransactions([FromQuery] GetAllTransactionsDto getAllTransactionsDto)
    {
        var response = await _mediator.Send(new GetAllTransactionsQuery()
            { GetAllTransactionsDto = getAllTransactionsDto });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });

        var value = new
        {
            response.Data,
            response.PageSize,
            response.CurrentPage,
            response.TotalCount,
            response.NextPage,
            response.PreviousPage,
            response.TotalPages
        };
        return Ok(value);
    }

    /// <summary>
    /// Gets a transaction by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the transaction.</param>
    /// <returns>An <see cref="IActionResult"/> containing the transaction.</returns>
    [HttpGet("{id:int}", Name = "GetTransactionById")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        var response = await _mediator.Send(new GetSingleTransactionQuery() { Id = id });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message });
        return Ok(response.Resource);
    }

    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    /// <param name="transactionDto">The transaction data transfer object.</param>
    /// <returns>An <see cref="IActionResult"/> containing the result of the creation operation.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto transactionDto)
    {
        var response = await _mediator.Send(new CreateTransactionCommand() { CreateTransactionDto = transactionDto });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });
        return CreatedAtRoute("GetTransactionById", new { Id = response.Id }, response.CreatedResource);
    }

    /// <summary>
    /// Updates an existing transaction.
    /// </summary>
    /// <param name="id">The identifier of the transaction to update.</param>
    /// <param name="transactionDto">The updated transaction data transfer object.</param>
    /// <returns>An <see cref="IActionResult"/> containing the result of the update operation.</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] UpdateTransactionDto transactionDto)
    {
        transactionDto.Id = id;
        var response = await _mediator.Send(new UpdateTransactionCommand() { TransactionDto = transactionDto });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });
        return Ok(new { response.Message });
    }

    /// <summary>
    /// Deletes a transaction by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the transaction to delete.</param>
    /// <returns>An <see cref="IActionResult"/> containing the result of the delete operation.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var response = await _mediator.Send(new DeleteTransactionCommand() { Id = id });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message });
        return Ok(new { response.Message });
    }

    /// <summary>
    /// Gets transactions with the specified category.
    /// </summary>
    /// <param name="categoryId">The identifier of the category to be queried.</param>
    /// <param name="transactionsByCategoryDto">Information about how to get the transactions,
    /// like page size and current page.</param>
    /// <returns>An <see cref="IActionResult"/> containing the result of the query operation.</returns>
    [HttpGet("categories/{categoryId:int}")]
    public async Task<IActionResult> GetTransactionsByCategory(
        int categoryId,
        [FromBody] GetTransactionsByCategoryDto transactionsByCategoryDto)
    {
        transactionsByCategoryDto.CategoryId = categoryId;
        var response = await _mediator.Send(new GetTransactionsByCategoryQuery()
            { TransactionByCategoryDto = transactionsByCategoryDto });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.ErrorMessages });

        var value = new
        {
            response.PageSize,
            response.NextPage,
            response.PreviousPage,
            response.CurrentPage,
            response.TotalPages,
            response.TotalCount,
            Transactions = response.Data
        };
        return Ok(value);
    }

    /// <summary>
    /// Gets the summary of transactions for each month of a specified year.
    /// </summary>
    /// <param name="year">The year for which the transaction summary is to be retrieved.</param>
    /// <returns>An IActionResult containing the monthly transaction summaries for the specified year.</returns>
    [HttpGet("year/{year:int}")]
    public async Task<IActionResult> GetTransactionsByMonthForYear(int year)
    {
        var response = await _mediator.Send(new GetTransactionsSummaryForSingleYearQuery()
            { GetTransactionsByMonthForYearDto = new GetTransactionsSummaryDto() { Year = year } });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, Errors = response.Errors });

        return Ok(response.MonthlySummary);
    }

    /// <summary>
    /// Gets the summary of transactions for a specific month of a specified year.
    /// </summary>
    /// <param name="year">The year for which the transaction summary is to be retrieved.</param>
    /// <param name="month">The month for which the transaction summary is to be retrieved.</param>
    /// <returns>An IActionResult containing the transaction summary for the specified month and year.</returns>
    [HttpGet("year/{year:int}/month/{month:int}")]
    public async Task<IActionResult> GetTransactionSummaryForSingleMonth(int year, int month)
    {
        var response = await _mediator.Send(new GetTransactionsSummaryForSingleMonthQuery()
        {
            GetTransactionsSummaryDto = new GetTransactionsSummaryDto() { Year = year, Month = month }
        });

        if (response.Succeeded == false)
            return BadRequest(new { response.Message, response.Errors });
        return Ok(new { response.Summary });
    }
    
    /// <summary>
    ///  Gets the summary of transactions for a specific day of a specific year
    /// </summary>
    /// <param name="year">The year for which the transaction summary is to be retrieved.</param>
    /// <param name="month">The month for which the transaction summary is to be retrieved.</param>
    /// <param name="day">The day for which the transaction summary is to be retrieved.</param>
    /// <returns>An IActionResult containing the transaction summary for the specified day, month and year.</returns>
    [HttpGet("year/{year:int}/month/{month:int}/day/{day:int}")]
    public async Task<IActionResult> GetTransactionsSummaryForSingleDay(int year, int month, int day)
    {
        var response = await _mediator.Send(new GetTransactionsSummaryForSingleDayQuery()
        {
            GetTransactionsSummaryDto = new GetTransactionsSummaryDto() { Year = year, Month = month, Day = day }
        });

        if (response.Succeeded == false)
            return BadRequest(new { response.ErrorMessage, response.Errors });
        return Ok(new { response.TotalAmount, response.Income, response.Expense });
    }
}