using MediatR;
using Microsoft.AspNetCore.Mvc;
using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Features.Transaction.Requests.Queries;
using ft.transaction_management.Application.Features.Transaction.Requests.Commands;

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
    /// Gets a transaction by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the transaction.</param>
    /// <returns>An <see cref="IActionResult"/> containing the transaction.</returns>
    [HttpGet("{id:int}", Name = "GetTransactionById")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        var response = await _mediator.Send(new GetSingleTransactionRequest() { Id = id });

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
}