namespace WebAPI.Models
{
    /// <summary>
    /// The Product class represents a product entity.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The Id property represents the product's unique identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Name property represents the product's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Price property represents the product's price.
        /// </summary>
        public decimal Price { get; set; }
    }

}