using Sozeris.Server.Logic.Services.Interfaces;

namespace Sozeris.Server.Logic.Services;

public class ProductService : IProductService
{
    private readonly IProductService _productService;

    public ProductService(IProductService productService)
    {
        _productService = productService;
    }
}