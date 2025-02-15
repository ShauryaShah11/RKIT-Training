﻿using FinalDemo.Enums;
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
        /// <param name="type">The type of operation (Insert, Update, etc.).</param>
        /// <returns>A response indicating success or validation errors.</returns>
        Response ValidateOnSave(T1 poco, EnmOperationType type);

        /// <summary>
        /// Saves a POCO entity to the database.
        /// </summary>
        /// <param name="poco">The entity to save.</param>
        /// <param name="type">The type of operation (Insert, Update, etc.).</param>
        /// <returns>A response indicating the result of the save operation.</returns>
        Response Save(T1 poco, EnmOperationType type);

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

        // <summary>
        /// Handles an operation (Insert, Update, Delete) on a DTO entity.
        /// This method determines the operation type and executes the appropriate logic.
        /// </summary>
        /// <param name="dto">The entity data transfer object to process.</param>
        /// <param name="type">The type of operation to perform.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        Response HandleOperation(T2 dto, EnmOperationType type);

    }
}
