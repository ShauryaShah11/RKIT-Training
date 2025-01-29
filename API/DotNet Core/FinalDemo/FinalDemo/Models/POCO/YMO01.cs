namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMO01 class reprsents User OrderBook entity.
    /// </summary>
    public class YMO01
    {
        /// <summary>
        /// Gets or Sets User Id.
        /// </summary>
        public int O01F01 { get; set; }
        /// <summary>
        /// Gets or Sets Stock Id.
        /// </summary>
        public int O01F02 { get; set; }
        /// <summary>
        /// Gets or Sets Stock Qunatity.
        /// </summary>
        public int O01F03 { get; set; }
        /// <summary>
        /// Gets or Sets Stock Purchase Price.
        /// </summary>
        public decimal O01F04 { get; set; }
    }
}
