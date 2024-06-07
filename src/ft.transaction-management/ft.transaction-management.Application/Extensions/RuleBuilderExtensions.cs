using System;
using FluentValidation;
using FluentValidation.Validators;

namespace ft.transaction_management.Application.Extensions;

public class EnumPropertyValidator<T, TEnum> : PropertyValidator<T, string?>
{
    public override string Name => "EnumPropertyValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        return Enum.IsDefined(typeof(TEnum), value!);
    }
}

public static class EnumPropertyValidatorExtensions
{
    public static IRuleBuilderOptions<T, string?> IsValidEnum<T, TEnum>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new EnumPropertyValidator<T, TEnum>());
    }
}