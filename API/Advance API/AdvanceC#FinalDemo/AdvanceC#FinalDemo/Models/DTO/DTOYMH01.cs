﻿using System;

namespace AdvanceC_FinalDemo.Models.DTO
{
    /// <summary>
    /// The DTOYMH01 class represents the data transfer object (DTO) for the  book history.
    /// </summary>
    public class DTOYMH01
    {
        /// <summary>
        /// Gets or Sets the Book Id.
        /// </summary>
        public int H01101 { get; set; }
        /// <summary>
        /// Gets or Sets the Member Id.
        /// </summary>
        public int H02102 { get; set; }
        /// <summary>
        /// Gets or Sets the Book Issue Date.
        /// </summary>
        public DateTime H03103 { get; set; }
        /// <summary>
        /// Gets or Sets the Book Return Date.
        /// </summary>
        public DateTime? H04104 { get; set; }

    }
}