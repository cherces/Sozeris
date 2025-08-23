using FluentValidation;
using Sozeris.Server.Api.DTO.Delivery;
using Sozeris.Server.Domain.Enums;

namespace Sozeris.Server.Api.Validators.Delivery;

public class DeliveryMarkRequestDTOValidator : AbstractValidator<DeliveryMarkRequestDTO>
{
    public DeliveryMarkRequestDTOValidator()
    {
        RuleFor(x => x.Status).IsInEnum();

        When(x => x.Status is DeliveryStatus.NotDelivered, () =>
        {
            RuleFor(y => y.Reason).NotEmpty().WithMessage("Reason is required when status is NotDelivered.");
        });
    }
}