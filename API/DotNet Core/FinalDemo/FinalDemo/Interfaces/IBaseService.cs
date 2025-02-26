using FinalDemo.Enums;
using FinalDemo.Models;
using FinalDemo.Models.DTO;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Defines common service operations for entities.
    /// Provides methods for saving, validating, and deleting records.
    /// </summary>
    /// <typeparam name="T1">The POCO (Persistence Object) that represents the entity in the database.</typeparam>
    /// <typeparam name="T2">The DTO (Data Transfer Object) used for data exchange.</typeparam>
    public interface IBaseService<T1, T2>
    {
        /// <summary>
        /// Prepares the DTO before converting it to POCO for saving.
        /// </summary>
        /// <param name="dto">The entity data transfer object.</param>
        /// <returns>The POCO entity ready for saving.</returns>
        T1 PreSave(T2 dto);

        /// <summary>
        /// Validates a POCO entity before saving.
        /// </summary>
        /// <param name="poco">The entity to validate.</param>
        /// <returns>A response indicating success or validation errors.</returns>
        Response ValidateOnSave(T1 poco);

        /// <summary>
        /// Saves a POCO entity to the database.
        /// </summary>
        /// <param name="poco">The entity to save.</param>
        /// <returns>A response indicating the result of the save operation.</returns>
        Response Save(T1 poco);

        /// <summary>
        /// Prepares the DTO before converting it to POCO for deletion.
        /// </summary>
        /// <param name="dto">The entity data transfer object.</param>
        /// <returns>The POCO entity ready for deletion.</returns>
        T1 PreDelete(T2 dto);

        /// <summary>
        /// Validates a POCO entity before deletion.
        /// </summary>
        /// <param name="poco">The entity to validate.</param>
        /// <returns>A response indicating success or validation errors.</returns>
        Response ValidateOnDelete(T1 poco);

        /// <summary>
        /// Deletes a POCO entity from the database.
        /// </summary>
        /// <param name="poco">The entity to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        Response Delete(T1 poco);

        /// <summary>
        /// Set Operation Type
        /// </summary>
        /// <param name="type">The operation type to set</param>
        void SetOperationType(EnmOperationType type);
    }
}
