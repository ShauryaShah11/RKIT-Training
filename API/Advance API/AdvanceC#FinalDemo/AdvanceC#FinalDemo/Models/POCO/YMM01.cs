using ServiceStack.DataAnnotations;
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
        [PrimaryKey]
        [AutoIncrement]
        public int M01F01 { get; set; }
        /// <summary>
        /// Gets or Sets the member name.
        /// </summary>
        [StringLength(50)]
        [Required]
        public string M01F02 { get; set; }
        /// <summary>
        /// Gets or Sets the Member Email.
        /// </summary>
        [StringLength(50)]
        [Required]
        [Unique]
        public string M01F03 { get; set; }
        /// <summary>
        /// Gets or Sets the Member Password.
        /// </summary>
        [StringLength(64)]
        [Required]
        public string M01F04 { get; set; }
        /// <summary>
        /// Gets or Sets the Membership Joining Date.
        /// </summary>
        [Required]
        public DateTime M01F05 { get; set; } = DateTime.Now;
    }
}