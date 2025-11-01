using FluentValidation;
using Sozeris.Server.Api.DTO.Product;

namespace Sozeris.Server.Api.Validators.Product;

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Некорректный Id");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название продукта обязательно")
            .MaximumLength(50).WithMessage("Название продукта не должен быть длиннее 50 символов");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Цена продукта должна быть больше 0")
            .LessThan(decimal.MaxValue).WithMessage($"Цена продукта не может быть больше {decimal.MaxValue}");
    
        RuleFor(x => x.ImageBase64)
            .Must(img => IsValidBase64(img)).WithMessage("Некорректное изображение")
            .Must(img => img!.Length < 5_000_000).WithMessage("Изображение слишком большое (максимум 5 МБ)");
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