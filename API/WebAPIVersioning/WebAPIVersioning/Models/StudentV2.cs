namespace WebAPIVersioning.Models
{
    /// <summary>
    /// The StudentV1 class reperesents version 1 for Student entity
    /// </summary>
    public class StudentV2
    {
        /// <summary>
        /// The Id field is used to uniquely identify student
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The first  of student
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of student
        /// </summary>
        public string LastName { get; set; }
    }
}