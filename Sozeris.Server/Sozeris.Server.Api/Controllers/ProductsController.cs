using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<Product>> GetProductById(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        
        if (product == null)
            return NotFound();
        
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct([FromBody] Product? product)
    {
        if (product == null)
            return BadRequest();
        
        var success = await _productService.AddProductAsync(product);
        
        if(!success)
            return BadRequest("Product creation failed.");
        
        return CreatedAtAction(nameof(GetProductById), new { productId = product.Id }, product);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromBody] Product? product)
    {
        if (product == null)
            return BadRequest();
        
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