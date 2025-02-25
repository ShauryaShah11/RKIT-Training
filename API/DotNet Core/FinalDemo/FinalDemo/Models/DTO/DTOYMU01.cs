using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTO
{
    public class DTOYMU01
    {
        /// <summary>
        /// Gets or Sets user unique id.
        /// </summary>
        [Required(ErrorMessage = "U01101 (User ID) is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "U01101 (User ID) must be a positive integer.")]
        [JsonProperty("U01101")]
        public int U01F01 { get; set; }

        /// <summary>
        /// Gets or Sets User name.
        /// </summary>
        [Required(ErrorMessage = "U01102 (User Name) is required.")]
        [StringLength(50, ErrorMessage = "U01102 (User Name) must be at most 50 characters long.")]
        [JsonProperty("U01102")]
        public string U01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the User Email.
        /// </summary>
        [Required(ErrorMessage = "U01103 (Email) is required.")]
        [EmailAddress(ErrorMessage = "U01103 (Email) is not a valid email address.")]
        [JsonProperty("U01103")]
        public string U01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the User Password.
        /// </summary>
        [Required(ErrorMessage = "U01104 (Password) is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "U01104 (Password) must be between 8 and 100 characters.")]
        [JsonProperty("U01104")]
        public string U01F04 { get; set; }
    }
}
