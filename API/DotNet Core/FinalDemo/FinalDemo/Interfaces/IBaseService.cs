using FinalDemo.Enums;
using FinalDemo.Models;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Defines common service operations for entities.
    /// Provides methods for saving, validating, and deleting records.
    /// </summary>
    /// <typeparam name="T">The type of entity that the service operates on.</typeparam>
    public interface IBaseService<T>
    {
        /// <summary>
        /// Prepares the entity before saving, allowing modifications or validations.
        /// </summary>
        /// <param name="dto">The entity data transfer object before saving.</param>
        /// <returns>The modified entity ready for saving.</returns>
        T PreSave(T dto);

        /// <summary>
        /// Validates an entity before saving.
        /// </summary>
        /// <param name="poco">The entity to validate.</param>
        /// <param name="type">The type of operation (Insert, Update, etc.).</param>
        /// <returns>A response indicating success or validation errors.</returns>
        Response ValidateOnSave(T poco, OperationType type);

        /// <summary>
        /// Saves an entity to the database.
        /// </summary>
        /// <param name="poco">The entity to save.</param>
        /// <param name="type">The type of operation (Insert, Update, etc.).</param>
        /// <returns>A response indicating the result of the save operation.</returns>
        Response Save(T poco, OperationType type);

        /// <summary>
        /// Prepares the entity before deletion, allowing modifications or validations.
        /// </summary>
        /// <param name="dto">The entity data transfer object before deletion.</param>
        /// <returns>The modified entity ready for deletion.</returns>
        T PreDelete(T dto);

        /// <summary>
        /// Validates an entity before deletion.
        /// </summary>
        /// <param name="poco">The entity to validate.</param>
        /// <returns>A response indicating success or validation errors.</returns>
        Response ValidateOnDelete(T poco);

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="poco">The entity to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        Response Delete(T poco);
    }
}
