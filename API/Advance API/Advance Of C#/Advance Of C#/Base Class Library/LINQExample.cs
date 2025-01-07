using System;
using System.Linq;

namespace Advance_Of_C_.Base_Class_Library
{
    public class LINQExample
    {
        public void DemonstrateLINQ()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6 };

            // LINQ Query to find even numbers
            var evenNumbers = from n in numbers
                              where n % 2 == 0
                              select n;

            Console.WriteLine("Even Numbers:");
            foreach (var num in evenNumbers)
            {
                Console.WriteLine(num);
            }

            // LINQ Method Syntax to find numbers greater than 3
            var greaterThanThree = numbers.Where(n => n > 3);
            Console.WriteLine("\nNumbers greater than 3:");
            foreach (var num in greaterThanThree)
            {
                Console.WriteLine(num);
            }
        }
    }
}
