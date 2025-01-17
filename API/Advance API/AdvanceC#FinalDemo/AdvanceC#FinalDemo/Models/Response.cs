using System.Data;

namespace AdvanceC_FinalDemo.Models
{
    /// <summary>
    /// Represents a standard response model for API operations, encapsulating the result data, 
    /// error status, and additional messages.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the data returned by the operation in a DataTable format.
        /// </summary>
        public DataTable Data { get; set; }

        /// <summary>
        /// Indicates whether an error occurred during the operation.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Gets or sets a message providing details about the operation's result or error.
        /// </summary>
        pu
    }