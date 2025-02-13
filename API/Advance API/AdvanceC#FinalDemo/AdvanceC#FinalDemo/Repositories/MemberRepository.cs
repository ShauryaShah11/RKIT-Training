using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Extensions;
using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Models.POCO;
using AdvanceC_FinalDemo.Security;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;
using System.Linq;

namespace AdvanceC_FinalDemo.Repositories
{
    /// <summary>
    /// This class handles operations related to Member data, including saving, deleting, and fetching member details.
    /// </summary>
    public class MemberRepository
    {
        private readonly IDbConnection _db;

        public EnmOperationType type;

        private static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        // Initialize the OrmLiteConnectionFactory with the connection string and provider
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

        /// <summary>
        /// Constructor that opens the database connection and ensures the YMM01 table exists.
        /// </summary>
        public MemberRepository()
        {
            _db = dbFactory.Open();
            _db.CreateTableIfNotExists<YMM01>();
        }

        /// <summary>
        /// Retrieves all members from the database.
        /// </summary>
        /// <returns>A response object containing a DataTable of members or an error message.</returns>
        public Response GetAllMember()
        {
            try
            {
                var members = _db.Select<YMM01>();
                DataTable memberTable = members.ToDataTable();
                return new Response { Message = "Member Retrieved Successfully.", Data = memberTable };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Retrieves a member by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the member to retrieve.</param>
        /// <returns>A response object containing the member's data or an error message.</returns>
        public Response GetMemberById(int id)
        {
            try
            {
                var members = _db.Select<YMM01>().Where(m => m.M01F01 == id);
                DataTable memberDataTable = members.ToDataTable();
                return new Response { Data = memberDataTable, Message = "Member retrieved successfully." };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Verifies a member's login credentials.
        /// </summary>
        /// <param name="email">The member's email.</param>
        /// <param name="password">The member's password.</param>
        /// <returns>A response object indicating whether the login was successful or not.</returns>
        public Response VerifyMemberLogin(string email, string password)
        {
            try
            {
                // Retrieve the member by email and verify the hashed password
                bool isExist = _db.Exists<YMM01>(m => m.M01F03 == email && m.M01F04 == PasswordHasher.HashedPassword(password));

                if (isExist)
                {
                    return new Response { Message = "Login successful." };
                }
                else
                {
                    return new Response { IsError = true, Message = "Invalid email or password." };
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error verifying member login: {ex.Message}");
                return new Response { IsError = true, Message = "An error occurred during login." };
            }
        }

        /// <summary>
        /// Retrieves the total count of records (members) in the YMB01 table.
        /// The method executes a SQL query to count all rows in the YMB01 table and returns the count in the response.
        /// </summary>
        /// <returns>A Response object containing the total count of members or a default message in case of an error.</returns>
        public Response GetMemberCount()
        {
            try
            {
                // The query returns a single value: the count of records in the table YMM01
                int count = _db.Scalar<int>("SELECT COUNT(*) FROM YMM01");

                return new Response { Message = $"Total Member count is : {count}" }; // Return the count of rows
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions (e.g., database connectivity issues)
                return new Response { IsError = true, Message = $"Total Member count is : {0}" }; // Return the count of rows
            }
        }

        /// <summary>
        /// Prepares a DTO (Data Transfer Object) for saving by converting it to a POCO (Plain Old CLR Object).
        /// </summary>
        /// <param name="dto">The DTO to convert.</param>
        /// <returns>A POCO instance representing the member.</returns>
        public YMM01 PreSave(DTOYMM01 dto)
        {
            return dto.ToPoco<YMM01>();
        }

        /// <summary>
        /// Prepares a DTO for deletion by converting it to a POCO.
        /// </summary>
        /// <param name="dto">The DTO to convert for deletion.</param>
        /// <returns>A POCO instance for deletion.</returns>
        public YMM01 PreDelete(DTOYMM01 dto)
        {
            return dto.ToPoco<YMM01>();
        }

        /// <summary>
        /// Validates the member data before saving based on the operation type (ADD, UPDATE).
        /// </summary>
        /// <param name="pocoM01">The POCO to validate.</param>
        /// <returns>A response indicating whether validation passed or failed.</returns>
        public Response ValidateOnSave(YMM01 pocoM01)
        {
            if (type == EnmOperationType.ADD && _db.Exists<YMM01>(m => m.M01F01 == pocoM01.M01F01)) // Check if the member already exists
            {
                return new Response { IsError = true, Message = "Member already exists." };
            }
            else if (type == EnmOperationType.UPDATE && !_db.Exists<YMM01>(m => m.M01F01 == pocoM01.M01F01))
            {
                return new Response { IsError = true, Message = "Member not found." };
            }

            return new Response { IsError = false };
        }

        /// <summary>
        /// Validates the member data before deleting.
        /// </summary>
        /// <param name="pocoM01">The POCO to validate for deletion.</param>
        /// <returns>A response indicating whether the deletion can proceed or not.</returns>
        public Response ValidateOnDelete(YMM01 pocoM01)
        {
            bool isExist = _db.Exists<YMM01>(m => m.M01F01 == pocoM01.M01F01);
            if (!isExist)
            {
                return new Response { IsError = true, Message = "Member not found for deletion." };
            }
            return new Response { IsError = false };
        }

        /// <summary>
        /// Saves a member to the database (either adds or updates based on the operation type).
        /// </summary>
        /// <param name="pocoM01">The POCO to save.</param>
        /// <param name="type">The type of operation (ADD, UPDATE).</param>
        /// <returns>A response indicating whether the operation was successful or not.</returns>
        public Response Save(YMM01 pocoM01)
        {
            try
            {
                if (type == EnmOperationType.ADD)
                {
                    // Add a new record
                    _db.Insert(pocoM01);
                    return new Response { Message = "Member added successfully." };
                }
                else
                {
                    // Update an existing record
                    _db.Update(pocoM01);
                    return new Response { Message = "Member updated successfully." };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsError = true,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Deletes a member from the database based on the provided POCO.
        /// </summary>
        /// <param name="pocoM01">The POCO of the member to delete.</param>
        /// <returns>A response indicating whether the delete operation was successful or not.</returns>
        public Response Delete(YMM01 pocoM01)
        {
            try
            {
                // Delete the member with the given ID
                _db.Delete<YMM01>(m => m.M01F01 == pocoM01.M01F01);
                return new Response { Message = "Member deleted successfully." };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsError = true,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
