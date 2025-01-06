using System;

/// <summary>
/// The ExceptionHandlingExample class demonstrates basic exception handling in C#.
/// </summary>
class ExceptionHandlingExample
{
    /// <summary>
    /// The HandleExceptionWithTryCatchFinally method demonstrates basic exception handling in C#.
    /// It includes a try-catch-finally block to handle exceptions.
    /// </summary>
    public void HandleExceptionWithTryCatchFinally()
    {
        try
        {
            // Simulate some code that might throw an exception
            int number = 10;
            int result = number / 0; // Division by zero will throw an exception
            Console.WriteLine(result);
        }
        catch (DivideByZeroException ex)
        {
            // Handle specific exception: Division by zero
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle other general exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // This block is always executed, regardless of whether an exception was thrown or not
            Console.WriteLine("Execution completed.");
        }
    }

    /// <summary>
    /// The HandleExceptionWithThrow method demonstrates throwing an exception in C#.
    /// Throw allows you to create custom exceptions and handle them in the calling code.
    /// </summary>
    /// <param name="age">The age to check for eligibility.</param>
    /// <exception cref="ArithmeticException">Thrown when the age is less than 18.</exception>
    public void HandleExceptionWithThrow(int age)
    {
        if (age < 18)
        {
            // Throw an ArithmeticException if age is less than 18
            throw new ArithmeticException("You are not eligible to vote.");
        }
        else
        {
            Console.WriteLine("You are eligible to vote");
        }
    }

    /// <summary>
    /// The CustomException method demonstrates throwing and catching a custom exception in C#.
    /// </summary>
    public void CustomException()
    {
        try
        {
            int age = 15;
            if (age < 18)
            {
                // Throw a custom exception if age is less than 18
                throw new InLegalAgeException("You are not eligible to vote.");
            }
            else
            {
                Console.WriteLine("You are eligible to vote");
            }
        }
        catch (InLegalAgeException ex)
        {
            // Catch and handle the custom exception
            Console.WriteLine(ex);
        }
    }
}

/// <summary>
/// Custom exception class for handling illegal age scenarios.
/// </summary>
public class InLegalAgeException : Exception
{
    /// <summary>
    /// Initializes a new instance of the InLegalAgeException class.
    /// </summary>
    public InLegalAgeException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the InLegalAgeException class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InLegalAgeException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the InLegalAgeException class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public InLegalAgeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}


