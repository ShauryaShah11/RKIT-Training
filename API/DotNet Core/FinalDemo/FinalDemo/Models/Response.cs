namespace FinalDemo.Models
{
    /// <summary>
    /// Represents a standard API response model.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Indicates whether the response contains an error.
        /// Defaults to false.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Provides a message describing the response outcome.
        /// Can be null if no message is required.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Holds the actual response data.
        /// Can be of any type (dynamic).
        /// </summary>
        public dynamic Data { get; set; }
    }
}
