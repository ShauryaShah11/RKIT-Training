using System;

namespace Advance_Of_C_.Base_Class_Library
{
    class DateTimeExample
    {
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
