using FluentValidation;
using Sozeris.Server.Api.DTO.User;

namespace Sozeris.Server.Api.Validators.User;

public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
{
    public UserCreateDTOValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Логин обязателен")
            .MinimumLength(8).WithMessage("Логин должен быть не меньше 8 символов");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(8).WithMessage("Пароль должен быть не меньше 8 символов");

        RuleFor(x => x.Phone)
            .Matches(@"^\+?[1-9]\d{9,14}$")
            .WithMessage("Телефон должен быть в формате +71234567890");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Адрес обязателен");
    }
}