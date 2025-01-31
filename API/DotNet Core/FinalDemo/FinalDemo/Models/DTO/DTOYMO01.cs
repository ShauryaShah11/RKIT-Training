using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalDemo.Models.DTO
{
    public class DTOYMO01
    {
        /// <summary>
        /// Gets or Sets User Id.
        /// </summary>
        [JsonPropertyName("O01101")]
        public int O01F01 { get; set; }

        /// <summary>
        /// Gets or Sets Stock Id.
        /// </summary>
        [JsonPropertyName("O01102")]
        public int O01F02 { get; set; }

        /// <summary>
        /// Gets or Sets Stock Quantity.
        /// </summary>
        [JsonPropertyName("O01103")]
        public int O01F03 { get; set; }

        /// <summary>
        /// Gets or Sets Stock Purchase Price.
        /// </summary>
        [JsonPropertyName("O01104")]
        public decimal O01F04 { get; set; }
    }
}
