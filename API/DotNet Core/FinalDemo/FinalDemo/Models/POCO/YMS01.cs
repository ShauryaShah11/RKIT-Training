using ServiceStack.DataAnnotations;

namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMS01 class represents a Stock entity.
    /// </summary>
    public class YMS01
    {
        /// <summary>
        /// Gets or Sets the Stock Unique ID (Primary Key).
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int S01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Name (Unique).
        /// </summary>
        [Required] // Ensures this field is mandatory
        [StringLength(50)] // Restricts length to 50 characters
        [Index(Unique = true)] // Creates a unique index for performance
        public string S01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Symbol (Unique).
        /// </summary>
        [Required] // Ensures stock symbol is mandatory
        [StringLength(10)] // Limits the symbol length (e.g., AAPL, TSLA)
        [Index(Unique = true)] // Ensures uniqueness
        public string S01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock's Current Price.
        /// </summary>
        [DecimalLength(18, 2)] // Precision: 18 digits total, 2 decimal places
        public decimal S01F04 { get; set; }
    }
}
