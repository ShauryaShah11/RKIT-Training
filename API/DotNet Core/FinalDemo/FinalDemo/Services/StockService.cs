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
    /// StockService handles operations like adding, updating, deleting, and fetching stock records
    /// from the database. It performs necessary validations, saves records, and handles errors.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IOrmLiteDbFactory _dbFactory;  // Dependency injected factory for opening database connections
        private IDbConnection _dbConnection; // Database connection instance

        /// <summary>
        /// Gets or sets the operation type (A, U, D).
        /// </summary>
        public EnmOperationType type { get; set; }

        /// <summary>
        /// Initializes the StockService with a database factory to open database connections.
        /// </summary>
        /// <param name="dbFactory">Database factory for creating database connections.</param>
        public StockService(IOrmLiteDbFactory dbFactory)
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
                        ? new Response { Message = "Stock deleted successfully" }
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
                        return new Response { IsError = true, Message = "Stocks do not exist" };
                    }

                    DataTable data = stocks.ConvertToDataTable<YMS01>();
                    return new Response { Data = data, Message = "Stocks retrieved successfully" };
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
                        return new Response { IsError = true, Message = "Stock does not exist" };
                    }
                    DataTable data = stocks.ConvertToDataTable<YMS01>();
                    return new Response { Data = data, Message = $"Stock with ID {id} retrieved successfully" };
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
                        return new Response { IsError = true, Message = "Stock not found by name" };
                    }

                    return new Response { Data = stocks, Message = $"Stock with name {name} retrieved successfully" };
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
        /// <returns>A Response object containing the status and message of the operation.</returns>
        public Response Save(YMS01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    // Insert a new stock record
                    if ((type & EnmOperationType.A) == EnmOperationType.A)
                    {
                        _dbConnection.Insert(poco);
                        return new Response { IsError = false, Message = "Stock added successfully" };
                    }

                    // U an existing stock record
                    if ((type & EnmOperationType.U) == EnmOperationType.U)
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
        /// Validates the stock record before saving (A or U).
        /// </summary>
        /// <param name="poco">The POCO object representing the stock to validate.</param>
        /// <returns>A Response object indicating the validation result.</returns>
        public Response ValidateOnSave(YMS01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    // Validation for A operation (checks if the stock name already exists)
                    if ((type & EnmOperationType.A) == EnmOperationType.A)
                    {
                        bool isExist = _dbConnection.Exists<YMS01>(x => x.S01F02 == poco.S01F02); // Assume S01F02 is stock name
                        if (isExist)
                        {
                            return new Response { IsError = true, Message = "Stock with this name already exists." };
                        }
                    }

                    // Validation for U operation (checks if the stock exists)
                    if ((type & EnmOperationType.U) == EnmOperationType.U)
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