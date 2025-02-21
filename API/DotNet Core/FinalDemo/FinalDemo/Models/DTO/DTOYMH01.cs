using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalDemo.Models.DTO
{
    public class DTOYMH01
    {
        /// <summary>
        /// Gets or Sets the Stock Price History Id.
        /// </summary>
        [Required(ErrorMessage = "H01101 (Stock Price History ID) is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "H01101 (Stock Price History ID) must be a positive number.")]
        [JsonPropertyName("H01101")]
        public int H01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Id.
        /// </summary>
        [Required(ErrorMessage = "H01102 (Stock ID) is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "H01102 (Stock ID) must be a positive number.")]
        [JsonPropertyName("H01102")]
        public int H01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Date.
        /// </summary>
        [Required(ErrorMessage = "H01103 (Date) is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(DTOYMH01), nameof(ValidateDate))]
        [JsonPropertyName("H01103")]
        public DateTime H01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Price.
        /// </summary>
        [Required(ErrorMessage = "H01104 (Stock Price) is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "H01104 (Stock Price) must be between 0.01 and 1,000,000.")]
        [DataType(DataType.Currency)]
        [JsonPropertyName("H01104")]
        public decimal H01F04 { get; set; }

        /// <summary>
        /// Custom validation method to ensure the date is not in the future.
        /// </summary>
        public static ValidationResult ValidateDate(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Today)
            {
                return new ValidationResult("H01103 (Date) cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
