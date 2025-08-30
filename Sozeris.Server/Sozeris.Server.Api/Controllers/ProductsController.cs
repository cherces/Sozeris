using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Product;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<ProductResponseDTO>>>> GetAllProducts(CancellationToken ct)
    {
        var products = await _productService.GetAllProductsAsync(ct);
        var dto = _mapper.Map<List<ProductResponseDTO>>(products);
        
        return Ok(ApiResponse<List<ProductResponseDTO>>.Ok(dto));
    }

    [HttpGet("{productId:int}")]
    public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> GetProductById(int productId, CancellationToken ct)
    {
        var result = await _productService.GetProductByIdAsync(productId, ct);

        return result.Match(
            onSuccess: product => _mapper.Map<ProductResponseDTO>(product).ToApiResponse(this),
            onFailure: error => error.ToApiResponse<ProductResponseDTO>(this)
        );
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> AddProduct([FromBody] ProductCreateDTO productDto, CancellationToken ct)
    {
        var product = _mapper.Map<Product>(productDto);
        var result = await _productService.AddProductAsync(product, ct);

        return result.Match(
            onSuccess: created => _mapper.Map<ProductResponseDTO>(created).ToApiResponse(this),
            onFailure: error => error.ToApiResponse<ProductResponseDTO>(this)
        );
    }

    [HttpPut("{productId:int}")]
    public async Task<ActionResult<ApiResponse>> UpdateProduct(int productId, [FromBody] ProductUpdateDTO productDto, CancellationToken ct)
    {
        var product = _mapper.Map<Product>(productDto);
        product.Id = productId;
        var result = await _productService.UpdateProductAsync(productId, product, ct);

        return result.Match(
            onSuccess: () => this.ToApiResponse(),
            onFailure: error => error.ToApiResponse(this)
        );
    }

    [HttpDelete("{productId:int}")]
    public async Task<ActionResult<ApiResponse>> DeleteProductById(int productId, CancellationToken ct)
    {
        var result = await _productService.DeleteProductByIdAsync(productId, ct);

        return result.Match(
            onSuccess: () => this.ToApiResponse(),
            onFailure: error => error.ToApiResponse(this)
        );
    }
}