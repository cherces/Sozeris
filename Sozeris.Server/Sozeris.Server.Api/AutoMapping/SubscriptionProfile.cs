using AutoMapper;
using Sozeris.Server.Api.DTO;
using Sozeris.Server.Api.DTO.Subscription;
using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Api.AutoMapping;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<Subscription, SubscriptionResponseDto>();
        
        CreateMap<SubscriptionCreateDto, Subscription>()
            .ForMember(dest => dest.Id, 
                opt => opt.Ignore())
            .ForMember(dest => dest.Orders, 
                opt => opt.MapFrom(src => src.Orders));
    }
}