namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMH01 class represents Stock Price History entity.
    /// </summary>
    public class YMH01
    {
        /// <summary>
        /// Gets or Sets the Stock Price History Id.
        /// </summary>
        public int H01F01 { get; set; }
        /// <summary>
        /// Gets or Sets the Stock Id.
        /// </summary>
        public int H02F02 { get; set; }
        /// <summary>
        /// Gets or Sets the Date.
        /// </summary>
        public DateTime H03F03 { get; set; }
        /// <summary>
        /// Gets or Sets the Stock Price.
        /// </summary>
        public decimal H04F04 { get; set; }
    }
}
