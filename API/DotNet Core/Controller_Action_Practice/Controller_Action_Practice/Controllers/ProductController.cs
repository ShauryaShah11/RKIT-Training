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

        [HttpGet]
        public ActionResult<List<Product>> GetAllProduct()
        {
            return Ok(_products);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> GetProductById(int id)
        {
            Product? product = _products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody]Product product)
        {
            _products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, [FromBody]Product product)
        {
            Product? existingProduct = _products.FirstOrDefault(p =>  product.Id == id);
            if(existingProduct == null)
            {
                return NotFound("Product Not Found");
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            Product? product = _products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound("Product Not Found");
            }
            _products.Remove(product);
            return NoContent();
        }
    }
}
