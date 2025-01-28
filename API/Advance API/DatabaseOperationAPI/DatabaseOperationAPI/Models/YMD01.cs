using System;

namespace DatabaseOperationAPI.Models.POCO
{
    /// <summary>
    /// The YMD01 Class represents Department entity in Database.
    /// </summary>
    public class YMD01
    {
        /// <summary>
        /// Gets or sets the unique id of the department.
        /// </summary>
        public int D01F01 { get; set; }
        /// <summary>
        /// Gets or sets the short name of department.
        /// </summary>
        public string D01F02 { get; set; }
        /// <summary>
        /// Gets or sets Name of the department.
        /// </summary>
        public string D01F03 { get; set; }
        /// <summary>
        /// Gets or sets Head name of the department.
        /// </summary>
        public string D01F04 { get; set; } 
    }
}