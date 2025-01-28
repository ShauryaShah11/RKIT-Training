using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Extension_Methods;
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
    public class MemberRepository
    {
        private readonly IDbConnection _db;

        static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        // Initialize the OrmLiteConnectionFactory with the connection string and provider
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

        public MemberRepository()
        {
            _db = dbFactory.Open();

            _db.CreateTableIfNotExists<YMM01>();
        }

        public Response HandleOperation(DTOYMM01 dto, OperationType type)
        {
            string email = dto.M01103;
            if (!email.IsValidEmail())
            {
                return new Response { IsError = true, Message = "Invalid email Format" };
            }
            var poco = PreSave(dto);
            var validationResponse = ValidateOnSave(poco, type);
            if (validationResponse.IsError)
            {
                return validationResponse;
            }
            return Save(poco, type);
        }

        public Response GetAllMember()
        {
            try
            {
                var members = _db.Select<YMM01>();
                DataTable memberTable = members.ToDataTable();
                return new Response { IsError = false, Message = "Member Retrieved Succesfully.", Data = memberTable };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        public Response GetMemberById(int id)
        {
            try
            {
                var members = _db.Select<YMM01>().Where(m => m.M01F01 == id);
                DataTable bookDataTable = members.ToDataTable();
                return new Response { IsError = false, Data = bookDataTable, Message = "Book retrieved succesfully." };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        public Response VerifyMemberLogin(string email, string password)
        {
            try
            {
                // Retrieve the member by email from the database
                bool IsExist = _db.Exists<YMM01>(m => m.M01F03 == email && m.M01F04 == PasswordHasher.HashedPassword(password));

                // Check if the hashed input password matches the stored hashed password
                if (IsExist)
                {
                    return new Response { IsError = false, Message = "Login successful." };
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

                // Return the error message in the response
                return new Response { IsError = true, Message = "An error occurred during login." };
            }
        }


        public YMM01 PreSave(DTOYMM01 dto)
        {
            return new YMM01
            {
                M01F01 = dto.M01101,
                M01F02 = dto.M01102,
                M01F03 = dto.M01103,
                M01F04 = PasswordHasher.HashedPassword(dto.M01104),
                M01F05 = DateTime.Now
            };
        }

        public Response ValidateOnSave(YMM01 poco, OperationType type)
        {
            if (type == OperationType.ADD && _db.Exists<YMM01>(m => m.M01F01 == poco.M01F01)) // Compare using equality
            {
                // Perform ADD-specific validation
                return new Response { IsError = true, Message = "Member Already exists." };

            }
            else if (type == OperationType.UPDATE && !_db.Exists<YMM01>(m => m.M01F01 == poco.M01F01))
            {
                // Perform UPDATE-specific validation
                return new Response { IsError = true, Message = "Member not Found" };
            }
            else if (type == OperationType.DELETE && !_db.Exists<YMM01>(m => m.M01F01 == poco.M01F01))
            {
                // Perform DELETE-specific validation
                return new Response { IsError = true, Message = "Member not Found" };
            }

            return new Response { IsError = false }; //return an appropriate response
        }

        public Response Save(YMM01 poco, OperationType type)
        {
            try
            {
                if (poco == null)
                    return new Response { IsError = true, Message = "Invalid data provided." };

                switch (type)
                {
                    case OperationType.ADD:
                        // Add a new record
                        _db.Insert(poco);
                        return new Response { IsError = false, Message = "Member added successfully." };

                    case OperationType.UPDATE:
                        // Update the record with matching Book ID
                        _db.Update(poco);
                        return new Response { IsError = false, Message = "Member updated successfully." };

                    case OperationType.DELETE:
                        // Delete the record with matching Book ID
                        _db.Delete<YMM01>(m => m.M01F01 == poco.M01F01);
                        return new Response { IsError = false, Message = "Member deleted successfully." };

                    default:
                        return new Response { IsError = true, Message = "Invalid operation type." };
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and provide error details
                return new Response
                {
                    IsError = true,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}