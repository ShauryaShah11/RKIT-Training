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
        public string D02102 { get; set; }

        /// <summary>
        /// Gets or Sets the full name of the department.
        /// </summary>
        public string D03103 { get; set; }

        /// <summary>
        /// Gets or Sets the name of the department head.
        /// </summary>
        public string D04104 { get; set; }
    }
}
