using System;

/// <summary>
/// The Statement class demonstrates various types of statements in C#.
/// </summary>
class Statement
{
    #region Private field
    // Private fields: Only accessible within this class
    int num1 = 10, num2 = 20;
    #endregion

    /// <summary>
    /// The PracticeStatements method demonstrates different types of statements in C#.
    /// </summary>
    public void PracticeStatements()
    {
        // 1. Declaration Statement
        int sum;

        // 2. Assignment Statement
        sum = num1 + num2;

        // 3. Expression Statement
        Console.WriteLine("The sum is: " + sum);

        // 4. Conditional Statement (if-else)
        if (num1 > num2)
        {
            Console.WriteLine("num1 is greater than num2");
        }
        else
        {
            Console.WriteLine("num2 is greater than or equal to num1");
        }

        // 5. Iterative Statement (for loop)
        Console.WriteLine("Iterating using a for loop:");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Iteration number: " + i);
        }

        // 6. Iterative Statement (while loop)
        Console.WriteLine("Iterating using a while loop:");
        int counter = 0;
        while (counter < 3)
        {
            Console.WriteLine("Counter value: " + counter);
            counter++;
        }

        // 7. Jump Statement (break and continue)
        Console.WriteLine("Using break and continue:");
        for (int i = 0; i < 5; i++)
        {
            if (i == 2)
            {
                Console.WriteLine("Skipping iteration 2 (continue statement)");
                continue;
            }
            if (i == 4)
            {
                Console.WriteLine("Stopping loop at iteration 4 (break statement)");
                break;
            }
            Console.WriteLine("Iteration: " + i);
        }

        // 8. Switch Statement
        Console.WriteLine("Using a switch statement:");
        switch (num1)
        {
            case 10:
                Console.WriteLine("num1 is 10");
                break;
            case 20:
                Console.WriteLine("num1 is 20");
                break;
            default:
                Console.WriteLine("num1 is not 10 or 20");
                break;
        }

        // 9. Return Statement
        Console.WriteLine("Result of AddNumbers(5, 7): " + AddNumbers(5, 7));
    }

    /// <summary>
    /// Method with a return statement.
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    /// <returns>The sum of the two integers.</returns>
    public int AddNumbers(int a, int b)
    {
        return a + b; // Return statement
    }

    /// <summary>
    /// The ExceptionHandlingDemo method demonstrates exception handling in C#.
    /// </summary>
    public void ExceptionHandlingDemo()
    {
        try
        {
            Console.WriteLine("Attempting to divide by zero...");
            int result = num1 / 0; // This will throw a DivideByZeroException
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Exception caught: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed.");
        }
    }
}