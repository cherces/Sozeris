using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Api.Controllers;

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

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        var dtos = _mapper.Map<IEnumerable<ProductDTO>>(products);
        
        return Ok(dtos);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        
        if (product == null)
            return NotFound();

        return Ok(_mapper.Map<ProductDTO>(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> AddProduct([FromBody] ProductDTO dto)
    {
        if (dto == null)
            return BadRequest();

        var product = _mapper.Map<Product>(dto);
        var success = await _productService.AddProductAsync(product);

        if (!success)
            return BadRequest("Product creation failed.");

        return CreatedAtAction(nameof(GetProductById), new { productId = product.Id }, _mapper.Map<ProductDTO>(product));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromBody] ProductDTO dto)
    {
        if (dto == null)
            return BadRequest();

        var product = _mapper.Map<Product>(dto);
        var success = await _productService.UpdateProductAsync(product);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult> DeleteProductById(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
            return NotFound();

        var success = await _productService.DeleteProductByIdAsync(productId);
        if (!success)
            return BadRequest("Product deletion failed.");

        return NoContent();
    }
}