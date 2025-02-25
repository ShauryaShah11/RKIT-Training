using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller to manage orderbook operations such as retrieval, addition, update, and deletion of orders.
    /// Provides endpoints for handling order data.
    /// </summary>
    [Route("api/orders")]
    [ApiController]
    public class OrderbookController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Constructor to initialize OrderbookController with the required order service.
        /// </summary>
        /// <param name="orderService">The order service to handle order-related operations.</param>
        public OrderbookController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Endpoint to get all orders.
        /// </summary>
        /// <returns>Returns a list of all orders or an error message if retrieval fails.</returns>
        [HttpGet("all")]
        public IActionResult GetAllOrders()
        {
            Response response = _orderService.GetAllOrder();
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        /// <summary>
        /// Endpoint to get an order by its ID.
        /// </summary>
        /// <param name="id">ID of the order to retrieve.</param>
        /// <returns>Returns the order data or a not found response if the order doesn't exist.</returns>
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            Response response = _orderService.GetOrderById(id);
            if (response.IsError)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        /// <summary>
        /// Endpoint to add a new order.
        /// </summary>
        /// <param name="dto">The order data to add.</param>
        /// <returns>Returns the created order or an error message if the addition fails.</returns>
        [HttpPost("add")]
        public IActionResult AddOrder([FromBody] DTOYMO01 dto)
        {
            Response response;
            _orderService.SetOperationType(EnmOperationType.Add);
            YMO01 poco = _orderService.PreSave(dto);
            response = _orderService.ValidateOnSave(poco);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetOrderById), new { id = (dto.O01F01) }, response.Message);
        }

        /// <summary>
        /// Endpoint to update an existing order.
        /// </summary>
        /// <param name="dto">The updated order data.</param>
        /// <returns>Returns the updated order or an error message if the update fails.</returns>
        [HttpPut("update")]
        public IActionResult UpdateOrder([FromBody] DTOYMO01 dto)
        {
            Response response;
            _orderService.SetOperationType(EnmOperationType.Add);
            YMO01 poco = _orderService.PreSave(dto);
            response = _orderService.ValidateOnSave(poco);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            response = _orderService.Save(poco);
            return Ok(response.Message);
        }

        /// <summary>
        /// Endpoint to delete an order by its ID.
        /// </summary>
        /// <param name="id">ID of the order to delete.</param>
        /// <returns>Returns a success message or an error message if deletion fails.</returns>
        [HttpDelete("delete")]
        public IActionResult DeleteOrder([FromBody] DTOYMO01 dto)
        {
            Response response;
            YMO01 poco = _orderService.PreDelete(dto);
            response = _orderService.ValidateOnDelete(poco);
            if (response.IsError)
            {
                return BadRequest(response.Message);
            }
            response = _orderService.Delete(poco);
            return Ok(response.Message);
        }
    }
}

