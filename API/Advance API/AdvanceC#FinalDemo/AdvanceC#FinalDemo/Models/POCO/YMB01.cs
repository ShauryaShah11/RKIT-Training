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
        [AutoIncrement]
        public int B01F01 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Author Name.
        /// </summary>
        [StringLength(50)]
        public string B02F02 { get; set; }

        /// <summary>
        /// Gets or Sets the Book Name.
        /// </summary>
        [StringLength(50)]
        [Required]
        public string B03F03 { get; set; }

        /// <summary>
        /// Gets or Sets the Book's category.
        /// </summary>
        [StringLength(50)]
        [Index]
        public string B04F04 { get; set; }

        /// <summary>
        /// Gets or Sets the available copy of books.
        /// </summary>
        [Required]
        public int B05F05 { get; set; }

        /// <summary>
        /// Gets or Sets the Book published year (only year, not full date).
        /// </summary>
        [Required]
        public int B06F06 { get; set; }
    }
}
