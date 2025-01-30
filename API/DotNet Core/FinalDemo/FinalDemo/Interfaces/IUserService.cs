using FinalDemo.Models.DTO;
using FinalDemo.Models;
using FinalDemo.Models.POCO;
using System.Collections.Generic;
using FinalDemo.Enums;

namespace FinalDemo.Interfaces
{
    public interface IUserService
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
        /// Prepares a user for saving by converting a DTO (Data Transfer Object) into a POCO (Plain Old CLR Object).
        /// </summary>
        /// <param name="dto">The data transfer object representing the user to be saved.</param>
        /// <returns>The corresponding user POCO object ready for saving.</returns>
        YMU01 PreSave(DTOYMU01 dto);

        /// <summary>
        /// Validates the user before saving to ensure all conditions are met.
        /// </summary>
        /// <param name="poco">The POCO object representing the user to be validated.</param>
        /// <returns>A response indicating whether the user data is valid for saving.</returns>
        Response ValidateOnSave(YMU01 poco, OperationType type);

        /// <summary>
        /// Saves the provided user details to the database.
        /// </summary>
        /// <param name="poco">The user POCO object to be saved.</param>
        /// <returns>A response indicating the success or failure of the save operation.</returns>
        Response Save(YMU01 poco, OperationType type);

        /// <summary>
        /// Prepares a user for deletion by converting a DTO (Data Transfer Object) to a POCO (Plain Old CLR Object).
        /// </summary>
        /// <param name="dto">The data transfer object representing the user to be deleted.</param>
        /// <returns>The corresponding user POCO object ready for deletion.</returns>
        YMU01 PreDelete(DTOYMU01 dto);

        /// <summary>
        /// Validates a user before deletion to ensure it can be safely removed.
        /// </summary>
        /// <param name="poco">The POCO object representing the user to be validated for deletion.</param>
        /// <returns>A response indicating whether the user can be deleted.</returns>
        Response ValidateOnDelete(YMU01 poco);

        /// <summary>
        /// Deletes the specified user from the database.
        /// </summary>
        /// <param name="poco">The POCO object representing the user to be deleted.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        Response Delete(YMU01 poco);

        Response HandleOperation(DTOYMU01 user, OperationType type);
    }
}
