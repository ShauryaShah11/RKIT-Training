using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdvanceC_FinalDemo.Models.DTO
{
    /// <summary>
    /// The DTOYMM01 class represents the data transfer object (DTO) for the Member entity.
    /// </summary>
    public class DTOYMM01
    {
        /// <summary>
        /// Gets or Sets Unique Id for Member (Required and must be a positive integer).
        /// </summary>
        [Required(ErrorMessage = "Member Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Member Id must be a positive integer.")]
        [JsonProperty("M01101")]
        public int M01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the member name (Required and max length of 100 characters).
        /// </summary>
        [Required(ErrorMessage = "Member Name is required.")]
        [StringLength(100, ErrorMessage = "Member Name cannot exceed 100 characters.")]
        [JsonProperty("M01102")]
        public string M01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Member Email (Required and must be a valid email address).
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        [JsonProperty("M01103")]
        public string M01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Member Password (Required and must be at least 6 characters long).
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [JsonProperty("M01104")]
        public string M01F04 { get; set; }

        /// <summary>
        /// Gets or Sets the Membership Joining Date (Required and must be a valid date).
        /// </summary>
        [Required(ErrorMessage = "Joining Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Joining Date format.")]
        [JsonProperty("M01105")]
        public DateTime M01F05 { get; set; }
    }
}
