using FinalDemo.Enums;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Interface for managing stock price history, including methods for retrieving, saving, updating, and deleting stock price records.
    /// </summary>
    public interface IStockPriceHistoryService
    {
        /// <summary>
        /// Retrieves a stock price history entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the stock price history entry.</param>
        /// <returns>The stock price history entry with the specified ID.</returns>
        YMH01 GetStockPriceHistoryById(int id);

        /// <summary>
        /// Prepares a stock price history entry for saving by converting a DTO to POCO.
        /// </summary>
        /// <param name="dto">The data transfer object (DTO) representing the stock price history.</param>
        /// <returns>The corresponding stock price history POCO object.</returns>
        YMH01 PreSave(DTOYMH01 dto);

        /// <summary>
        /// Validates a stock price history entry before saving to ensure all necessary conditions are met.
        /// </summary>
        /// <param name="poco">The stock price history POCO object to be validated.</param>
        /// <returns>A response indicating whether the validation was successful or not.</returns>
        Response ValidateOnSave(YMH01 poco, OperationType type);

        /// <summary>
        /// Saves the provided stock price history entry to the database.
        /// </summary>
        /// <param name="poco">The stock price history POCO object to be saved.</param>
        /// <returns>A response indicating whether the save operation was successful.</returns>
        Response Save(YMH01 poco, OperationType type);

        /// <summary>
        /// Prepares a stock price history entry for deletion by converting a DTO to POCO.
        /// </summary>
        /// <param name="dto">The data transfer object (DTO) representing the stock price history to be deleted.</param>
        /// <returns>The corresponding stock price history POCO object to be deleted.</returns>
        YMH01 PreDelete(DTOYMH01 dto);

        /// <summary>
        /// Validates a stock price history entry before deletion to ensure it can be safely deleted.
        /// </summary>
        /// <param name="poco">The stock price history POCO object to be validated for deletion.</param>
        /// <returns>A response indicating whether the stock price history can be safely deleted.</returns>
        Response ValidateOnDelete(YMH01 poco);

        /// <summary>
        /// Deletes the specified stock price history entry from the database.
        /// </summary>
        /// <param name="poco">The stock price history POCO object to be deleted.</param>
        /// <returns>A response indicating whether the delete operation was successful.</returns>
        Response Delete(YMH01 poco);
    }
}
