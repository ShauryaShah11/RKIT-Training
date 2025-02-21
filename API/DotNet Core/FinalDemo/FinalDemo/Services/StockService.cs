using FinalDemo.Enums;
using FinalDemo.ExtensionMethods;
using FinalDemo.Extensions;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using Newtonsoft.Json;
using ServiceStack.OrmLite;
using System.Data;
using System.Diagnostics;

namespace FinalDemo.Services
{
    /// <summary>
    /// StockService handles operations like adding, updating, deleting, and fetching stock records
    /// from the database. It performs necessary validations, saves records, and handles errors.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IOrmLiteDbFactory _dbFactory;  // Dependency injected factory for opening database connections
        private IDbConnection _dbConnection; // Database connection instance

        /// <summary>
        /// Initializes the StockService with a database factory to open database connections.
        /// </summary>
        /// <param name="dbFactory">Database factory for creating database connections.</param>
        public StockService(IOrmLiteDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// Deletes a stock record from the database.
        /// </summary>
        /// <param name="poco">The POCO object representing the stock to be deleted.</param>
        /// <returns>A Response object containing the status and message of the operation.</returns>
        public Response Delete(YMS01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    int rowsAffected = _dbConnection.DeleteById<YMS01>(poco.S01F01);

                    return rowsAffected > 0
                        ? new Response { IsError = false, Message = "Stock deleted successfully" }
                        : new Response { IsError = true, Message = "Stock not found or not deleted" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Retrieves all stock records from the database.
        /// </summary>
        /// <returns>A Response object containing the list of stocks and the operation result.</returns>
        public Response GetAllStocks()
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    List<YMS01> stocks = _dbConnection.Select<YMS01>();

                    if (stocks == null)
                    {
                        return new Response { IsError = false, Message = "Stocks do not exist" };
                    }

                    string jsonstring = JsonConvert.SerializeObject(stocks, Formatting.Indented);

                    return new Response { IsError = false, Data = jsonstring, Message = "Stocks retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Retrieves a specific stock record by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the stock to retrieve.</param>
        /// <returns>A Response object containing the stock data and operation result.</returns>
        public Response GetStockById(int id)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    List<YMS01> stocks = _dbConnection.Select<YMS01>(s => s.S01F01 == id);

                    if (stocks == null)
                    {
                        return new Response { IsError = false, Message = "Stock does not exist" };
                    }
                    return new Response { IsError = true, Data = stocks, Message = $"Stock with ID {id} retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Retrieves a specific stock record by its name from the database.
        /// </summary>
        /// <param name="name">The name of the stock to retrieve.</param>
        /// <returns>A Response object containing the stock data and operation result.</returns>
        public Response GetStockByName(string name)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    List<YMS01> stocks = _dbConnection.Select<YMS01>(s => s.S01F02 == name); // Assume S01F02 is the stock name field

                    if (stocks == null)
                    {
                        return new Response { IsError = false, Message = "Stock not found by name" };
                    }

                    return new Response { IsError = true, Data = stocks, Message = $"Stock with name {name} retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Prepares a DTO object for deletion by converting it to a POCO object.
        /// </summary>
        /// <param name="dto">The DTO object representing the stock to delete.</param>
        /// <returns>The corresponding POCO object.</returns>
        public YMS01 PreDelete(DTOYMS01 dto)
        {
            return dto.ToPoco<YMS01>();
        }

        /// <summary>
        /// Prepares a DTO object for saving by converting it to a POCO object.
        /// </summary>
        /// <param name="dto">The DTO object representing the stock to save.</param>
        /// <returns>The corresponding POCO object.</returns>
        public YMS01 PreSave(DTOYMS01 dto)
        {
            return dto.ToPoco<YMS01>();
        }

        /// <summary>
        /// Saves (inserts or updates) a stock record based on the specified operation type.
        /// </summary>
        /// <param name="poco">The POCO object representing the stock to save.</param>
        /// <param name="type">The type of operation to perform (Add or Update).</param>
        /// <returns>A Response object containing the status and message of the operation.</returns>
        public Response Save(YMS01 poco, EnmOperationType type)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    // Insert a new stock record
                    if ((type & EnmOperationType.Add) == EnmOperationType.Add)
                    {
                        _dbConnection.Insert(poco);
                        return new Response { IsError = false, Message = "Stock added successfully" };
                    }

                    // Update an existing stock record
                    if ((type & EnmOperationType.Update) == EnmOperationType.Update)
                    {
                        _dbConnection.Update(poco);
                        return new Response { IsError = false, Message = "Stock updated successfully" };
                    }

                    return new Response { IsError = true, Message = "Invalid operation type" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Validates the stock record before deletion.
        /// </summary>
        /// <param name="poco">The POCO object representing the stock to validate.</param>
        /// <returns>A Response object indicating the validation result.</returns>
        public Response ValidateOnDelete(YMS01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    YMS01? existingStock = _dbConnection.SingleById<YMS01>(poco.S01F01);
                    if (existingStock == null)
                    {
                        return new Response { IsError = true, Message = "Stock not found for delete." };
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
        /// Validates the stock record before saving (Add or Update).
        /// </summary>
        /// <param name="poco">The POCO object representing the stock to validate.</param>
        /// <param name="type">The type of operation to validate (Add or Update).</param>
        /// <returns>A Response object indicating the validation result.</returns>
        public Response ValidateOnSave(YMS01 poco, EnmOperationType type)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    // Validation for Add operation (checks if the stock name already exists)
                    if ((type & EnmOperationType.Add) == EnmOperationType.Add)
                    {
                        bool isExist = _dbConnection.Exists<YMS01>(x => x.S01F02 == poco.S01F02); // Assume S01F02 is stock name
                        if (isExist)
                        {
                            return new Response { IsError = true, Message = "Stock with this name already exists." };
                        }
                    }

                    // Validation for Update operation (checks if the stock exists)
                    if ((type & EnmOperationType.Update) == EnmOperationType.Update)
                    {
                        YMS01? existingStock = _dbConnection.SingleById<YMS01>(poco.S01F01);
                        if (existingStock == null)
                        {
                            return new Response { IsError = true, Message = "Stock not found for update." };
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
