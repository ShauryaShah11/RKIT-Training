using System;

/// <summary>
/// The ArrayPractice class demonstrates basic array operations in C#.
/// It includes methods for reading user input into an array and displaying array lengths.
/// </summary>
class ArrayPractice
{
    /// <summary>
    /// Prompts the user to enter numbers to populate an array,
    /// and then displays the length of the array and a predefined array of car brands.
    /// </summary>
    /// <remarks>
    /// This method is called from the GUI to practice single-dimensional array operations.
    /// </remarks>
    /// <exception cref="FormatException">Thrown when the input is not a valid integer.</exception>
    public void PracticeSingleDimensionArray()
    {
        int[] arr = new int[5];
        Console.WriteLine("Enter a number: ");
        for (int i = 0; i < arr.Length; i++)
        {
            try
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid number.");
                i--; // Decrement i to retry the current index
            }
        }

        Console.WriteLine("Array length is : {0}", arr.Length);

        // Create an array of four elements without specifying the size 
        string[] cars = new string[] { "Volvo", "BMW", "Ford", "Mazda" };

        Console.WriteLine("Cars array length is : {0}", cars.Length);
    }

    /// <summary>
    /// Demonstrates the creation and basic usage of a multi-dimensional array.
    /// </summary>
    /// <remarks>
    /// This method is called to practice multi-dimensional array operations.
    /// </remarks>
    public void PracticeMultiDimensionArray()
    {
        int[,] arr = new int[5, 5]; // Correctly declaring a 2D array with 5 rows and 5 columns

        // Populate the array
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                arr[i, j] = i * j; // Example of populating the array
            }
        }

        // Print the array
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write(arr[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Demonstrates the creation and basic usage of a jagged array.
    /// </summary>
    /// <remarks>
    /// This method is called to practice jagged array operations.
    /// </remarks>
    public void PracticeJaggedArray()
    {
        // Declare a jagged array
        int[][] jaggedArray = new int[3][];
        // Initialize the jagged array
        jaggedArray[0] = new int[] { 1, 2, 3, 4 };
        jaggedArray[1] = new int[] { 5, 6, 7 };
        jaggedArray[2] = new int[] { 8, 9 };
        // Print the jagged array
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write(jaggedArray[i][j] + " ");
            }
            Console.WriteLine();
        }
    }
}