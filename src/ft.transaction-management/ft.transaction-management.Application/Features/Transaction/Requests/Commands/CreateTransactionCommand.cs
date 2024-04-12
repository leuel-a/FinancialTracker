using ft.transaction_management.Application.DTOs.TransactionDto;
using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Commands;

public class CreateTransactionCommand : IRequest<ReadTransactionDto>
{
    public CreateTransactionDto TransactionDto { get; set; }
}