using LibraryManagement.Enums;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using LibraryManagement.Logging;  // Import the logging library
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Services
{
    /// <summary>
    /// The LibraryManager class is responsible for managing the library.
    /// It implements the IManageable interface.
    /// It includes methods to add, delete, update, find, and list books.
    /// </summary>
    public class LibraryManager : IManageable
    {
        #region Private Fields
        /// <summary>
        /// A private list to store books in the library.
        /// </summary>
        private readonly List<Book> _books;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the LibraryManager class.
        /// </summary>
        public LibraryManager()
        {
            _books = new List<Book>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a book to the library.
        /// </summary>
        /// <param name="book">The book to add.</param>
        public void AddBook(Book book)
        {
            _books.Add(book);
            string logMessage = $"Book added: {book}";
            Logger.Log(logMessage);  // Log the action
            Console.WriteLine(logMessage);  // Display in console
        }

        public List<Book> GetBooks()
        {
            return _books;
        }

        /// <summary>
        /// Deletes a book from the library by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        public void DeleteBook(int id)
        {
            Book book = _books.Find(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
                string logMessage = $"Book with ID {id} deleted.";
                Logger.Log(logMessage);  // Log the action
                Console.WriteLine(logMessage);  // Display in console
            }
            else
            {
                string logMessage = $"Book with ID {id} not found.";
                Logger.Log(logMessage);  // Log the action
                Console.WriteLine(logMessage);  // Display in console
            }
        }

        /// <summary>
        /// Updates the details of a specific book in the library.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="updatedBook">The updated book details.</param>
        public void UpdateBook(int id, Book updatedBook)
        {
            Book book = _books.Find(b => b.Id == id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.Category = updatedBook.Category;
                string logMessage = $"Book with ID {id} updated: {book}";
                Logger.Log(logMessage);  // Log the action
                Console.WriteLine(logMessage);  // Display in console
            }
            else
            {
                string logMessage = $"Book with ID {id} not found.";
                Logger.Log(logMessage);  // Log the action
                Console.WriteLine(logMessage);  // Display in console
            }
        }

        /// <summary>
        /// Finds a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to find.</param>
        /// <returns>The book if found; otherwise, null.</returns>
        public Book FindBook(int id)
        {
            Book book = _books.Find(b => b.Id == id);
            string logMessage;
            if (book != null)
            {
                logMessage = $"Book found: {book}";
                Logger.Log(logMessage);  // Log the action
                Console.WriteLine(logMessage);  // Display in console
                return book;
            }
            else
            {
                logMessage = $"Book with ID {id} not found.";
                Logger.Log(logMessage);  // Log the action
                Console.WriteLine(logMessage);  // Display in console
                return null;
            }
        }

        /// <summary>
        /// Lists all the books in the library.
        /// </summary>
        public void ListBooks()
        {
            if (_books.Count == 0)
            {
                string logMessage = "No books available.";
                Logger.Log(logMessage);  // Log the action
                Console.WriteLine(logMessage);  // Display in console
                return;
            }

            foreach (Book book in _books)
            {
                Console.WriteLine(book);
            }
        }

        /// <summary>
        /// Lists all the books in the library by category.
        /// </summary>
        /// <param name="category">The Category of the Books to display</param>
        public void ListBooksByCategory(BookCategory category)
        {
            List<Book> booksInCategory = _books.Where(b => b.Category == category).ToList();

            string logMessage;
            if (booksInCategory.Any())
            {                
                logMessage = $"Listed books in category {category}.";
            }
            else
            {
                logMessage = $"No books found in category {category}.";
            }
            Logger.Log(logMessage);  // Log the action
            Console.WriteLine(logMessage);  // Display in console
            DisplayBooks(booksInCategory);
        }

        /// <summary>
        /// Lists all the books created between two dates.
        /// </summary>
        /// <param name="startDate">The start date to filter books.</param>
        /// <param name="endDate">The end date to filter books.</param>
        public List<Book> ListBooksCreatedBetweenDates(DateTime startDate, DateTime endDate)
        {
            List<Book> booksInRange = _books.Where(b => b.CreatedDate >= startDate && b.CreatedDate <= endDate).ToList();

            string logMessage;
            if (booksInRange.Any())
            {                
                logMessage = $"Listed books created between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}.";
            }
            else
            {
                logMessage = $"No books found between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}.";
            }
            Logger.Log(logMessage);  // Log the action
            Console.WriteLine(logMessage);  // Display in console
            return booksInRange;
        }

        /// <summary>
        /// Lists all the books created on a specific date.
        /// </summary>
        /// <param name="specificDate">The date to filter books by.</param>
        public List<Book> ListBooksCreatedOnDate(DateTime specificDate)
        {
            List<Book> booksOnSpecificDate = _books.Where(b => b.CreatedDate.Date == specificDate.Date).ToList();

            string logMessage;
            if (booksOnSpecificDate.Any())
            {                
                logMessage = $"Listed books created on {specificDate.ToShortDateString()}.";
            }
            else
            {
                logMessage = $"No books found on {specificDate.ToShortDateString()}.";
            }
            Logger.Log(logMessage);  // Log the action
            Console.WriteLine(logMessage);  // Display in console
            return booksOnSpecificDate;
        }

        /// <summary>
        /// The DisplayBooks method displays the list of books in the library, including their creation date.
        /// </summary>
        /// <param name="books">The list of books to be displayed.</param>
        public void DisplayBooks(List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                Console.WriteLine("No books available to display.");
                return;
            }

            Console.WriteLine("\nList of Books:");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"ID",-5} {"Title",-20} {"Author",-20} {"Category",-15} {"Created Date",-15}");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Id,-5} {book.Title,-20} {book.Author,-20} {book.Category,-15} {book.CreatedDate.ToShortDateString(),-15}");
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }
        #endregion
    }
}
