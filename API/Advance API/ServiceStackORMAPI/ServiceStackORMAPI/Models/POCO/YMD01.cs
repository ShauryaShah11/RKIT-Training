using ServiceStack.DataAnnotations;

namespace ServiceStackORMAPI.Models.POCO
{
    /// <summary>
    /// The YMD01 class represents the POCO object of department entity.
    /// </summary>
    public class YMD01
    {
        /// <summary>
        /// Gets or Sets the unique identifier of the department.
        /// This is the primary key and auto-increments with each new entry.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int D01F01 { get; set; }   // DepartmentId

        /// <summary>
        /// Gets or Sets the short name of the department.
        /// The maximum length for this field is 50 characters.
        /// </summary>
        [StringLength(50)]
        public string D02F02 { get; set; } // Short Department Name

        /// <summary>
        /// Gets or Sets the full name of the department.
        /// This field is required and the maximum length is 50 characters.
        /// </summary>
        [StringLength(50)]
        [Required]
        public string D03F03 { get; set; } // Full Department Name

        /// <summary>
        /// Gets or Sets the name of the department head.
        /// This field is required and the maximum length is 50 characters.
        /// </summary>
        [StringLength(50)]
        [Required]
        public string D04F04 { get; set; } // Department Head
    }
}