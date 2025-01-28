using System;
using System.Collections.Generic;

namespace Advance_Of_C_.Base_Class_Library
{
    public class CollectionExample
    {
        public void DemonstrateCollections()
        {
            // List Example
            List<string> fruits = new List<string> { "Apple", "Banana", "Orange" };
            fruits.Add("Grapes");
            Console.WriteLine("Fruits List:");
            foreach (var fruit in fruits)
            {
                Console.WriteLine(fruit);
            }

            // Dictionary Example
            Dictionary<int, string> students = new Dictionary<int, string>
            {
                { 1, "Alice" },
                { 2, "Bob" },
                { 3, "Charlie" }
            };

            Console.WriteLine("\nStudent Dictionary:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Key}, Name: {student.Value}");
            }
        }
    }
}
