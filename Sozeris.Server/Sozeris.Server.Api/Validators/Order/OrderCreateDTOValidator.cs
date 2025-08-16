using FluentValidation;
using Sozeris.Server.Api.DTO.Order;

namespace Sozeris.Server.Api.Validators.Order;

public class OrderCreateDTOValidator : AbstractValidator<OrderCreateDTO>
{
    public OrderCreateDTOValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Некорректный Id продукта");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Количество должно быть больше 0")
            .LessThan(0).WithMessage("Количество должно быть меньше 99");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Цена должна быть больше нуля")
            .LessThan(decimal.MaxValue).WithMessage($"Цена должна быть меньше {decimal.MaxValue}");
    }
}