using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// The ProductsController class represents a controller for managing products.
    /// It has various api endpoint to perform CRUD operations on the product data.
    /// </summary>
    public class ProductsController : ApiController
    {
        private readonly ProductRepository _repository = new ProductRepository();

        // GET api/products
        [HttpGet]
        public IEnumerable<Product> Get() => _repository.GetAll();

        // GET api/products/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var product = _repository.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        public IHttpActionResult Post([FromBody] Product product)
        {
            if (product == null) return BadRequest("Invalid data.");
            _repository.Add(product);
            return Ok(product);
        }

        // PUT api/products/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Product product)
        {
            if (product == null || product.Id != id) return BadRequest("Invalid data.");
            _repository.Update(product);
            return Ok(product);
        }

        // DELETE api/products/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var product = _repository.GetById(id);
            if (product == null) return NotFound();
            _repository.Delete(id);
            return Ok();
        }
    }

}
