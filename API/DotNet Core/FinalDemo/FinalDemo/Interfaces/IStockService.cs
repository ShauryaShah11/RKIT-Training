using FinalDemo.Enums;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Interfaces
{
    public interface IStockService
    {
        /// <summary>
        /// Retrieves all stock records.
        /// </summary>
        /// <returns>A response containing the list of all stocks.</returns>
        Response GetAllStocks();

        /// <summary>
        /// Retrieves a specific stock by its ID.
        /// </summary>
        /// <param name="id">The ID of the stock to be retrieved.</param>
        /// <returns>A response containing the stock details with the specified ID.</returns>
        Response GetStockById(int id);

        /// <summary>
        /// Retrieves a specific stock by its name.
        /// </summary>
        /// <param name="name">The name of the stock to be retrieved.</param>
        /// <returns>A response containing the stock details with the specified name.</returns>
        Response GetStockByName(string name);

        /// <summary>
        /// Prepares a stock for saving by converting a DTO (Data Transfer Object) to a POCO (Plain Old CLR Object).
        /// </summary>
        /// <param name="dto">The data transfer object representing the stock.</param>
        /// <returns>The corresponding stock POCO object ready for saving.</returns>
        YMS01 PreSave(DTOYMS01 dto);

        /// <summary>
        /// Validates a stock before saving to ensure all required conditions are met.
        /// </summary>
        /// <param name="poco">The stock POCO to be validated.</param>
        /// <returns>A response indicating whether the stock validation was successful or failed.</returns>
        Response ValidateOnSave(YMS01 poco, OperationType type);

        /// <summary>
        /// Saves the provided stock to the database.
        /// </summary>
        /// <param name="poco">The stock POCO object to be saved.</param>
        /// <returns>A response indicating whether the save operation was successful or not.</returns>
        Response Save(YMS01 poco, OperationType type);

        /// <summary>
        /// Prepares a stock for deletion by converting a DTO to POCO.
        /// </summary>
        /// <param name="dto">The data transfer object representing the stock to be deleted.</param>
        /// <returns>The corresponding stock POCO object to be deleted.</returns>
        YMS01 PreDelete(DTOYMS01 dto);

        /// <summary>
        /// Validates a stock before deletion to ensure it can be safely deleted.
        /// </summary>
        /// <param name="poco">The stock POCO to be validated for deletion.</param>
        /// <returns>A response indicating whether the stock can be safely deleted or not.</returns>
        Response ValidateOnDelete(YMS01 poco);

        /// <summary>
        /// Deletes the specified stock from the database.
        /// </summary>
        /// <param name="poco">The stock POCO object to be deleted.</param>
        /// <returns>A response indicating whether the delete operation was successful or failed.</returns>
        Response Delete(YMS01 poco);
    }
}
