using AutoMapper;
using Sozeris.Server.Api.DTO.Product;
using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Api.AutoMapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Image, 
                opt => opt.MapFrom(src => Convert.FromBase64String(src.ImageBase64)));

        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ImageBase64) 
                    ? null 
                    : Convert.FromBase64String(src.ImageBase64)));

        CreateMap<Product, ProductResponseDto>()
            .ConstructUsing(src => new ProductResponseDto(
                src.Id,
                src.Name,
                src.Price,
                null, // ImageBase64 всегда null(пока что)
                src.IsActive
            ));
    }
}