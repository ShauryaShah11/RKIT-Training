using System;

/// <summary>
/// The StringClass demonstrates the usage of the string class in C#.
/// </summary>
class StringClass
{
    /// <summary>
    /// The StringPractice method demonstrates the usage of various string methods in C#.
    /// It includes demonstrations of methods such as IndexOf, Substring, Replace, ToLower, ToUpper, Trim, Contains, StartsWith, EndsWith, Format, Concat, Join, and Interpolation.
    /// </summary>
    public void StringPractice()
    {
        // Using string literal
        string greeting = "Hello, World!";
        // Creating a string using the constructor
        string name = new string('A', 5);
        Console.WriteLine(greeting);  // Output: Hello, World!
        Console.WriteLine(name);      // Output: AAAAA

        // Getting the length of the string
        Console.WriteLine(greeting.Length);  // Output: 13

        // Finding the first occurrence of 'o'
        int index = greeting.IndexOf('o');
        Console.WriteLine(index);  // Output: 4

        // Getting a substring starting at index 7 with length 5
        string subStr = greeting.Substring(7, 5);
        Console.WriteLine(subStr);  // Output: World

        // Replacing "World" with "C#"
        string replaced = greeting.Replace("World", "C#");
        Console.WriteLine(replaced);  // Output: Hello, C#!

        // Converting to lowercase
        Console.WriteLine(greeting.ToLower());  // Output: hello, world!
        // Converting to uppercase
        Console.WriteLine(greeting.ToUpper());  // Output: HELLO, WORLD!

        // Trimming whitespace from both ends
        string str = "  Hello, World!  ";
        Console.WriteLine(str.Trim());  // Output: Hello, World!

        // Checking if the string contains "World"
        Console.WriteLine(str.Contains("World"));  // Output: True
        // Checking if the string starts with "Hello"
        Console.WriteLine(str.StartsWith("Hello"));  // Output: False (because of leading spaces)
        // Checking if the string ends with "World!"
        Console.WriteLine(str.EndsWith("World!"));  // Output: False (because of trailing spaces)

        // Using string.Format to format a string
        int age = 25;
        name = "shaurya";
        string message = string.Format("Name: {0}, Age: {1}", name, age);
        Console.WriteLine(message);  // Output: Name: shaurya, Age: 25


        // Concatenating strings
        string firstName = "John";
        string lastName = "Doe";
        string fullName = string.Concat(firstName, " ", lastName);
        Console.WriteLine(fullName);  // Output: John Doe

        // Joining an array of strings with a separator
        string[] words = { "apple", "banana", "cherry" };
        string result = string.Join(", ", words);
        Console.WriteLine(result);  // Output: apple, banana, cherry

        // converting string array based on delimiter
        string city = "Ahmedabad,Surat,Vadodara,Mumbai,Nadiad";
        string[] cities = city.Split(',');
        for(int i=0; i<cities.Length; i++)
        {
            Console.WriteLine(cities[i]);
        }

        // Using string interpolation
        message = $"Name: {name}, Age: {age}";
        Console.WriteLine(message);  // Output: Name: shaurya, Age: 25


    }
}