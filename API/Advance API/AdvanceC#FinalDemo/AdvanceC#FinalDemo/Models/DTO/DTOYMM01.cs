using System;

namespace AdvanceC_FinalDemo.Models.DTO
{
    /// <summary>
    /// The DTOYMM01 class represents the data transfer object (DTO) for the  Mamber entity.
    /// </summary>
    public class DTOYMM01
    {
        /// <summary>
        /// Gets or Sets Unique Id for Member.
        /// </summary>
        public int M01101 { get; set; }
        /// <summary>
        /// Gets or Sets the member name.
        /// </summary>
        public string M01102 { get; set; }
        /// <summary>
        /// Gets or Sets the Member Email.
        /// </summary>
        public string M01103 { get; set; }
        /// <summary>
        /// Gets or Sets the Member Password.
        /// </summary>
        public string M01104 { get; set; }
        /// <summary>
        /// Gets or Sets the Membership Joining Date.
        /// </summary>
        public DateTime M01105 { get; set; }
    }
}