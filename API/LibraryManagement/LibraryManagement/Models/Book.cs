using LibraryManagement.Enums;
using System;

namespace LibraryManagement.Models
{
    /// <summary>
    /// Represents a book in the library management system.
    /// Provides properties for the book's ID, title, author, category, and creation timestamp.
    /// Includes constructors for creating instances with or without initial values 
    /// </summary>
    public class Book
    {
        #region Public Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public BookCategory Category { get; set; }
        public DateTime CreatedDate { get; set; } // New DateTime property for creation timestamp
        #endregion

        #region Constructors
        // Parameterized Constructor with validation
        public Book(int id, string title, string author, BookCategory category)
        {     

            Id = id;
            Title = title;
            Author = author;
            Category = category;
            CreatedDate = DateTime.Now; // Set CreatedDate to current date and time
        }

        // Default Constructor (optional)
        public Book()
        {
            CreatedDate = DateTime.Now; // Initialize CreatedDate to current date and time
        }
        #endregion

        #region Public Methods
        // ToString method for displaying book details
        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Author: {Author}, Category: {Category}, CreatedDate: {CreatedDate}";
        }
        #endregion
    }
}
