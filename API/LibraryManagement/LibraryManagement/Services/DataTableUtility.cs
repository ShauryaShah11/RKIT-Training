using LibraryManagement.Enums;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace LibraryManagement.Services
{
    /// <summary>
    /// The DataTableUtility class provides methods for converting DataTable to List<Book> and vice versa.
    /// </summary>
    class DataTableUtility
    {
        /// <summary>
        /// Method to convert DataTable to List<Book> using constructor
        /// </summary>
        /// <param name="dt">The DataTable containing book data to be converted.</param>
        /// <returns>A list of Book objects populated with data from the DataTable.</returns>
        public static List<Book> DataTableToList(DataTable dt)
        {
            List<Book> booksList = new List<Book>();

            foreach (DataRow row in dt.Rows)
            {
                Book book = new Book
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Title = row["Title"].ToString(),
                    Author = row["Author"].ToString(),
                    Category = (BookCategory)Enum.Parse(typeof(BookCategory), row["Category"].ToString()), // Handling enum conversion
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                };
                booksList.Add(book);
            }

            return booksList;
        }

        /// <summary>
        /// Method to convert List<Book> to DataTable.
        /// </summary>
        /// <param name="books">The List of book containing book data to be converte.</param>
        /// <returns>A DataTable of Book Populated with data from the list of book.</returns>
        public static DataTable ListToDataTable(List<Book> books)
        {
            // Define DataTable and columns
            DataTable dt = new DataTable("book");

            // Add columns
            dt.Columns.Add(new DataColumn("Id", typeof(int)));
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Author", typeof(string)));
            dt.Columns.Add(new DataColumn("Category", typeof(string))); // Genre as string for Enum
            dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));

            // Add rows to DataTable
            foreach (Book book in books)
            {
                dt.Rows.Add(book.Id, book.Title, book.Author, book.Category.ToString(), book.CreatedDate);
            }
            return dt;
        }

        /// <summary>
        /// Method to display DataTable information in the console.
        /// </summary>
        /// <param name="dt">The DataTable object.</param>
        public static void DisplayDataTable(DataTable dt)
        {
            Console.WriteLine("Displaying DataTable Information:");

            // Print column names
            foreach (DataColumn column in dt.Columns)
            {
                Console.Write(column.ColumnName + "\t");
            }
            Console.WriteLine(); // New line after column headers

            // Print each row in the DataTable
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item + "\t");
                }
                Console.WriteLine(); // New line after each row
            }
        }
    }
}
