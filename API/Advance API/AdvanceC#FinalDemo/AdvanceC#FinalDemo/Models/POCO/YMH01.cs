﻿using ServiceStack.DataAnnotations;
using System;

namespace AdvanceC_FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMH01 class represents the Book History Entity in the database.
    /// </summary>
    public class YMH01
    {
        /// <summary>
        /// Gets or Sets the Book Id.
        /// </summary>
        [References(typeof(YMB01))]
        public int H01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Member Id.
        /// </summary>
        [References(typeof(YMM01))]
        public int H01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Issued Date.
        /// </summary>
        public DateTime H01F03 { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or Sets the Book Return Date.
        /// </summary>
        public DateTime? H01F04 { get; set; } = null;
    }
}