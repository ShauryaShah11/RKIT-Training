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
    /// Provides operations for managing orders, including add, update, delete, and retrieval from the database.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrmLiteDbFactory _dbFactory;
        private IDbConnection _dbConnection;

        /// <summary>
        /// Gets or sets the operation type (A, U, D).
        /// </summary>
        public EnmOperationType type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory to create connections.</param>
        public OrderService(IOrmLiteDbFactory dbFactory)
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
        /// Deletes an order from the database based on the provided POCO object.
        /// </summary>
        /// <param name="poco">POCO representing the order to be deleted.</param>
        /// <returns>A response indicating the result of the deletion.</returns>
        public Response Delete(YMO01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    int rowsAffected = _dbConnection.DeleteById<YMO01>(poco.O01F01);

                    return rowsAffected > 0
                        ? new Response { Message = "Order deleted successfully" }
                        : new Response { IsError = true, Message = "Order not found or not deleted" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        /// <returns>A response containing a list of orders in JSON format or an error message.</returns>
        public Response GetAllOrder()
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    List<YMO01> orders = _dbConnection.Select<YMO01>();

                    if (orders == null || orders.Count == 0)
                    {
                        return new Response { Message = "No orders available" };
                    }

                    DataTable data = orders.ConvertToDataTable<YMO01>();
                    return new Response { Data = data, Message = "Orders retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Retrieves a specific order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to be retrieved.</param>
        /// <returns>A response containing the order data or an error message.</returns>
        public Response GetOrderById(int id)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    YMO01 order = _dbConnection.SingleById<YMO01>(id);

                    if (order == null)
                    {
                        return new Response { IsError = true, Message = "Order not found" };
                    }

                    return new Response { Data = order, Message = "Order retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Converts the provided DTO to a POCO object for deletion.
        /// </summary>
        /// <param name="dto">DTO to be converted.</param>
        /// <returns>The corresponding POCO object.</returns>
        public YMO01 PreDelete(DTOYMO01 dto)
        {
            return dto.ToPoco<YMO01>();
        }

        /// <summary>
        /// Converts the provided DTO to a POCO object for saving.
        /// </summary>
        /// <param name="dto">DTO to be converted.</param>
        /// <returns>The corresponding POCO object.</returns>
        public YMO01 PreSave(DTOYMO01 dto)
        {
            return dto.ToPoco<YMO01>();
        }

        /// <summary>
        /// Saves the order (either adding or updating) in the database based on the operation type.
        /// </summary>
        /// <param name="poco">POCO representing the order to be saved.</param>
        /// <returns>A response indicating the result of the save operation.</returns>
        public Response Save(YMO01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    if ((type & EnmOperationType.A) == EnmOperationType.A)
                    {
                        _dbConnection.Insert(poco);
                        return new Response { Message = "Order added successfully" };
                    }

                    if ((type & EnmOperationType.U) == EnmOperationType.U)
                    {
                        _dbConnection.Update(poco);
                        return new Response { Message = "Order updated successfully" };
                    }

                    return new Response { IsError = true, Message = "Invalid operation type" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Message = "An error occurred while processing the request: " + ex.Message };
            }
        }

        /// <summary>
        /// Validates the order before deletion.
        /// </summary>
        /// <param name="poco">POCO representing the order to be validated.</param>
        /// <returns>A response indicating the result of the validation.</returns>
        public Response ValidateOnDelete(YMO01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    YMO01? existingOrder = _dbConnection.SingleById<YMO01>(poco.O01F01);
                    if (existingOrder == null)
                    {
                        return new Response { IsError = true, Message = "Order not found for delete" };
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
        /// Validates the order before saving (A or U).
        /// </summary>
        /// <param name="poco">POCO representing the order to be validated.</param>
        /// <returns>A response indicating the result of the validation.</returns>
        public Response ValidateOnSave(YMO01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    if ((type & EnmOperationType.A) == EnmOperationType.A)
                    {
                        bool isExist = _dbConnection.Exists<YMO01>(x => x.O01F02 == poco.O01F02 && x.O01F02 == poco.O01F02);
                        if (isExist)
                        {
                            return new Response { IsError = true, Message = "Order for this date already exists." };
                        }
                    }

                    if ((type & EnmOperationType.U) == EnmOperationType.U)
                    {
                        YMO01? existingOrder = _dbConnection.SingleById<YMO01>(poco.O01F01);
                        if (existingOrder == null)
                        {
                            return new Response { IsError = true, Message = "Order not found for update." };
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
    }
}