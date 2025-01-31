using FinalDemo.Enums;
using FinalDemo.Models.DTO;
using FinalDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller to manage stock price history operations including retrieval, addition, update, and deletion of stock price history records.
    /// </summary>
    [Route("api/stock-history")]
    [ApiController]
    public class StockHistoryController : ControllerBase
    {
        private readonly StockPriceHistoryService _stockPriceHistoryService;

        /// <summary>
        /// Constructor to initialize StockHistoryController with the required stock price history service.
        /// </summary>
        /// <param name="stockPriceHistoryService">Stock price history service to handle operations related to stock price history records.</param>
        public StockHistoryController(StockPriceHistoryService stockPriceHistoryService)
        {
            _stockPriceHistoryService = stockPriceHistoryService;
        }

        /// <summary>
        /// Endpoint to get all stock price history records.
        /// </summary>
        /// <returns>Returns all stock price history records or an error response if retrieval fails.</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAllStockPriceHistory()
        {
            var response = _stockPriceHistoryService.GetAllStockPriceHistory();
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to get a specific stock price history record by ID.
        /// </summary>
        /// <param name="id">ID of the stock price history record.</param>
        /// <returns>Returns the stock price history record or a not found response if the record doesn't exist.</returns>
        [HttpGet("GetById/{id}")]
        public IActionResult GetStockPriceHistoryById(int id)
        {
            var response = _stockPriceHistoryService.GetStockPriceHistoryById(id);
            if (response.IsError)
            {
                return NotFound(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to get stock price history records by stock ID.
        /// </summary>
        /// <param name="stockId">ID of the stock to retrieve the price history for.</param>
        /// <returns>Returns the stock price history records or a not found response if no records are found.</returns>
        [HttpGet("GetByStockId/{stockId}")]
        public IActionResult GetStockPriceHistoryByStockId(int stockId)
        {
            var response = _stockPriceHistoryService.GetStockPriceHistoryByStockId(stockId);
            if (response.IsError)
            {
                return NotFound(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to save (add or update) stock price history record.
        /// </summary>
        /// <param name="dto">DTO containing the stock price history data to save.</param>
        /// <returns>Returns a success message or an error response if the operation fails.</returns>
        [HttpPost("Save")]
        public IActionResult SaveStockPriceHistory([FromBody] DTOYMH01 dto)
        {
            var response = _stockPriceHistoryService.HandleOperation(dto, EnmOperationType.Add);
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message });
        }

        /// <summary>
        /// Endpoint to delete a stock price history record by ID.
        /// </summary>
        /// <param name="id">ID of the stock price history record to delete.</param>
        /// <returns>Returns a success message or an error response if the deletion fails.</returns>
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteStockPriceHistory(int id)
        {
            var dto = new DTOYMH01 { H01F01 = id }; // Assuming H01F01 is the ID field
            var response = _stockPriceHistoryService.HandleOperation(dto, EnmOperationType.Delete);
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message });
        }
    }
}

