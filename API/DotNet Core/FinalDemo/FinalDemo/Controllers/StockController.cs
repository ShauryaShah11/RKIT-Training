using FinalDemo.Enums;
using FinalDemo.Helpers;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authorization;
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
        private readonly JwtHelper _jwtHelper;

        /// <summary>
        /// Constructor to initialize StockController with the required stock service.
        /// </summary>
        /// <param name="stockService">The stock service to handle stock-related operations.</param>
        public StockController(IStockService stockService, JwtHelper jwtHelper)
        {
            _stockService = stockService;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// Endpoint to get all stocks.
        /// </summary>
        /// <returns>Returns a list of all stocks or an error message if retrieval fails.</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetAllStocks()
        {
            Response response = _stockService.GetAllStocks();
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
            Response response = _stockService.GetStockById(id);
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
            Response response = _stockService.GetStockByName(name);
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
        [Authorize]
        public IActionResult AddStock([FromBody] DTOYMS01 stock)
        {
            Response response;
            _stockService.SetOperationType(EnmOperationType.Add);
            YMS01 poco = _stockService.PreSave(stock);
            response = _stockService.ValidateOnSave(poco);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            response = _stockService.Save(poco);
            return CreatedAtAction(nameof(GetStockById), new { id = stock.S01F01 }, stock);
        }

        /// <summary>
        /// Endpoint to update an existing stock.
        /// </summary>
        /// <param name="id">ID of the stock to update.</param>
        /// <param name="stock">The updated stock data.</param>
        /// <returns>Returns the updated stock data or an error message if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateStock(int id, [FromBody] DTOYMS01 stock)
        {
            Response response;
            stock.S01F01 = id;
            _stockService.SetOperationType(EnmOperationType.Update);
            YMS01 poco = _stockService.PreSave(stock);
            response = _stockService.ValidateOnSave(poco);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            response = _stockService.Save(poco);
            return Ok(response.Data);
        }

        /// <summary>
        /// Endpoint to delete a stock by its ID.
        /// </summary>
        /// <param name="id">ID of the stock to delete.</param>
        /// <returns>Returns a success message or an error message if deletion fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteStock(int id)
        {
            Response response;
            DTOYMS01 stock = new DTOYMS01 { S01F01 = id };
            YMS01 poco = _stockService.PreDelete(stock);
            response = _stockService.ValidateOnDelete(poco);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            response = _stockService.Delete(poco);
            return Ok(response.Message);
        }
    }
}
