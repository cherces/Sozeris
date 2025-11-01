using AutoMapper;
using Sozeris.Server.Api.DTO.Delivery;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Models;

namespace Sozeris.Server.Api.AutoMapping;

public class DeliveryProfile : Profile
{
    public DeliveryProfile()
    {
        CreateMap<DeliveryHistory, DeliveryForHistoryDto>().ReverseMap();
        
        CreateMap<DeliveryItem, DeliveryItemDto>().ReverseMap();
        CreateMap<Delivery, DeliveryForDayDto>()
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items))
            .ReverseMap();
    }
}