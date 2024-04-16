using System;

using MediatR;

namespace ft.transaction_management.Application.Features.Transaction.Requests.Commands;

public class DeleteTransactionCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}