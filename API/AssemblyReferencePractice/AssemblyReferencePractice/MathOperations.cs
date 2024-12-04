using System;
namespace AssemblyReferencePractice
{
    /// <summary>
    ///  MathOperations class contains methods for basic arithmetic operations.
    /// </summary>
    public class MathOperations
    {
        /// <summary>
        /// Adds two integers.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <returns>The sum of the two integers.</returns>
        public int add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Subtracts the second integer from the first integer.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <returns>The difference between the two integers.</returns>
        public int sub(int a, int b)
        {
            return a - b;
        }

        /// <summary>
        /// Multiplies two integers.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <returns>The product of the two integers.</returns>
        public int mul(int a, int b)
        {
            return a * b;
        }

        /// <summary>
        /// Divides the first integer by the second integer.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <returns>The quotient of the two integers.</returns>
        /// <exception cref="DivideByZeroException">Thrown when the second integer is zero.</exception>
        public int div(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Division by zero is not allowed.");
            }
            return a / b;
        }
    }
}
