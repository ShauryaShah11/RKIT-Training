using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    /// <summary>
    /// The IManageable interface defines the operations for managing books in the library system.
    /// It includes methods for performing CRUD (Create, Read, Update, Delete) operations on books.
    /// </summary>
    public interface IManageable
    {
        /// <summary>
        /// Adds a new book to the library.
        /// </summary>
        /// <param name="book">The book to be added.</param>
        void AddBook(Book book);

        /// <summary>
        /// Deletes a book from the library by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book to be deleted.</param>
        void DeleteBook(int id);

        /// <summary>
        /// Updates the details of an existing book in the library.
        /// </summary>
        /// <param name="id">The unique identifier of the book to be updated.</param>
        /// <param name="book">The updated book information.</param>
        void UpdateBook(int id, Book book);

        /// <summary>
        /// Finds a book in the library by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book to be found.</param>
        /// <returns>The book with the specified identifier.</returns>
        Book FindBook(int id);
    }

}
