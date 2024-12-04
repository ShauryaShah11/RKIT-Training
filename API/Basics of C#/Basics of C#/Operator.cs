using System;

/// <summary>
/// The Operator class demonstrates different types of operators in C#.
/// It includes Arithmetic Operators, Unary Arithmetic Operators, Relational Operators, Logical Operators, and Bitwise Operators.
/// </summary>
class Operator
{
    /// <summary>
    /// The ArithmeticOperator method demonstrates basic arithmetic operations in C#.
    /// </summary>
    /// <param name="num1">The first integer.</param>
    /// <param name="num2">The second integer.</param>
    public void ArithmeticOperator(int num1, int num2)
    {
        int result;

        // Addition Operator
        result = num1 + num2;
        Console.WriteLine("Addition Operator: " + result);

        // Subtraction Operator
        result = num1 - num2;
        Console.WriteLine("Subtraction Operator: " + result);

        // Multiplication Operator
        result = num1 * num2;
        Console.WriteLine("Multiplication Operator: " + result);

        // Division Operator
        // Check to avoid division by zero
        if (num2 != 0)
        {
            result = num1 / num2;
            Console.WriteLine("Division Operator: " + result);
        }
        else
        {
            Console.WriteLine("Division by zero is not allowed.");
        }
    }

    /// <summary>
    /// The UnaryArithmeticOperator method demonstrates unary arithmetic operations in C#.
    /// </summary>
    /// <param name="num1">The first integer.</param>
    /// <param name="num2">The second integer.</param>
    public void UnaryArithmeticOperator(int num1, int num2)
    {
        int result;

        // Post-increment Operator: value is assigned and then it is incremented
        result = num1++;
        Console.WriteLine("Post-increment: num1 is {0}, result is {1}", num1, result);

        // Post-decrement Operator: value is assigned and then it is decremented
        result = num2--;
        Console.WriteLine("Post-decrement: num2 is {0}, result is {1}", num2, result);

        // Pre-increment Operator: value is incremented and then it is assigned
        result = ++num1;
        Console.WriteLine("Pre-increment: num1 is {0}, result is {1}", num1, result);

        // Pre-decrement Operator: value is decremented and then it is assigned
        result = --num2;
        Console.WriteLine("Pre-decrement: num2 is {0}, result is {1}", num2, result);
    }

    /// <summary>
    /// The RelationalOperator method demonstrates relational operations in C#.
    /// </summary>
    /// <param name="num1">The first integer.</param>
    /// <param name="num2">The second integer.</param>
    public void RelationalOperator(int num1, int num2)
    {
        bool result;

        // Equal to Operator (==)
        result = num1 == num2;
        Console.WriteLine("Equal to (==): " + result);

        // Not equal to Operator (!=)
        result = num1 != num2;
        Console.WriteLine("Not equal to (!=): " + result);

        // Greater than Operator (>)
        result = num1 > num2;
        Console.WriteLine("Greater than (>): " + result);

        // Less than Operator (<)
        result = num1 < num2;
        Console.WriteLine("Less than (<): " + result);

        // Greater than or equal to Operator (>=)
        result = num1 >= num2;
        Console.WriteLine("Greater than or equal to (>=): " + result);

        // Less than or equal to Operator (<=)
        result = num1 <= num2;
        Console.WriteLine("Less than or equal to (<=): " + result);
    }

    /// <summary>
    /// The LogicalOperator method demonstrates logical operations in C#.
    /// </summary>
    /// <param name="bool1">The first boolean.</param>
    /// <param name="bool2">The second boolean.</param>
    public void LogicalOperator(bool bool1, bool bool2)
    {
        bool result;
        bool bool3 = false;

        // Logical AND Operator (&&)
        result = bool1 && bool2;
        Console.WriteLine("Logical AND (&&): " + result);

        result = (bool1 && bool2) & bool3;
        Console.WriteLine("Logical AND with bitwise AND (&): " + result);

        result = (bool1 & bool2) & bool3;
        Console.WriteLine("Bitwise AND (&): " + result);

        // Logical OR Operator (||)
        result = bool1 || bool2;
        Console.WriteLine("Logical OR (||): " + result);

        // Logical NOT Operator (!)
        result = !bool1;
        Console.WriteLine("Logical NOT (!): " + result);
    }

    /// <summary>
    /// The BitWiseOperator method demonstrates bitwise operations in C#.
    /// </summary>
    public void BitWiseOperator()
    {
        int num1 = 5;  // Binary: 0101
        int num2 = 3;  // Binary: 0011

        // AND Operator (&)
        int andResult = num1 & num2; // Binary: 0001 (1 in decimal)
        Console.WriteLine("AND Operator (&): " + andResult); // Output: 1

        // OR Operator (|)
        int orResult = num1 | num2; // Binary: 0111 (7 in decimal)
        Console.WriteLine("OR Operator (|): " + orResult);  // Output: 7

        // XOR Operator (^)
        int xorResult = num1 ^ num2; // Binary: 0110 (6 in decimal)
        Console.WriteLine("XOR Operator (^): " + xorResult); // Output: 6

        // NOT Operator (~)
        int notResult = ~num1; // Binary: 1010 (in 32-bit: -6 in decimal due to two's complement)
        Console.WriteLine("NOT Operator (~): " + notResult); // Output: -6

        // Left Shift Operator (<<)
        int leftShiftResult = num1 << 1; // Binary: 1010 (10 in decimal)
        Console.WriteLine("Left Shift Operator (<<): " + leftShiftResult); // Output: 10

        // Right Shift Operator (>>)
        int rightShiftResult = num1 >> 1; // Binary: 0010 (2 in decimal)
        Console.WriteLine("Right Shift Operator (>>): " + rightShiftResult); // Output: 2
    }
}



