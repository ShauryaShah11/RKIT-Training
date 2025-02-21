namespace FilterPractice.Models
{
    /// <summary>
    /// Represents the login credentials provided by a user.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
