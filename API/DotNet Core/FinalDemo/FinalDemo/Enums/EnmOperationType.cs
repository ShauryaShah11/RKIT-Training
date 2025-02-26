namespace FinalDemo.Enums
{
    /// <summary>
    /// Enum representing the different types of operations that can be performed in the application.
    /// </summary>
    [Flags]
    public enum EnmOperationType
    {
        /// <summary>
        /// Represents the addition of a new entity.
        /// </summary>
        A,

        /// <summary>
        /// Represents updating an existing entity.
        /// </summary>
        U,

        /// <summary>
        /// Represents the deletion of an entity.
        /// </summary>
        D,
    }
}
