using FinalDemo.Enums;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Interface for managing orders, including methods for CRUD operations and validation.
    /// </summary>
    public interface IOrderService
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
        Response GetOrderbyId(int id);

        /// <summary>
        /// Prepares an order for saving by converting a DTO to POCO.
        /// </summary>
        /// <param name="dto">The data transfer object representing the order.</param>
        /// <returns>The corresponding order POCO object.</returns>
        YMO01 PreSave(DTOYMO01 dto);

        /// <summary>
        /// Validates an order before saving.
        /// </summary>
        /// <param name="poco">The POCO representing the order to be validated.</param>
        /// <returns>A response indicating whether validation was successful.</returns>
        Response ValidateOnSave(YMO01 poco, OperationType  type);

        /// <summary>
        /// Saves the provided order to the database.
        /// </summary>
        /// <param name="poco">The POCO representing the order to be saved.</param>
        /// <returns>A response indicating whether the save operation was successful.</returns>
        Response Save(YMO01 poco, OperationType type);

        /// <summary>
        /// Prepares an order for deletion by converting a DTO to POCO.
        /// </summary>
        /// <param name="dto">The data transfer object representing the order.</param>
        /// <returns>The corresponding order POCO object.</returns>
        YMO01 PreDelete(DTOYMO01 dto);

        /// <summary>
        /// Validates an order before deletion.
        /// </summary>
        /// <param name="poco">The POCO representing the order to be validated for deletion.</param>
        /// <returns>A response indicating whether the order can be deleted.</returns>
        Response ValidateOnDelete(YMO01 poco);

        /// <summary>
        /// Deletes the specified order from the database.
        /// </summary>
        /// <param name="poco">The POCO representing the order to be deleted.</param>
        /// <returns>A response indicating whether the delete operation was successful.</returns>
        Response Delete(YMO01 poco);
    }
}
