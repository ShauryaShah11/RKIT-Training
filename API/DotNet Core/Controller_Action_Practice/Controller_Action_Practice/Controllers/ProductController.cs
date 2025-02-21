using Controller_Action_Practice.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controller_Action_Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private static readonly List<Product> _products = new List<Product>();

        public ProductController()
        {
            if (!_products.Any())
            {
                _products.Add(new Product { Id = 1, Name = "Laptop", Price = 800 });
                _products.Add(new Product { Id = 2, Name = "Phone", Price = 500 });
                _products.Add(new Product { Id = 3, Name = "TV", Price = 5000 });
            }
        }

        /// <summary>
        /// Gets all available products.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return Ok(_products);
        }

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (_products.Any(p => p.Id == product.Id))
            {
                return BadRequest("Product with the same ID already exists.");
            }

            _products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return NoContent();
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            _products.Remove(product);
            return NoContent();
        }
    }
}
