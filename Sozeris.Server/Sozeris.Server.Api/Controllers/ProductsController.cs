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
    public async Task<ActionResult<ApiResponse<List<ProductResponseDTO>>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        var dto = _mapper.Map<List<ProductResponseDTO>>(products);
        
        return Ok(ApiResponse<List<ProductResponseDTO>>.Ok(dto));
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> GetProductById(int productId)
    {
        var result = await _productService.GetProductByIdAsync(productId);
        if (!result.Success) return NotFound(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));
        
        var dto = _mapper.Map<ProductResponseDTO>(result.Value);
        
        return Ok(ApiResponse<ProductResponseDTO>.Ok(dto));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> CreateProduct([FromBody] ProductCreateDTO productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.Fail(ModelState));
        
        var product = _mapper.Map<Product>(productDto);
        
        var result = await _productService.AddProductAsync(product);
        if (!result.Success) return BadRequest(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));
        
        var response = _mapper.Map<ProductResponseDTO>(result.Value);

        return CreatedAtAction(nameof(GetProductById), new { productId = result.Value.Id }, response);
    }

    [HttpPut("{productId}")]
    public async Task<ActionResult<ApiResponse>> UpdateProduct(int productId, [FromBody] ProductUpdateDTO productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.Fail(ModelState));
        
        var product = _mapper.Map<Product>(productDto);
        product.Id = productId;
        
        var result = await _productService.UpdateProductAsync(productId, product);
        if (!result.Success) return BadRequest(ApiResponse.Fail(new Exception(result.ErrorMessage)));

        return Ok(ApiResponse.Ok());
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult<ApiResponse>> DeleteProductById(int productId)
    {
        var result = await _productService.DeleteProductByIdAsync(productId);
        if (!result.Success) return NotFound(ApiResponse.Fail(new Exception(result.ErrorMessage)));
        
        return Ok(ApiResponse.Ok());
    }
}