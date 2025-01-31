using FinalDemo.Enums;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Interface for managing stock price history, including methods for retrieving, saving, updating, and deleting stock price records.
    /// </summary>
    public interface IStockPriceHistoryService: IBaseService<YMH01, DTOYMH01>
    {
        /// <summary>
        /// Retrieves a stock price history entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the stock price history entry.</param>
        /// <returns>The stock price history entry with the specified ID.</returns>
        Response GetStockPriceHistoryById(int id);        
    }
}
