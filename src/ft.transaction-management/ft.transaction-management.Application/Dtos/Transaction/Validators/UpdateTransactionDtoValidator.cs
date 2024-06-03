using FluentValidation;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Application.Dtos.Transaction.Validators;

public class UpdateTransactionDtoValidator : BaseTransactionDtoValidator<UpdateTransactionDto>
{
    public UpdateTransactionDtoValidator(ITransactionsRepository transactionsRepository,
        ICategoriesRepository categoriesRepository)
    {
        RuleFor(p => p.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var transaction = await transactionsRepository.GetByIdAsync(id);
                return transaction != null;
            }).WithMessage(p => $"Transaction with id: {p.Id} can not be found.");

        RuleFor(p => p.CategoryId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (categoryId, cancellationToken) =>
            {
                var category = await categoriesRepository.GetByIdAsync(categoryId!.Value);
                return category != null;
            }).When(p => p.CategoryId.HasValue)
            .WithMessage(p => $"Category with id: {p.CategoryId} not found.");
    }
}