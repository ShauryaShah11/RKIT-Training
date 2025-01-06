namespace ActionMethodResponse.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user's unique identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user's location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the user's location pin address.
        /// </summary>
        public int PIN { get; set; }

        /// <summary>
        /// Gets or sets the user's age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the user's sex.
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Gets or sets the user's occupation.
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Gets or sets the user's annual income.
        /// </summary>
        public float AnnualIncome { get; set; }

        /// <summary>
        /// Gets or sets the user's marital status.
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the user's nationality.
        /// </summary>
        public string Nationality { get; set; }
    }
}
