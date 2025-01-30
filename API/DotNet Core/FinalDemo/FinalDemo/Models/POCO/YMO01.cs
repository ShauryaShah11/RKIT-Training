using ServiceStack.DataAnnotations;

namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMO01 class represents the User OrderBook entity.
    /// </summary>
    [CompositeKey("O01F01", "O01F02")] // Defines composite primary key
    public class YMO01
    {
        /// <summary>
        /// Gets or Sets the User ID (Foreign Key).
        /// </summary>
        [ForeignKey(typeof(YMU01), OnDelete = "CASCADE")]
        [Index] // Improves query performance on user-based lookups
        public int O01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock ID (Foreign Key).
        /// </summary>
        [ForeignKey(typeof(YMS01), OnDelete = "CASCADE")]
        [Index] // Speeds up searches by stock
        public int O01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Quantity (Required).
        /// </summary>
        [Required]
        [CheckConstraint("O01F03 > 0")]
        public int O01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Purchase Price.
        /// </summary>
        [Required]
        [DecimalLength(18, 2)] // Precision: 18 digits total, 2 decimal places
        [CheckConstraint("O01F04 >= 0")]
        public decimal O01F04 { get; set; }
    }
}
