using FluentValidation;
using System.Diagnostics;
using ft.transaction_management.Application.Contracts.Persistence;

namespace ft.transaction_management.Application.Dtos.Transaction.Validators;

public class CreateTransactionDtoValidator : BaseTransactionDtoValidator<CreateTransactionDto>
{
    public CreateTransactionDtoValidator(ICategoriesRepository categoriesRepository)
    {
        RuleFor(p => p.CategoryId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (id, token) =>
            {
                Debug.Assert(id != null, nameof(id) + " != null");
                var categories = await categoriesRepository.GetByIdAsync(id.Value);
                return categories != null;
            }).WithMessage("{PropertyName} doest not exist in the database.")
            .When(p => p.CategoryId.HasValue);
    }
}
