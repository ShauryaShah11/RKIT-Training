using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalDemo.Models.DTO
{
    public class DTOYMU01
    {
        /// <summary>
        /// Gets or Sets user unique id.
        /// </summary>
        [JsonPropertyName("U01101")]
        public int U01F01 { get; set; }
        /// <summary>
        /// Gets or Sets User name.
        /// </summary>
        [JsonPropertyName("U01102")]
        public string U01F02 { get; set; }
        /// <summary>
        /// Gets or Sets the User Email.
        /// </summary>
        [JsonPropertyName("U01103")]
        public string U01F03 { get; set; }
        /// <summary>
        /// Gets or Sets the User Password.
        /// </summary>
        [JsonPropertyName("U01104")]
        public string U01F04 { get; set; }
    }
}
