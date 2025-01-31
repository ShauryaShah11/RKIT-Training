using System.Text.Json.Serialization;

namespace FinalDemo.Models.DTO
{
    public class DTOYMH01
    {
        /// <summary>
        /// Gets or Sets the Stock Price History Id.
        /// </summary>
        [JsonPropertyName("H01101")]
        public int H01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Id.
        /// </summary>
        [JsonPropertyName("H01102")]
        public int H01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Date.
        /// </summary>
        [JsonPropertyName("H01103")]
        public DateTime H01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Price.
        /// </summary>
        [JsonPropertyName("H01104")]
        public decimal H01F04 { get; set; }
    }
}
