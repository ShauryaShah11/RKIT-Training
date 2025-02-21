using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalDemo.Models.DTO
{
    public class DTOYMS01
    {
        /// <summary>
        /// Gets or Sets the Stock Unique Id.
        /// </summary>
        [Required(ErrorMessage = "S01101 (Stock ID) is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "S01101 (Stock ID) must be a positive number.")]
        [JsonPropertyName("S01101")]
        public int S01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Name.
        /// </summary>
        [Required(ErrorMessage = "S01102 (Stock Name) is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "S01102 (Stock Name) must be between 3 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "S01102 (Stock Name) can only contain letters, numbers, and spaces.")]
        [JsonPropertyName("S01102")]
        public string S01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Symbol.
        /// </summary>
        [Required(ErrorMessage = "S01103 (Stock Symbol) is required.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "S01103 (Stock Symbol) must be between 2 and 10 characters.")]
        [RegularExpression(@"^[A-Z0-9]{2,10}$", ErrorMessage = "S01103 (Stock Symbol) must be uppercase letters or numbers.")]
        [JsonPropertyName("S01103")]
        public string S01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Current Price.
        /// </summary>
        [Required(ErrorMessage = "S01104 (Stock Price) is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "S01104 (Stock Price) must be between 0.01 and 1,000,000.")]
        [DataType(DataType.Currency)]
        [JsonPropertyName("S01104")]
        public decimal S01F04 { get; set; }
    }
}
