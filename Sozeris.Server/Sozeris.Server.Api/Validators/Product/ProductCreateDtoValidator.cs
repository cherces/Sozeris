using FluentValidation;
using Sozeris.Server.Api.DTO.Product;

namespace Sozeris.Server.Api.Validators.Product;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название продукта обязательно")
            .MaximumLength(50).WithMessage("Название продукта не должен быть длиннее 50 символов");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Цена продукта должна быть больше 0")
            .LessThan(decimal.MaxValue).WithMessage($"Цена продукта не может быть больше {decimal.MaxValue}");

        RuleFor(x => x.ImageBase64)
            .NotEmpty().WithMessage("Изображение обязательно")
            .Must(img => IsValidBase64(img)).WithMessage("Неправильный формат изображения")
            .Must(img => img.Length < 5_000_000).WithMessage("Изображние слишко большое(максимум 5 МБ)");
    }

    private bool IsValidBase64(string base64)
    {
        try
        {
            Convert.FromBase64String(base64);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}