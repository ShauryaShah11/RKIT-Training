using LibraryManagement.Enums;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryManagement.Services
{
    /// <summary>
    /// The FileUtility class provides methods for reading and writing book details to a file.
    /// It includes methods for loading books from a file, saving book details to a file,
    /// </summary>
    public class FileUtility
    {
        private const string FilePath = "books.txt"; // File path for storing book details

        /// <summary>
        /// Loads books from a file and returns them as a list of Book objects.
        /// </summary>
        /// <returns>A list of books read from the file.</returns>
        public List<Book> LoadBooksFromFile()
        {
            List<Book> books = new List<Book>();

            try
            {
                // Check if the file exists
                if (File.Exists(FilePath))
                {
                    string fileContent = File.ReadAllText(FilePath); // Read all content
                    string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None); // Split lines

                    foreach (string line in lines)
                    {
                        string[] bookDetails = line.Split(',');
                        
                            try
                            {
                                int id = int.Parse(bookDetails[0].Trim());
                                string title = bookDetails[1].Trim();
                                string author = bookDetails[2].Trim();
                                if (Enum.TryParse(bookDetails[3].Trim(), out BookCategory category))
                                {
                                    books.Add(new Book(id, title, author, category));
                                }
                                else
                                {
                                    Console.WriteLine($"Invalid category '{bookDetails[3].Trim()}' for book: {title}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error parsing book details: {ex.Message}");
                            }
                        
                    }
                    Console.WriteLine("Books loaded from file.");
                }
                else
                {
                    Console.WriteLine("No books found in the file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
            }

            return books;
        }        

        public void SaveBooksToFile(List<Book> books)
        {
            try
            {
                // Write all books to the file
                File.WriteAllText(FilePath, string.Join(Environment.NewLine, books.Select(b => $"{b.Id}, {b.Title}, {b.Author}, {b.Category}")));
                Console.WriteLine("Books saved to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving books: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific book from the file.
        /// </summary>
        /// <param name="book">The book to delete.</param>
        public void DeleteBookInFile(int id)
        {
            try
            {
                // Load all books from the file
                List<Book> books = LoadBooksFromFile();

                // Remove the specified book
                var bookToDelete = books.FirstOrDefault(b => b.Id == id);
                if (bookToDelete != null)
                {
                    books.Remove(bookToDelete);
                    Console.WriteLine("Book deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }

                // Write the updated list of books back to the file
                File.WriteAllText(FilePath, string.Join(Environment.NewLine, books.Select(b => $"{b.Id}, {b.Title}, {b.Author}, {b.Category}")));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting book: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes the file containing book details.
        /// </summary>
        public void DeleteFile()
        {
            try
            {
                // Check if the file exists
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    Console.WriteLine("File deleted successfully.");
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }
    }
}
