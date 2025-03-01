﻿using System.ComponentModel.DataAnnotations;

namespace ServiceStackORMAPI.Models.DTO
{
    /// <summary>
    /// The DTOYMD01 class represents the data transfer object (DTO) for the department entity.
    /// </summary>
    public class DTOYMD01
    {
        /// <summary>
        /// Gets or Sets the unique identifier of the department.
        /// </summary>
        public int D01101 { get; set; }

        /// <summary>
        /// Gets or Sets the short name or abbreviation of the department.
        /// </summary>
        [Required]
        [StringLength(10)]
        public string D01102 { get; set; }

        /// <summary>
        /// Gets or Sets the full name of the department.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string D01103 { get; set; }

        /// <summary>
        /// Gets or Sets the name of the department head.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string D01104 { get; set; }
    }
}
