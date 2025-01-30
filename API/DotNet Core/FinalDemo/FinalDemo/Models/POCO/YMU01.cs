using ServiceStack.DataAnnotations;

namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// /The YMU01 class represents User entity.
    /// </summary>
    public class YMU01
    {
        /// <summary>
        /// Gets or Sets user unique id.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int U01F01 { get; set; }
        /// <summary>
        /// Gets or Sets User name.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string U01F02 { get; set; }
        /// <summary>
        /// Gets or Sets the User Email.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Index(Unique = true)]
        public string U01F03 { get; set; }
        /// <summary>
        /// Gets or Sets the User Password.
        /// </summary>
        [StringLength(255)] // Allows for securely hashed passwords
        public string U01F04 { get; set; }
    }
}
