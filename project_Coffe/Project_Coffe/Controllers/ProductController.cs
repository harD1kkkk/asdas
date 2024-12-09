using Microsoft.AspNetCore.Mvc;
using Project_Coffe.Entities;
using Project_Coffe.Models.ModelInterface;

namespace Project_Coffe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                List<Product> products = (await _productService.GetAllProducts()).ToList();
                _logger.LogInformation("Fetched all products successfully.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching products: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching the products.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                Product? product = await _productService.GetProductById(id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {id} not found.");
                    return NotFound("Product not found.");
                }

                _logger.LogInformation($"Fetched product with ID {id} successfully.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching product with ID {id}: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching the product.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Product creation failed: Invalid model state.");
                    return BadRequest(ModelState);
                }

                await _productService.CreateProduct(product);
                _logger.LogInformation($"Product with ID {product.Id} created successfully.");
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid || id != product.Id)
                {
                    _logger.LogWarning($"Update failed: Invalid model or ID mismatch for product {id}.");
                    return BadRequest(ModelState);
                }

                await _productService.UpdateProduct(product);
                _logger.LogInformation($"Product with ID {id} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product with ID {id}: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                _logger.LogInformation($"Product with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product with ID {id}: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }

    }
}
