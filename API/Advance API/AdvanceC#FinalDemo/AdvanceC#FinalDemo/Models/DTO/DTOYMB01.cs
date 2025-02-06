using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AdvanceC_FinalDemo.Models.DTO
{
    /// <summary>
    /// The DTOYMB01 class represents the data transfer object for the book entity with validation.
    /// </summary>
    public class DTOYMB01
    {
        /// <summary>
        /// Unique ID for the book entity. (Auto-generated)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "B01101 must be a positive integer.")]
        [JsonProperty("B01101")]
        public int B01F01 { get; set; }

        /// <summary>
        /// Book Author Name. (Required, Max Length: 50)
        /// </summary>
        [Required(ErrorMessage = "B01102 (Author Name) is required.")]
        [StringLength(50, ErrorMessage = "B01102 (Author Name) must be at most 50 characters long.")]
        [JsonProperty("B01102")]
        public string B01F02 { get; set; }

        /// <summary>
        /// Book Name. (Required, Max Length: 50)
        /// </summary>
        [Required(ErrorMessage = "B01103 (Book Name) is required.")]
        [StringLength(50, ErrorMessage = "B01103 (Book Name) must be at most 50 characters long.")]
        [JsonProperty("B01103")]
        public string B01F03 { get; set; }

        /// <summary>
        /// Book Category. (Optional, Max Length: 50)
        /// </summary>
        [StringLength(50, ErrorMessage = "B01104 (Category) must be at most 50 characters long.")]
        [JsonProperty("B01104")]
        public string B01F04 { get; set; }

        /// <summary>
        /// Number of Available Copies. (Required, Must be a positive integer)
        /// </summary>
        [Required(ErrorMessage = "B01105 (Available Copies) is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "B01105 (Available Copies) must be a non-negative integer.")]
        [JsonProperty("B01105")]
        public int B01F05 { get; set; }

        /// <summary>
        /// Published Year. (Required, Must be a valid year between 1900 and the current year)
        /// </summary>
        [Required(ErrorMessage = "B01106 (Published Year) is required.")]
        [Range(1900, 2100, ErrorMessage = "B01106 (Published Year) must be between 1900 and 2100.")]
        [JsonProperty("B01106")]
        public int B01F06 { get; set; }
    }
}
