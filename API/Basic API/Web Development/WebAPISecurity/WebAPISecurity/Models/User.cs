namespace WebAPISecurity.Models
{
    /// <summary>
    /// The User class represents user model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The password of the user.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// The email of the user.
        /// </summary>
        public string Email { get; set; }
    }
}