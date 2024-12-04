using System;
using System.Collections.Generic;

/// <summary>
/// The CollectionFramework class demonstrates basic collection operations in C#.
/// It include method for practicing list, dictionary, queue, stack, and hashset.
/// </summary>
class CollectionFramework
{
    /// <summary>
    /// The ListPractice method demonstrates basic list operations in C#.
    /// List is similar to Array but it can grow dynamically.
    /// It includes usage of various list methods like Add, RemoveAt, and accessing elements by index.
    /// </summary>
    public void ListPractice()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        Console.Write("Before adding element: ");
        numbers.ForEach(n => Console.Write(n + " ")); // Print elements with space
        Console.WriteLine(); 

        numbers.Add(6);  // Adds a new element to the end of the list
        numbers.RemoveAt(2); // Removes the element at index 2 (value 3)

        // RemoveAll method removes all elements that match the condition
        numbers.RemoveAll(x => x % 2 == 0); // Removes all even elements

        Console.Write("After modifications: ");
        numbers.ForEach(n => Console.Write(n + " ")); // Print elements with space
        Console.WriteLine(); // New line for better readability
    }

    /// <summary>
    /// The DictionaryPractice method demonstrates basic dictionary operations in C#.
    /// Dictionary Contains Key, Value Pair.
    /// It includes usage of dictionary methods like Add, and accessing elements by key and contains key or contains value.
    /// </summary>
    public void DictionaryPractice()
    {
        Dictionary<string, string> countries = new Dictionary<string, string>();
        countries.Add("USA", "United States of America");
        countries.Add("UK", "United Kingdom");
        countries.Add("IN", "India");

        countries.Remove("IN");

        Console.WriteLine(countries.ContainsKey("USA")); // Returns true
        Console.WriteLine(countries.ContainsValue("United Kingdom")); // Returns true

        Console.WriteLine(countries["UK"]); // Output: United Kingdom
        // Iterating over the dictionary
        foreach (KeyValuePair<string, string> kvp in countries)
        {
            Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
        }

    }

    /// <summary>
    /// The QueuePractice method demonstrates basic queue operations in C#.
    /// Queue Works in First In First Out Structure.
    /// It includes various method such as Enqueue, Dequeue, Peek, and Contains.
    /// </summary>
    public void QueuePractice()
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");

        Console.WriteLine("Queue contains third: "+ queue.Contains("Third"));

        Console.WriteLine(queue.Dequeue()); // Output: First
        Console.WriteLine(queue.Peek());    // Output: Second

    }

    /// <summary>
    /// The StackPractice method demonstrates basic stack operations in C#.
    /// Stack Works in Last In First Out Structure.
    /// It includes various method such as Push, Pop, Peek, Count and Contains.
    /// </summary>
    public void StackPractice()
    {
        Stack<string> stack = new Stack<string>();
        stack.Push("First");
        stack.Push("Second");
        stack.Push("Third");

        Console.WriteLine("Stack contains Second : "+ stack.Contains("Second"));
        Console.WriteLine("Stack length is: "+ stack.Count); // Output: 3

        Console.WriteLine(stack.Pop()); // Output: Third
        Console.WriteLine(stack.Peek()); // Output: Second
    }

    /// <summary>
    /// The HashSetPractice method demonstrates basic hashset operations in C#.
    /// The HashSet only Contains unique elements.
    /// It includes various method such as Add, Remove, RemoveWhere, Contains and Count.
    /// </summary>
    public void HashSetPractice()
    {
        HashSet<int> numbers = new HashSet<int> { 1, 2, 3, 4, 5, 5, 5 };
        numbers.Add(6);
        numbers.Remove(3);

        Console.WriteLine("Hash Set items are: ");
        foreach (var item in numbers)
        {
            Console.Write(item+" ");
        }

        // Remove elements that matches condition
        numbers.RemoveWhere(x => x % 2 == 0); // Removes all even elements

        Console.WriteLine(numbers.Count);
        Console.WriteLine("Hash item after removing even elements: ");
        foreach (var item in numbers)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
        Console.WriteLine(numbers.Contains(3));
    }
}