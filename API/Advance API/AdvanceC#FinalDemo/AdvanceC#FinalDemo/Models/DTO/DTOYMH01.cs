using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdvanceC_FinalDemo.Models.DTO
{
    /// <summary>
    /// The DTOYMH01 class represents the data transfer object (DTO) for the book history.
    /// </summary>
    public class DTOYMH01
    {
        /// <summary>
        /// Gets or Sets the Book Id (Required and must be greater than 0).
        /// </summary>
        [Required(ErrorMessage = "Book Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Book Id must be a positive integer.")]
        [JsonProperty("H01101")]
        public int H01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Member Id (Required and must be greater than 0).
        /// </summary>
        [Required(ErrorMessage = "Member Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Member Id must be a positive integer.")]
        [JsonProperty("H01102")]
        public int H01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Issue Date (Required and must be a valid date).
        /// </summary>
        [Required(ErrorMessage = "Issue Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Issue Date format.")]
        [JsonProperty("H01103")]
        public DateTime H01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Return Date (Optional, but must be a valid date if provided).
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Invalid Return Date format.")]
        [JsonProperty("H01104")]
        public DateTime? H01F04 { get; set; }
    }
}
