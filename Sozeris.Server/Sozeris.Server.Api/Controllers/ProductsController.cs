using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Product;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Domain.Entities;
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
    public async Task<ActionResult<ApiResponse<List<ProductResponseDto>>>> GetAllProducts(CancellationToken ct)
    {
        var products = await _productService.GetAllProductsAsync(ct);
        var dto = _mapper.Map<List<ProductResponseDto>>(products);
        
        return Ok(ApiResponse<List<ProductResponseDto>>.Ok(dto));
    }

    [HttpGet("{productId:int}")]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> GetProductById(int productId, CancellationToken ct)
    {
        var result = await _productService.GetProductByIdAsync(productId, ct);

        if (result.IsSuccess)
            return _mapper.Map<ApiResponse<ProductResponseDto>>(result.Value);
        
        return result.Error.ToApiResponse<ProductResponseDto>(HttpContext);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> AddProduct([FromBody] ProductCreateDto productDto, CancellationToken ct)
    {
        var product = _mapper.Map<Product>(productDto);
        var result = await _productService.AddProductAsync(product, ct);

        if (result.IsSuccess)
            return _mapper.Map<ApiResponse<ProductResponseDto>>(result.Value);
        
        return result.Error.ToApiResponse<ProductResponseDto>(HttpContext);
    }

    [HttpPut("{productId:int}")]
    public async Task<ActionResult<ApiResponse>> UpdateProduct(int productId, [FromBody] ProductUpdateDto productDto, CancellationToken ct)
    {
        var product = _mapper.Map<Product>(productDto);
        product.Id = productId;
        var result = await _productService.UpdateProductAsync(productId, product, ct);

        if (result.IsSuccess)
            return ApiResponse.Ok();
        
        return result.Error.ToApiResponse(HttpContext);
    }

    [HttpDelete("{productId:int}")]
    public async Task<ActionResult<ApiResponse>> DeleteProductById(int productId, CancellationToken ct)
    {
        var result = await _productService.DeleteProductByIdAsync(productId, ct);
        
        if (result.IsSuccess)
            return ApiResponse.Ok();
        
        return result.Error.ToApiResponse(HttpContext);
    }
}