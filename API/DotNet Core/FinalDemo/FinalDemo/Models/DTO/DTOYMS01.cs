using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalDemo.Models.DTO
{
    public class DTOYMS01
    {
        /// <summary>
        /// Gets or Sets the Stock Unique Id.
        /// </summary>
        [JsonPropertyName("S01101")]
        public int S01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Name.
        /// </summary>
        [Required] // Ensuring Stock Name is required
        [StringLength(100)] // Stock Name has a max length of 100 characters
        [JsonPropertyName("S01102")]
        public string S01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Symbol.
        /// </summary>
        [Required] // Ensuring Stock Symbol is required
        [StringLength(10)] // Stock Symbol has a max length of 10 characters
        [JsonPropertyName("S01103")]
        public string S01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Current Price.
        /// </summary>
        [Required] // Ensuring Current Price is required
        [JsonPropertyName("S01104")]
        public decimal S01F04 { get; set; }
    }
}
