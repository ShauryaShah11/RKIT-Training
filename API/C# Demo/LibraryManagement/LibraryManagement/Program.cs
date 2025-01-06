using LibraryManagement.Enums;
using LibraryManagement.Models;
using LibraryManagement.Services;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instances of services
            LibraryManager libraryManager = new LibraryManager();
            FileUtility fileUtility = new FileUtility();
            DataTableUtility dataTableUtility = new DataTableUtility();

            while (true)
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Delete Book");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. List Books");
                Console.WriteLine("5. List Books by Category");
                Console.WriteLine("6. List Books Created Between Dates");
                Console.WriteLine("7. List Books Created On Specific Date");
                Console.WriteLine("8. Load Books from File");
                Console.WriteLine("9. Save Books to File");
                Console.WriteLine("10. Delete Book from File");
                Console.WriteLine("11. Delete File");
                Console.WriteLine("12. List<Book> to DataTable");
                Console.WriteLine("13. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddBook(libraryManager);
                            break;
                        case "2":
                            DeleteBook(libraryManager);
                            break;
                        case "3":
                            UpdateBook(libraryManager);
                            break;
                        case "4":
                            DisplayAllBooks(libraryManager);
                            break;
                        case "5":
                            ListBooksByCategory(libraryManager);
                            break;
                        case "6":
                            ListBooksCreatedBetweenDates(libraryManager);
                            break;
                        case "7":
                            ListBooksCreatedOnDate(libraryManager);
                            break;
                        case "8":
                            LoadBooksFromFile(fileUtility, libraryManager);
                            break;
                        case "9":
                            SaveBooksToFile(fileUtility, libraryManager);
                            break;
                        case "10":
                            DeleteBookFromFile(fileUtility);
                            break;
                        case "11":
                            fileUtility.DeleteFile();
                            Console.WriteLine("File deleted successfully.");
                            break;
                        case "12":
                            List<Book> books = libraryManager.GetBooks();
                            DataTable dt = DataTableUtility.ListToDataTable(books);
                            DataTableUtility.DisplayDataTable(dt);
                            break;
                        case "13":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        static void AddBook(LibraryManager libraryManager)
        {
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Book Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Available Categories:");
            foreach (BookCategory category in Enum.GetValues(typeof(BookCategory)))
            {
                Console.WriteLine($"{(int)category} - {category}");
            }

            Console.Write("Enter Category (number): ");
            int categoryChoice = int.Parse(Console.ReadLine());

            Book book = new Book
            {
                Id = libraryManager.GetBooks().Count + 1,
                Title = title,
                Author = author,
                Category = (BookCategory)categoryChoice,
                CreatedDate = DateTime.Now
            };

            libraryManager.AddBook(book);
        }

        static void DeleteBook(LibraryManager libraryManager)
        {
            Console.Write("Enter Book ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            libraryManager.DeleteBook(id);
        }

        static void UpdateBook(LibraryManager libraryManager)
        {
            Console.Write("Enter Book ID to update: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter New Book Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter New Book Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Available Categories:");
            foreach (BookCategory category in Enum.GetValues(typeof(BookCategory)))
            {
                Console.WriteLine($"{(int)category} - {category}");
            }

            Console.Write("Enter New Category (number): ");
            int categoryChoice = int.Parse(Console.ReadLine());

            Book updatedBook = new Book
            {
                Id = id,
                Title = title,
                Author = author,
                Category = (BookCategory)categoryChoice
            };

            libraryManager.UpdateBook(id, updatedBook);
        }

        static void DisplayAllBooks(LibraryManager libraryManager)
        {
            List<Book> books = libraryManager.GetBooks();
            libraryManager.DisplayBooks(books);
        }

        static void ListBooksByCategory(LibraryManager libraryManager)
        {
            Console.WriteLine("Available Categories:");
            foreach (BookCategory category in Enum.GetValues(typeof(BookCategory)))
            {
                Console.WriteLine($"{(int)category} - {category}");
            }

            Console.Write("Enter Category (number): ");
            int categoryChoice = int.Parse(Console.ReadLine());
            libraryManager.ListBooksByCategory((BookCategory)categoryChoice);
        }

        static void ListBooksCreatedBetweenDates(LibraryManager libraryManager)
        {
            DateTime startDate, endDate;

            // Get and parse the start date
            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            string startDateInput = Console.ReadLine();
            while (!DateTime.TryParse(startDateInput, out startDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid start date (yyyy-MM-dd).");
                Console.Write("Enter Start Date (yyyy-MM-dd): ");
                startDateInput = Console.ReadLine();
            }

            // Get and parse the end date
            Console.Write("Enter End Date (yyyy-MM-dd): ");
            string endDateInput = Console.ReadLine();
            while (!DateTime.TryParse(endDateInput, out endDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid end date (yyyy-MM-dd).");
                Console.Write("Enter End Date (yyyy-MM-dd): ");
                endDateInput = Console.ReadLine();
            }

            // Now that we have valid dates, call the method to list the books
            List<Book> createBetweenDates = libraryManager.ListBooksCreatedBetweenDates(startDate, endDate);
            libraryManager.DisplayBooks(createBetweenDates);
        }


        static void ListBooksCreatedOnDate(LibraryManager libraryManager)
        {
            DateTime specificDate;

            // Get and parse the specific date
            Console.Write("Enter Specific Date (yyyy-MM-dd): ");
            string specificDateInput = Console.ReadLine();

            // Use TryParse to ensure the date is valid
            while (!DateTime.TryParse(specificDateInput, out specificDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-MM-dd).");
                Console.Write("Enter Specific Date (yyyy-MM-dd): ");
                specificDateInput = Console.ReadLine();
            }

            // Now that we have a valid date, call the method to list the books
            List<Book> createdOnDate = libraryManager.ListBooksCreatedOnDate(specificDate);
            libraryManager.DisplayBooks(createdOnDate);
        }


        static void LoadBooksFromFile(FileUtility fileUtility, LibraryManager libraryManager)
        {
            List<Book> booksFromFile = fileUtility.LoadBooksFromFile();
            List<Book> books = libraryManager.GetBooks();
            foreach (Book book in booksFromFile)
            {
                // Check if the book ID is already present in the existing list of books in libraryManager
                if (!books.Any(existingBook => existingBook.Id == book.Id))
                {
                    libraryManager.AddBook(book);  // Add book only if ID doesn't exist
                }
            }

            Console.WriteLine("Books loaded successfully.");
        }

        static void SaveBooksToFile(FileUtility fileUtility, LibraryManager libraryManager)
        {
            fileUtility.SaveBooksToFile(libraryManager.GetBooks());
            Console.WriteLine("Books saved successfully.");
        }

        static void DeleteBookFromFile(FileUtility fileUtility)
        {
            Console.Write("Enter Book ID to delete from file: ");
            int id = int.Parse(Console.ReadLine());
            fileUtility.DeleteBookInFile(id);
        }
    }
}
