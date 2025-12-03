using CUSTOMERWEBAPI.DataAccess.Entities;
using CUSTOMERWEBAPI.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CUSTOMERWEBAPI.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/product/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/product/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new { Message = "Product not found" });

            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _productRepository.AddProductAsync(product);

            if (newId <= 0)
                return BadRequest(new { Message = "Product creation failed" });

            return CreatedAtAction(nameof(GetProductById), new { id = newId },
                new { ProductId = newId, Message = "Product created successfully" });
        }

        // PUT: api/product/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            product.prod_id = id;

            var success = await _productRepository.UpdateProductAsync(product);

            if (!success)
                return NotFound(new { Message = "Product not found or update failed" });

            return Ok(new { Message = "Product updated successfully" });
        }

        // DELETE: api/product/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productRepository.DeleteProductAsync(id);

            if (!success)
                return NotFound(new { Message = "Product not found or delete failed" });

            return Ok(new { Message = "Product deleted successfully" });
        }
    }
}
