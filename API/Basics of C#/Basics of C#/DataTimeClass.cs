using System;

/// <summary>
/// The DataTimeClass class demonstrates basic DateTime operations in C#.
/// </summary>
class DataTimeClass
{
    /// <summary>
    /// The DatePractice method demonstrates basic DateTime operations in C#.
    /// </summary>
    public void DatePractice()
    {
        // Create a DateTime object for a specific date
        DateTime date1 = new DateTime(2024, 12, 3);  // Year, Month, Day
        Console.WriteLine(date1);  // Output: 12/3/2024 12:00:00 AM

        // Get the current date and time
        DateTime currentDateTime = DateTime.Now;
        Console.WriteLine("Current Date and Time is: " + currentDateTime);  // Output: current date and time

        // Get the current UTC date and time
        DateTime utcDateTime = DateTime.UtcNow;
        Console.WriteLine("Current UTC Date and Time is: " + utcDateTime);  // Output: current UTC date and time

        // Get today's date (time part is set to 00:00:00)
        DateTime today = DateTime.Today;
        Console.WriteLine("Current Date is: " + today);  // Output: today's date with time set to 00:00:00

        // Parse a date string into a DateTime object
        string dateStr = "2024-12-03";
        DateTime parsedDate = DateTime.Parse(dateStr);
        Console.WriteLine(parsedDate);  // Output: 12/3/2024 12:00:00 AM

        // Try to parse an invalid date string
        string invalidDateStr = "2024-13-03";
        DateTime result;
        bool success = DateTime.TryParse(invalidDateStr, out result);
        if (success)
        {
            Console.WriteLine(result);   // Output: parsed date if successful
        }
        else
        {
            Console.WriteLine("Date is in invalid format: " + invalidDateStr); // Output: error message
        }

        // Create a DateTime object with specific date and time
        DateTime date = new DateTime(2024, 12, 3, 14, 30, 0);
        Console.WriteLine("Year: " + date.Year);        // Output: Year: 2024
        Console.WriteLine("Month: " + date.Month);      // Output: Month: 12
        Console.WriteLine("Day: " + date.Day);          // Output: Day: 3
        Console.WriteLine("Hour: " + date.Hour);        // Output: Hour: 14
        Console.WriteLine("Minute: " + date.Minute);    // Output: Minute: 30
        Console.WriteLine("Second: " + date.Second);    // Output: Second: 0
        Console.WriteLine("Millisecond: " + date.Millisecond); // Output: Millisecond: 0
    }
}

