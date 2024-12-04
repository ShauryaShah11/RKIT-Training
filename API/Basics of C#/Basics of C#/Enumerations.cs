using System;

/// <summary>
/// The Enumerations class demonstrates basic enumeration operations in C#.
/// </summary>
class Enumerations
{
    /// <summary>
    /// Represents different levels.
    /// </summary>
    enum Level
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// Represents the months of the year.
    /// </summary>
    enum Months
    {
        January,    // 0
        February,   // 1
        March,      // 2
        April,      // 3
        May,        // 4
        June,       // 5
        July        // 6
    }

    /// <summary>
    /// Represents the days of the week.
    /// </summary>
    enum Days
    {
        Sunday = 'a',
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    /// <summary>
    /// Demonstrates the usage of enumerations.
    /// </summary>
    public void EnumPractice()
    {
        // Using the Level enumeration
        Level myVar = Level.Medium;
        Console.WriteLine("Level: " + myVar); // Output: Medium

        // Using the Months enumeration
        int myNum = (int)Months.April;
        Console.WriteLine("Month (April) Index: " + myNum); // Output: 3

        // Using the Days enumeration
        int dayNum = (int)Days.Friday;
        Console.WriteLine("Day (Friday) Index: " + dayNum); // Output: 6

        // Displaying all values of the Level enumeration
        Console.WriteLine("All Levels:");
        foreach (Level level in Enum.GetValues(typeof(Level)))
        {
            Console.WriteLine(level);
        }

        // Displaying all values of the Months enumeration
        Console.WriteLine("All Months:");
        foreach (Months month in Enum.GetValues(typeof(Months)))
        {
            Console.WriteLine(month);
        }

        // Displaying all values of the Days enumeration
        Console.WriteLine("All Days:");
        foreach (Days day in Enum.GetValues(typeof(Days)))
        {
            Console.WriteLine(day);
        }
    }
}

