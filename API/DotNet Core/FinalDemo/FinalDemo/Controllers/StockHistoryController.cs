using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IStockPriceHistoryService _stockPriceHistoryService;

        /// <summary>
        /// Constructor to initialize StockHistoryController with the required stock price history service.
        /// </summary>
        /// <param name="stockPriceHistoryService">Stock price history service to handle operations related to stock price history records.</param>
        public StockHistoryController(IStockPriceHistoryService stockPriceHistoryService)
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
            Response response = _stockPriceHistoryService.GetAllStockPriceHistory();
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
            Response response = _stockPriceHistoryService.GetStockPriceHistoryById(id);
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
            Response response = _stockPriceHistoryService.GetStockPriceHistoryByStockId(stockId);
            if (response.IsError)
            {
                return NotFound(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to get min stock price history records by stock ID.
        /// </summary>
        /// <param name="stockId">ID of the stock to retrieve the price history for.</param>
        /// <returns>Returns the stock price history records or a not found response if no records are found.</returns>
        [HttpGet("max/{stockId}")]
        public IActionResult GetMaxStockPrice(int stockId)
        {
            Response response = _stockPriceHistoryService.GetMaxStockPriceDate(stockId);
            if (response.IsError)
            {
                return NotFound(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to get max stock price history records by stock ID.
        /// </summary>
        /// <param name="stockId">ID of the stock to retrieve the price history for.</param>
        /// <returns>Returns the stock price history records or a not found response if no records are found.</returns>
        [HttpGet("min/{stockId}")]
        public IActionResult GetMinStockPrice(int stockId)
        {
            Response response = _stockPriceHistoryService.GetMinStockPriceDate(stockId);
            if (response.IsError)
            {
                return NotFound(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to add a new stock price history record.
        /// </summary>
        /// <param name="dto">DTO containing the stock price history data to add.</param>
        /// <returns>Returns the created stock price history record or an error response if the addition fails.</returns>
        [HttpPost]
        [Authorize]
        public IActionResult AddStockPriceHistory([FromBody] DTOYMH01 dto)
        {
            Response response;
            _stockPriceHistoryService.SetOperationType(EnmOperationType.Add);
            YMH01 poco = _stockPriceHistoryService.PreSave(dto);
            response = _stockPriceHistoryService.ValidateOnSave(poco);
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }
            response = _stockPriceHistoryService.Save(poco);
            return CreatedAtAction(nameof(GetStockPriceHistoryById), new { id = dto.H01F01 }, dto);
        }

        /// <summary>
        /// Endpoint to update an existing stock price history record.
        /// </summary>
        /// <param name="id">ID of the stock price history record to update.</param>
        /// <param name="dto">DTO containing the updated stock price history data.</param>
        /// <returns>Returns the updated stock price history record or an error response if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateStockPriceHistory(int id, [FromBody] DTOYMH01 dto)
        {
            Response response;
            dto.H01F01 = id;
            _stockPriceHistoryService.SetOperationType(EnmOperationType.Update);
            YMH01 poco = _stockPriceHistoryService.PreSave(dto);
            response = _stockPriceHistoryService.ValidateOnSave(poco);
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }
            response = _stockPriceHistoryService.Save(poco);
            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to delete a stock price history record by ID.
        /// </summary>
        /// <param name="id">ID of the stock price history record to delete.</param>
        /// <returns>Returns a success message or an error response if the deletion fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteStockPriceHistory(int id)
        {
            DTOYMH01 dto = new DTOYMH01 { H01F01 = id }; // Assuming H01F01 is the ID field
            _stockPriceHistoryService.SetOperationType(EnmOperationType.Delete);
            YMH01 poco = _stockPriceHistoryService.PreDelete(dto);
            Response response = _stockPriceHistoryService.ValidateOnDelete(poco);
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }
            response = _stockPriceHistoryService.Delete(poco);
            return Ok(new { Message = response.Message });
        }
    }
}