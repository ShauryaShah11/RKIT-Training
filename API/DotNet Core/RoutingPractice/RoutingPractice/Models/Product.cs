using System.Threading.Tasks.Dataflow;

namespace RoutingPractice.Models
{
    /// <summary>
    /// Represents a product with an ID and a name.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }
    }
}