namespace AdvanceC_FinalDemo.Models.DTO
{
    /// <summary>
    /// The DTOYMB01 class represents the data transfer object (DTO) for the book entity.
    /// </summary>
    public class DTOYMB01
    {
        /// <summary>
        /// Gets or Sets the Unique id for book entity.
        /// </summary>
        public int B01101 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Author Name.
        /// </summary>
        public string B01102 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Name.
        /// </summary>
        public string B01103 { get; set; }

        /// <summary>
        /// Gets or Sets the Book's category.
        /// </summary>
        public string B01104 { get; set; }

        /// <summary>
        /// Gets or Sets the available copy of books.
        /// </summary>
        public int B01105 { get; set; }

        /// <summary>
        /// Gets or Sets the Book published year (only year, not full date).
        /// </summary>
        public int B01106 { get; set; }
    }
}