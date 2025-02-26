using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Interface for user-related operations, including authentication, retrieval, and management of users.
    /// </summary>
    public interface IUserService : IBaseService<YMU01, DTOYMU01>
    {
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A response containing a collection of all users.</returns>
        Response GetAllUsers();

        /// <summary>
        /// Retrieves a specific user by their unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A response containing the details of the user with the specified ID.</returns>
        Response GetUserById(int id);

        /// <summary>
        /// Authenticates a user by their email and password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The authenticated user if credentials are valid; otherwise, null.</returns>
        YMU01 Authenticate(string email, string password);
    }
}