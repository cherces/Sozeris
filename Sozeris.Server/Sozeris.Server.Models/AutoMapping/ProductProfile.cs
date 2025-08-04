using AutoMapper;
using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Models.AutoMapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null));

        CreateMap<ProductDTO, Product>()
            .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Image) ? Convert.FromBase64String(src.Image) : null));
    }
}