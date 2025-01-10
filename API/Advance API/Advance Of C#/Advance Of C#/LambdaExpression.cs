using System;

namespace Advance_Of_C_
{
    public class LambdaExpression
    {
        public void Practice()
        {
            Func<int, int> square = x => x * x;
            Console.WriteLine("Square of 5 is :" + square(5));

            Action<string> print = msg => Console.WriteLine(msg);
            print("Hello!"); // Output: Hello!

            Predicate<int> isPositive = x => x > 0;
            Console.WriteLine(isPositive(-1)); // Output: False

            // Nested Lambda Function
            Func<int, int> addTwo = x => x + 2;
            Func<int, int> addThree = x => x + 3;

            Func<int, Func<int, int>> addFunc = x => y => x + y;
            Console.WriteLine(addFunc(2)(3)); // Output: 5

        }
    }
}
