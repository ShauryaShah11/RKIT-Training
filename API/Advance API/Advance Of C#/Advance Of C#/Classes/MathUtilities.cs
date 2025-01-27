namespace Advance_Of_C_.Classes
{
    /// <summary>
    /// The MathUtilities static class provides basic mathematical utility methods.
    /// It includes methods for adding and multiplying two integers.
    /// </summary>
    public static class MathUtilities
    {
        /// <summary>
        /// Adds two integers together.
        /// </summary>
        /// <param name="num1">The first integer to be added.</param>
        /// <param name="num2">The second integer to be added.</param>
        /// <returns>The sum of the two integers.</returns>
        public static int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        /// <summary>
        /// Multiplies two integers together.
        /// </summary>
        /// <param name="num1">The first integer to be multiplied.</param>
        /// <param name="num2">The second integer to be multiplied.</param>
        /// <returns>The product of the two integers.</returns>
        public static int Multiply(int num1, int num2)
        {
            return num1 * num2;
        }
    }
}
