using ServiceStack.DataAnnotations;

namespace AdvanceC_FinalDemo.Models.POCO
{
    /// <summary>
    /// The YMB01 class represents book entity in database.
    /// </summary>
    public class YMB01
    {
        /// <summary>
        /// Gets or Sets the Unique id for book entity.
        /// </summary>
        [PrimaryKey]
        public int B01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Author Name.
        /// </summary>
        public string B01F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Name.
        /// </summary>
        public string B01F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Book's category.
        /// </summary>
        [Index]
        public string B01F04 { get; set; }

        /// <summary>
        /// Gets or Sets the available copy of books.
        /// </summary>
        public int B01F05 { get; set; }

        /// <summary>
        /// Gets or Sets the Book published year (only year, not full date).
        /// </summary>
        public int B01F06 { get; set; }
    }
}
