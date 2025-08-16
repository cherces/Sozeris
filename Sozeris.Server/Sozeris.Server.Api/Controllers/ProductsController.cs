using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Product;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Services;

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
    public async Task<ActionResult<List<ProductResponseDTO>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        
        return Ok(_mapper.Map<List<ProductResponseDTO>>(products));
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductResponseDTO>> GetProductById(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        return product is null ? NotFound() : Ok(_mapper.Map<ProductResponseDTO>(product));
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] ProductCreateDTO productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var product = _mapper.Map<Product>(productDto);
        var createdProduct = await _productService.AddProductAsync(product);
        
        if (createdProduct is null)
            return BadRequest("Product creation failed.");

        return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{productId}")]
    public async Task<ActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateDTO productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var product = _mapper.Map<Product>(productDto);
        product.Id = productId;
        
        var success = await _productService.UpdateProductAsync(productId, product);
        
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult> DeleteProductById(int productId)
    {
        var success = await _productService.DeleteProductByIdAsync(productId);
        return success ? NoContent() : NotFound();
    }
}