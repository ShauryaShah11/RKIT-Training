namespace WebAPIFinalDemo.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// The Password property is stored securely (hashed) to ensure user security.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// Password should be stored securely (hashed).
        /// </summary>
        public string Password { get; set; }
    }
}
