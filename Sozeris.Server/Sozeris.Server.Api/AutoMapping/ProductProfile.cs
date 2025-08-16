using AutoMapper;
using Sozeris.Server.Api.DTO.Product;
using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Api.AutoMapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreateDTO, Product>()
            .ForMember(dest => dest.Image, 
                opt => opt.MapFrom(src => Convert.FromBase64String(src.ImageBase64)));

        CreateMap<ProductUpdateDTO, Product>()
            .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ImageBase64) 
                    ? null 
                    : Convert.FromBase64String(src.ImageBase64)));

        CreateMap<Product, ProductResponseDTO>()
            .ForMember(dest => dest.ImageBase64, 
                opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)));
    }
}