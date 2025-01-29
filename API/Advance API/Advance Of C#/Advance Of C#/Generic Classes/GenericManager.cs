using System;
using System.Collections.Generic;

namespace Advance_Of_C_.Generic_Classes
{
    public class GenericManager<T1, T2>
    {
        // A generic method that returns a tuple with two generic values
        public (T1 item1, T2 item2) Add(T1 item1, T2 item2)
        {
            Console.WriteLine("Adding items...");
            return (item1, item2); // Return as a tuple
        }

        // A generic method that uses out parameters to return two values
        public void AddOutParams(T1 item1, T2 item2, out T1 result1, out T2 result2)
        {
            Console.WriteLine("Adding items with out parameters...");
            result1 = item1;
            result2 = item2;
        }

        // A generic method that returns a custom class holding two values
        public Result<T1, T2> AddCustomClass(T1 item1, T2 item2)
        {
            Console.WriteLine("Adding items using a custom class...");
            return new Result<T1, T2>(item1, item2);
        }

        // A generic method that returns a dictionary holding the two values as key-value pairs
        public Dictionary<string, object> AddToDictionary(T1 item1, T2 item2)
        {
            Console.WriteLine("Adding items to dictionary...");
            var dict = new Dictionary<string, object>
            {
                { "Item1", item1 },
                { "Item2", item2 }
            };
            return dict;
        }

        // A generic method that returns a List with two values
        public List<object> AddToList(T1 item1, T2 item2)
        {
            Console.WriteLine("Adding items to list...");
            return new List<object> { item1, item2 };
        }
    }

    // A custom class to hold two values of different types
    public class Result<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public Result(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}
