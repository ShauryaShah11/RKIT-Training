using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller to manage stock operations such as retrieval, addition, update, and deletion of stock records.
    /// Provides endpoints for handling stock data.
    /// </summary>
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        /// <summary>
        /// Constructor to initialize StockController with the required stock service.
        /// </summary>
        /// <param name="stockService">The stock service to handle stock-related operations.</param>
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        /// <summary>
        /// Endpoint to get all stocks.
        /// </summary>
        /// <returns>Returns a list of all stocks or an error message if retrieval fails.</returns>
        [HttpGet]
        public IActionResult GetAllStocks()
        {
            var response = _stockService.GetAllStocks();
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        /// <summary>
        /// Endpoint to get a stock by its ID.
        /// </summary>
        /// <param name="id">ID of the stock to retrieve.</param>
        /// <returns>Returns the stock data or a not found response if the stock doesn't exist.</returns>
        [HttpGet("{id}")]
        public IActionResult GetStockById(int id)
        {
            var response = _stockService.GetStockById(id);
            if (response.IsError)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        /// <summary>
        /// Endpoint to get a stock by its name.
        /// </summary>
        /// <param name="name">Name of the stock to retrieve.</param>
        /// <returns>Returns the stock data or a not found response if no stock with that name is found.</returns>
        [HttpGet("name/{name}")]
        public IActionResult GetStockByName(string name)
        {
            var response = _stockService.GetStockByName(name);
            if (response.IsError)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        /// <summary>
        /// Endpoint to add a new stock.
        /// </summary>
        /// <param name="stock">The stock data to add.</param>
        /// <returns>Returns the created stock or an error message if the addition fails.</returns>
        [HttpPost]
        public IActionResult AddStock([FromBody] DTOYMS01 stock)
        {
            var response = _stockService.HandleOperation(stock, EnmOperationType.Add);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetStockById), new { id = ((YMS01)response.Data).S01F01 }, response.Data);
        }

        /// <summary>
        /// Endpoint to update an existing stock.
        /// </summary>
        /// <param name="id">ID of the stock to update.</param>
        /// <param name="stock">The updated stock data.</param>
        /// <returns>Returns the updated stock data or an error message if the update fails.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateStock(int id, [FromBody] DTOYMS01 stock)
        {
            if (id != stock.S01F01) // Assuming S01F01 is the ID
            {
                return BadRequest("Stock ID mismatch");
            }

            var response = _stockService.HandleOperation(stock, EnmOperationType.Update);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        /// <summary>
        /// Endpoint to delete a stock by its ID.
        /// </summary>
        /// <param name="id">ID of the stock to delete.</param>
        /// <returns>Returns a success message or an error message if deletion fails.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteStock(int id)
        {
            var stock = new DTOYMS01 { S01F01 = id }; // Assuming you are passing ID to delete
            var response = _stockService.HandleOperation(stock, EnmOperationType.Delete);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }
    }
}
