using FluentValidation;
using Sozeris.Server.Api.DTO.User;

namespace Sozeris.Server.Api.Validators.User;

public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
{
    public UserUpdateDTOValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Некорректный Id");
        
        When(x => !string.IsNullOrEmpty(x.Login), () =>
        {
            RuleFor(x => x.Login)
                .MinimumLength(8).WithMessage("Логин должен быть не меньше 8 символа");
        });

        When(x => !string.IsNullOrEmpty(x.Password), () =>
        {
            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Пароль должен быть не меньше 8 символов");
        });

        When(x => !string.IsNullOrEmpty(x.Phone), () =>
        {
            RuleFor(x => x.Phone)
                .Matches(@"^\+?[1-9]\d{9,14}$")
                .WithMessage("Телефон должен быть в формате +71234567890");
        });
        
        When(x => !string.IsNullOrEmpty(x.Address), () =>
        {
            RuleFor(x => x.Address)
                .MaximumLength(100)
                .WithMessage("Слишком длинный адрес");
        });
    }
}