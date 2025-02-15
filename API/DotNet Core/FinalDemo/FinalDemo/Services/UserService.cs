﻿using FinalDemo.Enums;
using FinalDemo.ExtensionMethods;
using FinalDemo.Extensions;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;
using System.Diagnostics;

namespace FinalDemo.Services
{
    /// <summary>
    /// Provides user management functionality, including handling CRUD operations for users.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IOrmLiteDbFactory _dbFactory;
        private IDbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// Injects the database factory dependency to manage database connections.
        /// </summary>
        /// <param name="dbFactory">The ORM Lite database factory for handling connections.</param>
        public UserService(IOrmLiteDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// Handles the specified operation (Add, Update, or Delete) on the user data.
        /// </summary>
        /// <param name="user">The user data to operate on.</param>
        /// <param name="type">The type of operation to perform (Add, Update, or Delete).</param>
        /// <returns>A response indicating the result of the operation.</returns>
        public Response HandleOperation(DTOYMU01 user, EnmOperationType type)
        {
            if (((type & EnmOperationType.Add) == EnmOperationType.Add) || ((type & EnmOperationType.Update) == EnmOperationType.Update))
            {
                YMU01 poco = PreSave(user);
                Response userResponse = ValidateOnSave(poco, type);
                if (userResponse.IsError)
                {
                    return userResponse;
                }

                Response saveResponse = Save(poco, type);
                if (saveResponse.IsError)
                {
                    saveResponse.Message = "Error While Storing user in database";
                    return saveResponse;
                }
                return saveResponse;
            }

            if ((type & EnmOperationType.Delete) == EnmOperationType.Delete)
            {
                YMU01 poco = PreDelete(user);
                Response userResponse = ValidateOnDelete(poco);
                if (userResponse.IsError)
                {
                    return userResponse;
                }

                Response deleteResponse = Delete(poco);
                if (deleteResponse.IsError)
                {
                    deleteResponse.Message = "Error While Deleting user in database";
                    return deleteResponse;
                }
                return deleteResponse;
            }

            // Default return statement in case no operation type matches
            return new Response { IsError = true, Message = "Invalid operation type" };
        }

        /// <summary>
        /// Deletes a user from the database based on the provided user object.
        /// </summary>
        /// <param name="poco">The user data to delete.</param>
        /// <returns>A response indicating the result of the deletion operation.</returns>
        public Response Delete(YMU01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    // Try to delete by ID and check if the operation was successful
                    int rowsAffected = _dbConnection.DeleteById<YMU01>(poco.U01F01);

                    // Return success if rows were affected, otherwise return failure
                    return rowsAffected > 0
                        ? new Response { IsError = false, Message = "User deleted successfully" }
                        : new Response { IsError = true, Message = "User not found or not deleted" };
                }
            }
            catch (Exception ex)
            {
                // Log the error appropriately (for now, just printing to the console, but use a logging framework in real-world scenarios)
                Console.WriteLine(ex.Message);

                // Return a generic error message
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A response containing the list of users in JSON format.</returns>
        public Response GetAllUsers()
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    List<YMU01> users = _dbConnection.Select<YMU01>();

                    if (users == null)
                    {
                        return new Response { IsError = false, Message = "User does not exist" };
                    }

                    DataTable data = users.ConvertToDataTable<YMU01>();

                    return new Response { IsError = false, Data = data, Message = "User retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A response containing the user data or an error message if not found.</returns>
        public Response GetUserById(int id)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    List<YMU01> users = _dbConnection.Select<YMU01>(u => u.U01F01 == id);

                    if (users == null)
                    {
                        return new Response { IsError = false, Message = "User does not exist" };
                    }
                    return new Response { IsError = true, Data = users.ConvertToDataTable<YMU01>(), Message = $"User with ID {id} retrieved successfully" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Prepares a DTO object for deletion by converting it to a POCO.
        /// </summary>
        /// <param name="dto">The DTO object to convert.</param>
        /// <returns>The POCO representation of the DTO.</returns>
        public YMU01 PreDelete(DTOYMU01 dto)
        {
            return dto.ToPoco<YMU01>();
        }

        /// <summary>
        /// Prepares a DTO object for saving by converting it to a POCO.
        /// </summary>
        /// <param name="dto">The DTO object to convert.</param>
        /// <returns>The POCO representation of the DTO.</returns>
        public YMU01 PreSave(DTOYMU01 dto)
        {
            return dto.ToPoco<YMU01>();
        }

        /// <summary>
        /// Saves a user to the database, either by adding or updating.
        /// </summary>
        /// <param name="poco">The user data to save.</param>
        /// <param name="type">The type of operation (Add or Update).</param>
        /// <returns>A response indicating the result of the save operation.</returns>
        public Response Save(YMU01 poco, EnmOperationType type)
        {
            try
            {
                // Open the connection inside the try-catch block using 'using'
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    // Add Operation: Insert the new user record into the database
                    if ((type & EnmOperationType.Add) == EnmOperationType.Add)
                    {
                        _dbConnection.Insert(poco); // Insert the new user record
                        return new Response { IsError = false, Message = "User added successfully" };
                    }

                    // Update Operation: Update the existing user record
                    if ((type & EnmOperationType.Update) == EnmOperationType.Update)
                    {
                        _dbConnection.Update(poco); // Update the existing user record
                        return new Response { IsError = false, Message = "User updated successfully" };
                    }

                    // If none of the operation types match, return an error
                    return new Response { IsError = true, Message = "Invalid operation type" };
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like NLog or Serilog)
                Console.WriteLine($"Error occurred: {ex.Message}");

                // Return an error response
                return new Response { IsError = true, Message = "An error occurred while processing the request." };
            }
        }

        /// <summary>
        /// Validates if a user can be deleted based on existing records in the database.
        /// </summary>
        /// <param name="poco">The user data to validate.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateOnDelete(YMU01 poco)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {

                    YMU01? existingUser = _dbConnection.SingleById<YMU01>(poco.U01F01);
                    if (existingUser == null)
                    {
                        return new Response { IsError = true, Message = "User not found for delete." };
                    }

                    // Validation successful
                    return new Response { IsError = false, Message = "Validation successful" };
                }
            }
            catch (Exception ex)
            {
                // Catch any exceptions and log them, then return a user-friendly error message
                return new Response { IsError = true, Message = $"An error occurred during validation: {ex.Message}" };
            }
        }

        /// <summary>
        /// Validates if a user can be saved based on the specified operation type.
        /// </summary>
        /// <param name="poco">The user data to validate.</param>
        /// <param name="type">The type of operation (Add or Update).</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateOnSave(YMU01 poco, EnmOperationType type)
        {
            try
            {
                using (_dbConnection = _dbFactory.OpenConnection())
                {
                    // Add Operation: Check if user already exists (e.g., based on email or username)
                    if ((type & EnmOperationType.Add) == EnmOperationType.Add)
                    {
                        bool isExist = _dbConnection.Exists<YMU01>(x => x.U01F03 == poco.U01F03); // Assume U01F03 is the email field
                        if (isExist)
                        {
                            return new Response { IsError = true, Message = "User with this email already exists." };
                        }
                    }

                    // Update Operation: Ensure the user exists and can be updated
                    if ((type & EnmOperationType.Update) == EnmOperationType.Update)
                    {
                        YMU01? existingUser = _dbConnection.SingleById<YMU01>(poco.U01F01);
                        if (existingUser == null)
                        {
                            return new Response { IsError = true, Message = "User not found for update." };
                        }

                    }

                    // Validation successful
                    return new Response { IsError = false, Message = "Validation successful" };
                }
            }
            catch (Exception ex)
            {
                // Catch any exceptions and log them, then return a user-friendly error message
                return new Response { IsError = true, Message = $"An error occurred during validation: {ex.Message}" };
            }
        }
    }
}
