namespace WebAPIVersioning.Models
{
    /// <summary>
    /// The StudentV1 class reperesents version 1 for Student entity
    /// </summary>
    public class StudentV1
    {
        /// <summary>
        /// The Id field is used to uniquely identify student
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name  of student
        /// </summary>
        public string Name { get; set; }
    }
}