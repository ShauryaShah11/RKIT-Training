using ServiceStack.DataAnnotations;

namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMH01 class represents the Stock Price History entity.
    /// </summary>
    public class YMH01
    {
        /// <summary>
        /// Gets or Sets the Stock Price History ID (Primary Key).
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int H01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock ID (Foreign Key).
        /// </summary>
        [ForeignKey(typeof(YMS01), OnDelete = "CASCADE", OnUpdate ="CASCADE")]
        [Index] // Adds an index for faster queries
        public int H01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Date (Required).
        /// </summary>
        [Required]
        [Index] // Indexing dates improves performance for range queries
        public DateTime H01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Price.
        /// </summary>
        [Required] // Ensures price is always set
        [DecimalLength(18, 2)] // Precision: 18 digits total, 2 decimal places
        public decimal H01F04 { get; set; }
    }
}
