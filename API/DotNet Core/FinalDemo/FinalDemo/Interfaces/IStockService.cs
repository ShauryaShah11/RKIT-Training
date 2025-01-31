using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Interfaces
{
    public interface IStockService : IBaseService<YMS01, DTOYMS01>
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

    }
}
