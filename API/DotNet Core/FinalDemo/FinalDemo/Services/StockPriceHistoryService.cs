using FinalDemo.Enums;
using FinalDemo.Extensions;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using Newtonsoft.Json;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.Services
{
    /// <summary>
    /// StockPriceHistoryService is a service that handles operations related to stock price history.
    /// It supports CRUD operations (Create, Read, Update, Delete) for stock price data and provides 
    /// methods for validation, saving, and retrieving stock price history records.
    /// </summary>
    public class StockPriceHistoryService : IStockPriceHistoryService
    {
        private readonly IOrmLiteDbFactory _dbFactory;
        private IDbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the StockPriceHistoryService class.
        /// </summary>
        /// <param name="dbFactory">The database factory used to create database connections.</param>
        public StockPriceHistoryService(IOrmLiteDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// Handles different operations (Add, Update, Delete) for stock price history based on the operation type.
        /// </summary>
        /// <param name="dto">Data transfer object for stock price history.</param>
        /// <param name="type">The operation type (Add, Update, Delete).</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public Response HandleOperation(DTOYMH01 dto, EnmOperationType type)
        {
            if (((type & EnmOperationType.Add) == EnmOperationType.Add) || ((type & EnmOperationType.Update) == EnmOperationType.Update))
            {
                YMH01 poco = PreSave(dto);
                Response response = ValidateOnSave(poco, type);
                if (response.IsError)
                {
                    return response;
                }

                Response saveResponse = Save(poco, type);
                if (saveResponse.IsError)
                {
                    saveResponse.Message = "Error while storing stock price history in database";
                    return saveResponse;
                }
                return saveResponse;
            }

            if ((type & EnmOperationType.Delete) == EnmOperationType.Delete)
            {
                YMH01 poco = PreDelete(dto);
                Response response = ValidateOnDelete(poco);
                if (response.IsError)
                {
                    return response;
                }

                Response deleteResponse = Delete(poco);
                if (deleteResponse.IsError)
                {
                    deleteResponse.Message = "Error while deleting stock price history in database";
                    return deleteResponse;
                }
                return deleteResponse;
            }

            return new Response { IsError = true, Message = "Invalid operation type" };
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
                        ? new Response { IsError = false, Message = "Stock price history deleted successfully" }
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
                        return new Response { IsError = false, Message = "No stock price history available" };
                    }

                    string jsonstring = JsonConvert.SerializeObject(priceHistory, Formatting.Indented);
                    return new Response { IsError = false, Data = jsonstring, Message = "Stock price history retrieved successfully" };
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

                    return new Response { IsError = false, Data = priceHistory, Message = "Stock price history retrieved successfully" };
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
                        return new Response { IsError = false, Message = "No stock price history found for this stock" };
                    }

                    string jsonstring = JsonConvert.SerializeObject(priceHistory, Formatting.Indented);
                    return new Response { IsError = false, Data = jsonstring, Message = "Stock price history retrieved successfully" };
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
        /// <param name="type">The operation type (Add or Update).</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public Response Save(YMH01 poco, EnmOperationType type)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    if ((type & EnmOperationType.Add) == EnmOperationType.Add)
                    {
                        _dbConnection.Insert(poco);
                        return new Response { IsError = false, Message = "Stock price history added successfully" };
                    }

                    if ((type & EnmOperationType.Update) == EnmOperationType.Update)
                    {
                        _dbConnection.Update(poco);
                        return new Response { IsError = false, Message = "Stock price history updated successfully" };
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

                    return new Response { IsError = false, Message = "Validation successful" };
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
        /// <param name="type">The operation type (Add or Update).</param>
        /// <returns>A response indicating the result of the validation.</returns>
        public Response ValidateOnSave(YMH01 poco, EnmOperationType type)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    if ((type & EnmOperationType.Add) == EnmOperationType.Add)
                    {
                        bool isExist = _dbConnection.Exists<YMH01>(x => x.H01F02 == poco.H01F02 && x.H01F03 == poco.H01F03);
                        if (isExist)
                        {
                            return new Response { IsError = true, Message = "Stock price for this date already exists." };
                        }
                    }

                    if ((type & EnmOperationType.Update) == EnmOperationType.Update)
                    {
                        YMH01? existingHistory = _dbConnection.SingleById<YMH01>(poco.H01F01);
                        if (existingHistory == null)
                        {
                            return new Response { IsError = true, Message = "Stock price history not found for update." };
                        }
                    }

                    return new Response { IsError = false, Message = "Validation successful" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = $"An error occurred during validation: {ex.Message}" };
            }
        }
    }
}
