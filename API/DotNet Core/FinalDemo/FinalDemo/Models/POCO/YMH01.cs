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
        public int H01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock ID (Foreign Key).
        /// </summary>
        public int H01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Date (Required).
        /// </summary>
        public DateTime H01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Price.
        /// </summary>
        public decimal H01F04 { get; set; }
    }
}
