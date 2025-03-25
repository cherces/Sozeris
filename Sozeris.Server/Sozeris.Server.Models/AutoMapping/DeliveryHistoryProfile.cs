using AutoMapper;
using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Models.AutoMapping;

public class DeliveryHistoryProfile : Profile
{
    public DeliveryHistoryProfile()
    {
        CreateMap<DeliveryHistory, DeliveryHistoryDTO>().ReverseMap();
    }   
}