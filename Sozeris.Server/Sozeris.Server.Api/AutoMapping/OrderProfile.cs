using AutoMapper;
using Sozeris.Server.Api.DTO.Order;
using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Api.AutoMapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

        CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Subscription, opt => opt.Ignore());
    }
}