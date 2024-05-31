using FluentValidation;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Application.Dtos.Transaction.Validators;

public class UpdateTransactionDtoValidator : BaseTransactionDtoValidator<UpdateTransactionDto>
{
    public UpdateTransactionDtoValidator(ITransactionsRepository transactionsRepository)
    {
        RuleFor(p => p.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var transaction = await transactionsRepository.GetByIdAsync(id);
                return transaction != null;
            });
    }
}