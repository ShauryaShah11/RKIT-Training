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
        [References(typeof(YMU01))]
        public int O01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock ID (Foreign Key).
        /// </summary>
        [References(typeof(YMS01))]
        public int O01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Quantity (Required).
        /// </summary>
        public int O01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Purchase Price.
        /// </summary>
        public decimal O01F04 { get; set; }
    }
}
