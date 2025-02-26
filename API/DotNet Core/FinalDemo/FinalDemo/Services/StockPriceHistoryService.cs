using FinalDemo.Enums;
using FinalDemo.ExtensionMethods;
using FinalDemo.Extensions;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.Services
{
    /// <summary>
    /// StockPriceHistoryService is a service that handles operations related to stock price history.
    /// It supports CRUD operations (Create, Read, U, D) for stock price data and provides 
    /// methods for validation, saving, and retrieving stock price history records.
    /// </summary>
    public class StockPriceHistoryService : IStockPriceHistoryService
    {
        private readonly IOrmLiteDbFactory _dbFactory;
        private IDbConnection _dbConnection;

        /// <summary>
        /// Gets or sets the operation type (A, U, D).
        /// </summary>
        public EnmOperationType type { get; set; }

        /// <summary>
        /// Initializes a new instance of the StockPriceHistoryService class.
        /// </summary>
        /// <param name="dbFactory">The database factory used to create database connections.</param>
        public StockPriceHistoryService(IOrmLiteDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// Sets the operation type for the service.
        /// </summary>
        /// <param name="operationType">The operation type to set.</param>
        public void SetOperationType(EnmOperationType operationType)
        {
            type = operationType;
        }

        /// <summary>
        /// Deletes a stock price history record from the database.
        /// </summary>
        /// <param name="poco">The stock price history object to delete.</param>
        /// <returns>A response indicating whether the deletion was successful.</returns>
        public Response Delete(YMH01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    int rowsAffected = _dbConnection.DeleteById<YMH01>(poco.H01F01);

                    return rowsAffected > 0
                        ? new Response { Message = "Stock price history deleted successfully" }
                        : new Response { IsError = true, Message = "Stock price history not found or not deleted" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Retrieves all stock price history records from the database.
        /// </summary>
        /// <returns>A response containing all stock price history in JSON format.</returns>
        public Response GetAllStockPriceHistory()
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    List<YMH01> priceHistory = _dbConnection.Select<YMH01>();

                    if (priceHistory == null || priceHistory.Count == 0)
                    {
                        return new Response { Message = "No stock price history available" };
                    }

                    DataTable data = priceHistory.ConvertToDataTable<YMH01>();
                    return new Response { Data = data, Message = "Stock price history retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Retrieves a stock price history record by its ID.
        /// </summary>
        /// <param name="id">The ID of the stock price history record.</param>
        /// <returns>A response containing the stock price history record or an error message.</returns>
        public Response GetStockPriceHistoryById(int id)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    YMH01 priceHistory = _dbConnection.SingleById<YMH01>(id);

                    if (priceHistory == null)
                    {
                        return new Response { IsError = true, Message = "Stock price history not found" };
                    }

                    return new Response { Data = priceHistory, Message = "Stock price history retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Retrieves stock price history records by stock ID.
        /// </summary>
        /// <param name="stockId">The stock ID to filter the stock price history by.</param>
        /// <returns>A response containing the filtered stock price history records in JSON format.</returns>
        public Response GetStockPriceHistoryByStockId(int stockId)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    List<YMH01> priceHistory = _dbConnection.Select<YMH01>(x => x.H01F02 == stockId);

                    if (priceHistory == null || priceHistory.Count == 0)
                    {
                        return new Response { Message = "No stock price history found for this stock" };
                    }

                    DataTable data = priceHistory.ConvertToDataTable<YMH01>();
                    return new Response { Data = data, Message = "Stock price history retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Prepares a DTO for deletion by converting it to a POCO.
        /// </summary>
        /// <param name="dto">The DTO to convert.</param>
        /// <returns>The converted POCO object.</returns>
        public YMH01 PreDelete(DTOYMH01 dto)
        {
            return dto.ToPoco<YMH01>();
        }

        /// <summary>
        /// Prepares a DTO for saving by converting it to a POCO.
        /// </summary>
        /// <param name="dto">The DTO to convert.</param>
        /// <returns>The converted POCO object.</returns>
        public YMH01 PreSave(DTOYMH01 dto)
        {
            return dto.ToPoco<YMH01>();
        }

        /// <summary>
        /// Saves a stock price history record to the database (either adding or updating it).
        /// </summary>
        /// <param name="poco">The POCO object containing stock price history data.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public Response Save(YMH01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    if ((type & EnmOperationType.A) == EnmOperationType.A)
                    {
                        _dbConnection.Insert(poco);
                        return new Response { Message = "Stock price history added successfully" };
                    }

                    if ((type & EnmOperationType.U) == EnmOperationType.U)
                    {
                        _dbConnection.Update(poco);
                        return new Response { Message = "Stock price history updated successfully" };
                    }

                    return new Response { IsError = true, Message = "Invalid operation type" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Validates whether a stock price history record can be deleted.
        /// </summary>
        /// <param name="poco">The stock price history object to validate.</param>
        /// <returns>A response indicating the result of the validation.</returns>
        public Response ValidateOnDelete(YMH01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    YMH01? existingHistory = _dbConnection.SingleById<YMH01>(poco.H01F01);
                    if (existingHistory == null)
                    {
                        return new Response { IsError = true, Message = "Stock price history not found for delete" };
                    }

                    return new Response { Message = "Validation successful" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = $"An error occurred during validation: {ex.Message}" };
            }
        }

        /// <summary>
        /// Validates whether a stock price history record can be saved (added or updated).
        /// </summary>
        /// <param name="poco">The stock price history object to validate.</param>
        /// <returns>A response indicating the result of the validation.</returns>
        public Response ValidateOnSave(YMH01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    if ((type & EnmOperationType.A) == EnmOperationType.A)
                    {
                        bool isExist = _dbConnection.Exists<YMH01>(x => x.H01F02 == poco.H01F02 && x.H01F03 == poco.H01F03);
                        if (isExist)
                        {
                            return new Response { IsError = true, Message = "Stock price for this date already exists." };
                        }
                    }

                    if ((type & EnmOperationType.U) == EnmOperationType.U)
                    {
                        YMH01? existingHistory = _dbConnection.SingleById<YMH01>(poco.H01F01);
                        if (existingHistory == null)
                        {
                            return new Response { IsError = true, Message = "Stock price history not found for update." };
                        }
                    }

                    return new Response { Message = "Validation successful" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = $"An error occurred during validation: {ex.Message}" };
            }
        }

        /// <summary>
        /// Retrieves the date when the stock price was at its maximum.
        /// </summary>
        /// <param name="stockId">The stock ID to filter the stock price history by.</param>
        /// <returns>A response containing the date when the stock price was at its maximum.</returns>
        public Response GetMaxStockPriceDate(int stockId)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    SqlExpression<YMH01>? q = _dbConnection.From<YMH01>()
                         .Where(x => x.H01F02 == stockId)
                         .OrderByDescending(x => x.H01F03)
                         .Take(1);

                    List<YMH01> maxPriceRecord = _dbConnection.Select(q);

                    if (maxPriceRecord == null)
                    {
                        return new Response { Message = "No stock price history found for this stock" };
                    }

                    return new Response { Data = maxPriceRecord, Message = "Max stock price date retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Retrieves the date when the stock price was at its minimum.
        /// </summary>
        /// <param name="stockId">The stock ID to filter the stock price history by.</param>
        /// <returns>A response containing the date when the stock price was at its minimum.</returns>
        public Response GetMinStockPriceDate(int stockId)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    SqlExpression<YMH01>? q = _dbConnection.From<YMH01>()
                         .Where(x => x.H01F02 == stockId)
                         .OrderBy(x => x.H01F03)
                         .Take(1);

                    List<YMH01> minPriceRecord = _dbConnection.Select(q);

                    if (minPriceRecord == null)
                    {
                        return new Response { Message = "No stock price history found for this stock" };
                    }
                    return new Response { Data = minPriceRecord, Message = "Min stock price date retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }
    }
}