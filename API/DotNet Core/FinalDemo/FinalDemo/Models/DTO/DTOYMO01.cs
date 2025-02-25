using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalDemo.Models.DTO
{
    public class DTOYMO01
    {
        /// <summary>
        /// Gets or Sets User Id.
        /// </summary>
        [JsonProperty("O01101")]
        [Required(ErrorMessage = "User Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User Id must be a positive integer.")]
        public int O01F01 { get; set; }

        /// <summary>
        /// Gets or Sets Stock Id.
        /// </summary>
        [JsonProperty("O01102")]
        [Required(ErrorMessage = "Stock Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Stock Id must be a positive integer.")]
        public int O01F02 { get; set; }

        /// <summary>
        /// Gets or Sets Stock Quantity.
        /// </summary>
        [JsonProperty("O01103")]
        [Required(ErrorMessage = "Stock Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Stock Quantity must be at least 1.")]
        public int O01F03 { get; set; }

        /// <summary>
        /// Gets or Sets Stock Purchase Price.
        /// </summary>
        [JsonProperty("O01104")]
        [Required(ErrorMessage = "Stock Purchase Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Stock Purchase Price must be greater than zero.")]
        public decimal O01F04 { get; set; }
    }
}
