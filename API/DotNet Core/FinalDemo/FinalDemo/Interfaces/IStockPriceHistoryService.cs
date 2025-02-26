using FinalDemo.Enums;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Interface for managing stock price history, including methods for retrieving, 
    /// saving, updating, and deleting stock price records.
    /// </summary>
    public interface IStockPriceHistoryService : IBaseService<YMH01, DTOYMH01>
    {
        /// <summary>
        /// Retrieves a stock price history entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the stock price history entry.</param>
        /// <returns>A response containing the stock price history entry with the specified ID.</returns>
        Response GetStockPriceHistoryById(int id);

        /// <summary>
        /// Retrieves all stock price history entries.
        /// </summary>
        /// <returns>A response containing a list of all stock price history records.</returns>
        Response GetAllStockPriceHistory();

        /// <summary>
        /// Retrieves all stock price history records associated with a specific stock.
        /// </summary>
        /// <param name="stockId">The ID of the stock.</param>
        /// <returns>A response containing a list of stock price history records for the specified stock.</returns>
        Response GetStockPriceHistoryByStockId(int stockId);

        /// <summary>
        /// Retrieves the date of the minimum stock price for a specific stock.
        /// </summary>
        /// <param name="stockId">The ID of the stock.</param>
        /// <returns>A response containing the date of the minimum stock price for the specified stock.</returns>
        Response GetMinStockPriceDate(int stockId);

        /// <summary>
        /// Retrieves the date of the maximum stock price for a specific stock.
        /// </summary>
        /// <param name="stockId">The ID of the stock.</param>
        /// <returns>A response containing the date of the maximum stock price for the specified stock.</returns>
        Response GetMaxStockPriceDate(int stockId);
    }
}