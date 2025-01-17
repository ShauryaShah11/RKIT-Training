using System;

namespace AdvanceC_FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMM01 class represents Member entity in Database.
    /// </summary>
    public class YMM01
    {
        /// <summary>
        /// Gets or Sets Unique Id for Member.
        /// </summary>
        public int M01F01 { get; set; }
        /// <summary>
        /// Gets or Sets the member name.
        /// </summary>
        public string M02F02 { get; set; }
        /// <summary>
        /// Gets or Sets the Member Email.
        /// </summary>
        public string M03F03 { get; set; }
        /// <summary>
        /// Gets or Sets the Member Password.
        /// </summary>
        public string M04F04 { get; set; }
        /// <summary>
        /// Gets or Sets the Membership Joining Date.
        /// </summary>
        public DateTime M05F05 { get; set; } = DateTime.Now;
    }
}