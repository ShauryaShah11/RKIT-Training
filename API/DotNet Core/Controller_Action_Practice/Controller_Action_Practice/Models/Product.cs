namespace Controller_Action_Practice.Models
{
    /// <summary>
    /// Represents a product entity with basic properties.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }
    }
}
