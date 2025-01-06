using System;

/// <summary>
/// The MethodDemo class demonstrates basic method operations in C#.
/// It includes methods with and without parameters, methods that return values, static methods, and methods with default parameters.
/// </summary>
class MethodDemo
{
    /// <summary>
    /// Prints a greeting message to the console.
    /// </summary>
    public void PrintGreeting()
    {
        Console.WriteLine("Hello, welcome to C#!");
    }

    /// <summary>
    /// Prints the sum of two integers to the console.
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    public void PrintSum(int a, int b)
    {
        int sum = a + b;
        Console.WriteLine("The sum of " + a + " and " + b + " is " + sum);
    }

    /// <summary>
    /// Multiplies two integers and returns the result.
    /// </summary>
    /// <param name="x">The first integer.</param>
    /// <param name="y">The second integer.</param>
    /// <returns>The product of the two integers.</returns>
    public int MultiplyNumbers(int x, int y)
    {
        return x * y;
    }

    /// <summary>
    /// Prints the sum of two integers to the console, with the second integer having a default value.
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer, with a default value of 10.</param>
    public void DefaultParameter(int a, int b = 10)
    {
        Console.WriteLine("The sum of " + a + " and " + b + " is " + (a + b));
    }

    /// <summary>
    /// Prints the current time to the console.
    /// </summary>
    public static void PrintTime()
    {
        Console.WriteLine("Current Time: " + DateTime.Now.ToLongTimeString());
    }
}