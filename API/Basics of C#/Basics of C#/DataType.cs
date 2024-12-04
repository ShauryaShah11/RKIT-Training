using System;
using System.Globalization;

/// <summary>
/// The DataType class demonstrates different data types and type conversions in C#.
/// </summary>
class DataType
{
    // Fields with different data types
    int age = 20;                      // Integer type
    long myNum = 10000000000L;         // Long integer type with 'L' suffix
    double number = 10.0D;             // Double type with 'D' suffix
    decimal price = 99.99M;            // Decimal type with 'M' suffix
    char letter = 'A';                 // Single character
    bool myBool = true;                // Boolean type
    string myText = "Hello";           // String type
    float myFloat = 5.75F;             // Float type with 'F' suffix

    /// <summary>
    /// Displays the values of different data types.
    /// </summary>
    public void DisplayMessage()
    {
        Console.WriteLine("Age: " + age);
        Console.WriteLine("Number (Long): " + myNum);
        Console.WriteLine("Number (Double): " + number);
        Console.WriteLine("Price (Decimal): " + price);
        Console.WriteLine("Letter: " + letter);
        Console.WriteLine("Boolean: " + myBool);
        Console.WriteLine("Text: " + myText);
        Console.WriteLine("Float: " + myFloat);
    }

    /// <summary>
    /// Demonstrates type conversions in C#.
    /// </summary>
    public void ConvertToConversion()
    {
        // Implicit conversion (smaller to larger type)
        double doublePrice = (double)price;
        Console.WriteLine("Decimal to Double: " + doublePrice);

        // Explicit conversion (larger to smaller type)
        int truncatedNumber = (int)number;
        Console.WriteLine("Double to Int (Truncated): " + truncatedNumber);

        // Conversion using methods
        string numText = number.ToString();
        Console.WriteLine("Double to String: " + numText);

        // Parsing a string to a numeric type
        decimal parsedPrice = decimal.Parse("199.99");
        Console.WriteLine("Parsed Decimal: " + parsedPrice);
    }

    /// <summary>
    /// Demonstrates numeric formatting in C#.
    /// </summary>
    public void NumericFormating()
    {
        double value = 1000D / 12.34D;
        Console.WriteLine("Original Value: " + value);
        Console.WriteLine("Formatted (0): " + string.Format("{0:0}", value)); // 10.0 -> 10
        Console.WriteLine("Formatted (0.#): " + string.Format("{0:0.#}", value)); // 10.0 -> 10, 10.1 -> 10.1
        Console.WriteLine("Formatted (0.0): " + string.Format("{0:0.0}", value)); // 10.0 -> 10.0
        Console.WriteLine("Formatted (0.00): " + string.Format("{0:0.00}", value)); // 10.121 -> 10.12

        double money = 10D / 3D;
        Console.WriteLine("Money ($0.00): " + string.Format("${0:0.00}", money));
        Console.WriteLine("Money (Interpolated): " + string.Format("${0:0.00}", money));
        Console.WriteLine("Money (Currency): " + money.ToString("C"));
        Console.WriteLine("Money (Currency with Culture): " + money.ToString("C", CultureInfo.CurrentCulture));

        Console.ReadLine();
    }
}