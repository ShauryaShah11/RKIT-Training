using System;

namespace Advance_Of_C_.Base_Class_Library
{
    /// <summary>
    /// This class demonstrates the usage of the DateTime structure in C#.
    /// It showcases how to get the current date and time, add days to the current date, format dates, 
    /// and retrieve the day of the week using DateTime methods.
    /// </summary>
    class DateTimeExample
    {
        /// <summary>
        /// Demonstrates various functionalities of the DateTime class, including:
        /// - Getting the current date and time.
        /// - Adding days to the current date.
        /// - Formatting dates into a custom string format.
        /// - Getting the day of the week for the current date.
        /// </summary>
        public void DemonstrateDateTime()
        {
            DateTime currentDate = DateTime.Now;
            Console.WriteLine("Current Date and Time: " + currentDate);

            // Adding Days
            DateTime futureDate = currentDate.AddDays(7);
            Console.WriteLine("Date after 7 days: " + futureDate);

            // Formatting Dates
            string formattedDate = currentDate.ToString("dd/MM/yyyy");
            Console.WriteLine("Formatted Date: " + formattedDate);

            // Day of Week
            Console.WriteLine("Day of the Week: " + currentDate.DayOfWeek);
        }
    }
}
