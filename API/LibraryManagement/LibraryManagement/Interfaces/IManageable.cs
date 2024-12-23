using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    /// <summary>
    /// The IManageable interface is used for managing books in the library.
    /// The IManageable interface includes methods to add, delete, update, find, and list books.
    /// </summary>
    public interface IManageable
    {
        void AddBook(Book book);
        void DeleteBook(int id);
        void UpdateBook(int id, Book book);
        Book FindBook(int id);
    }
}
