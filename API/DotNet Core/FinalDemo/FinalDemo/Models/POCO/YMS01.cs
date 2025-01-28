namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMS01 Class Represents Stock entity.
    /// </summary>
    public class YMS01
    {
        /// <summary>
        /// Gets or Sets the Stock Unique Id.
        /// </summary>
        public int S01F01 { get; set; }
        /// <summary>
        /// Gets or Sets the Stock Name.
        /// </summary>
        public string S02F02 { get; set; }
        /// <summary>
        /// Gets or Sets the Stock Symbol.
        /// </summary>
        public string S03F03 { get; set; }
        /// <summary>
        /// Gets or Sets the Stock Current Price.
        /// </summary>
        public decimal S04F04 { get; set; }
    }
}
