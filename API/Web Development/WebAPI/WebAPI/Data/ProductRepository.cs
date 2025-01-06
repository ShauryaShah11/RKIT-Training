using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Data
{
    /// <summary>
    /// The ProductRepository class represents a repository for managing products.
    /// This class provides methods to perform CRUD operations on the product data.
    /// </summary>
    public class ProductRepository
    {
        /// <summary>
        /// The products field represents a static list of products, simulating a database.
        /// </summary>
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99M },
            new Product { Id = 2, Name = "Phone", Price = 499.99M },
            new Product { Id = 3, Name = "Tablet", Price = 299.99M },
            new Product { Id = 4, Name = "Smartwatch", Price = 199.99M },
            new Product { Id = 5, Name = "Headphones", Price = 99.99M },
            new Product { Id = 6, Name = "Keyboard", Price = 49.99M },
            new Product { Id = 7, Name = "Mouse", Price = 29.99M },
            new Product { Id = 8, Name = "Monitor", Price = 199.99M },
            new Product { Id = 9, Name = "External Hard Drive", Price = 79.99M },
            new Product { Id = 10, Name = "Printer", Price = 149.99M }
        };

        /// <summary>
        /// Retrieves a list of all products in the repository.
        /// </summary>
        /// <returns>A list containing all products.</returns>
        public List<Product> GetAll()
        {
            return products;
        }

        /// <summary>
        /// Finds a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to retrieve.</param>
        /// <returns>The product if found; otherwise, null.</returns>
        public Product GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Adds a new product to the repository.
        /// The product's ID is automatically assigned based on the current maximum ID.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public void Add(Product product)
        {
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
        }

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="product">The product with updated data. The ID must match an existing product.</param>
        public void Update(Product product)
        {
            Product existing = GetById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
            }
        }

        /// <summary>
        /// Deletes a product from the repository based on its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        public void Delete(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }
    }
}
