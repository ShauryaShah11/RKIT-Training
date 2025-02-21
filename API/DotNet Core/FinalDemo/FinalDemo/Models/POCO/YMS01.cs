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
        public int S01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Name (Unique).
        /// </summary>
        public string S01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock Symbol (Unique).
        /// </summary>
        public string S01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Stock's Current Price.
        /// </summary>
        public decimal S01F04 { get; set; }
    }
}
