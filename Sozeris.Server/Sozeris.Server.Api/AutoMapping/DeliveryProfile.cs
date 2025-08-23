using AutoMapper;
using Sozeris.Server.Api.DTO.Delivery;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Models;

namespace Sozeris.Server.Api.AutoMapping;

public class DeliveryProfile : Profile
{
    public DeliveryProfile()
    {
        CreateMap<DeliveryHistory, DeliveryForHistoryDTO>().ReverseMap();
        
        CreateMap<DeliveryItem, DeliveryItemDTO>().ReverseMap();
        CreateMap<Delivery, DeliveryForDayDTO>()
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items))
            .ReverseMap();
    }
}