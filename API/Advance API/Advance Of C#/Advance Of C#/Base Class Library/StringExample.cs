using System;

namespace Advance_Of_C_.Base_Class_Library
{
    class StringExample
    {
        public void DemonstrateString()
        {
            string str1 = "Hello";
            string str2 = "World";

            // Concatenation
            string result = string.Concat(str1, " ", str2);
            Console.WriteLine("Concatenated String: " + result);

            // String Interpolation
            string interpolatedResult = $"{str1}, {str2}!";
            Console.WriteLine("Interpolated String: " + interpolatedResult);

            // String Length
            Console.WriteLine("Length of str1: " + str1.Length);

            // Substring
            string substring = str1.Substring(1, 3);
            Console.WriteLine("Substring of str1: " + substring);
        }
    }
}
