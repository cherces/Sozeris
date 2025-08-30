using FluentValidation;
using Sozeris.Server.Api.DTO.Subscription;

namespace Sozeris.Server.Api.Validators.Subscription;

public class SubscriptionCreateDtoValidator : AbstractValidator<SubscriptionCreateDto>
{
    public SubscriptionCreateDtoValidator()
    {
        RuleFor(x => x.StartDate)
            .GreaterThan(DateTime.UtcNow.Date).WithMessage("Дата начала не может быть текущим днем")
            .LessThan(x => x.EndDate).WithMessage("Дата начала должна быть меньше даты окончания");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("Дата окончания должна быть больше даты начала")
            .LessThanOrEqualTo(x => x.StartDate.AddYears(1)).WithMessage("Дата окончания не может быть дальше чем через год от даты начала");
        
        RuleFor(x => x.EndDate.Subtract(x.StartDate).Days)
            .Equal(30).WithMessage("Подписка должна длиться ровно 30 дней");
    }
}