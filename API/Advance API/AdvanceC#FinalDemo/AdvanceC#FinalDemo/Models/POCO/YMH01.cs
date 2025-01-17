using System;

namespace AdvanceC_FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMH01 class represents Book History Entity in database.
    /// </summary>
    public class YMH01
    {
        /// <summary>
        /// Gets or Sets the Book Id.
        /// </summary>
        public int H01F01 { get; set; }
        /// <summary>
        /// Gets or Sets the Member Id.
        /// </summary>
        public int H02F02 { get; set; }
        /// <summary>
        /// Gets or Sets the Book Issued Date.
        /// </summary>
        public DateTime H03F03 { get; set; } = DateTime.Now;
        /// <summary>
        /// Gets or Sets the Book Return Date.
        /// </summary>
        public int H04F04 { get; set; }
    }
}