using FinalDemo.Models.DTO;
using FinalDemo.Models;
using FinalDemo.Models.POCO;
using System.Collections.Generic;
using FinalDemo.Enums;

namespace FinalDemo.Interfaces
{
    public interface IUserService: IBaseService<YMU01, DTOYMU01>
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
        
    }
}
