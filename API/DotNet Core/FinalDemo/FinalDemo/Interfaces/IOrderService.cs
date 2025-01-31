using FinalDemo.Enums;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Interface for managing orders, including methods for CRUD operations and validation.
    /// </summary>
    public interface IOrderService: IBaseService<YMO01, DTOYMO01>
    {
        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A response containing a list of all orders.</returns>
        Response GetAllOrder();

        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>A response containing the order details.</returns>
        Response GetOrderById(int id);        
    }
}
