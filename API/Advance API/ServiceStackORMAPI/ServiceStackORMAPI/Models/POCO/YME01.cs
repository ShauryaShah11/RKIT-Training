using ServiceStack.DataAnnotations;
using System;

namespace ServiceStackORMAPI.Models.POCO
{
    /// <summary>
    /// The YME01 class represents the POCO object of employee entity.
    /// </summary>
    public class YME01
    {
        /// <summary>
        /// Gets or Sets the unique identifier of the employee.
        /// This is the primary key and auto-increments with each new entry.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int E01F01 { get; set; } // Employee Id

        /// <summary>
        /// Gets or Sets the name of the employee.
        /// The maximum length for this field is 50 characters.
        /// </summary>
        [StringLength(50)]
        public string E01F02 { get; set; } // Employee Name

        /// <summary>
        /// Gets or Sets the hire date of the employee.
        /// </summary>
        public DateTime E01F03 { get; set; } // Hire Date

        /// <summary>
        /// Gets or Sets the department ID, which is a foreign key to the department table (YMD01).
        /// This establishes the relationship between the employee and the department they belong to.
        /// </summary>
        [ForeignKey(typeof(YMD01))]
        public int E01F04 { get; set; } // Foreign Key -> DEPT01 Table 
    }
}