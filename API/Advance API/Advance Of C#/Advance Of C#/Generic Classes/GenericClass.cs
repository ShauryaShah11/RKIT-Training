using System;
using System.Collections.Generic;

namespace Advance_Of_C_
{
    /// <summary>
    /// The GenericClass is a generic class that demonstrates the usage of generics in C#.
    /// It provides methods to add, retrieve, and print items in both a list and a dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the items that can be stored in the list and dictionary.</typeparam>
    public class GenericClass<T>
    {
        /// <summary>
        /// A list to store items of type T.
        /// </summary>
        private List<T> list = new List<T>();

        /// <summary>
        /// A dictionary to store key-value pairs of type T.
        /// </summary>
        private Dictionary<T, T> dict = new Dictionary<T, T>();

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item">The item of type T to be added to the list.</param>
        public void AddItemInList(T item)
        {
            list.Add(item);
        }

        /// <summary>
        /// Adds a key-value pair to the dictionary. If the key already exists, it updates the value.
        /// </summary>
        /// <param name="key">The key of type T.</param>
        /// <param name="value">The value of type T associated with the key.</param>
        public void AddItemInDict(T key, T value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }

        /// <summary>
        /// Retrieves an item from the dictionary by its key.
        /// </summary>
        /// <param name="key">The key of type T to retrieve the value.</param>
        /// <returns>The value associated with the key.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the key does not exist in the dictionary.</exception>
        public T GetItemFromDictByKey(T key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Retrieves an item from the list by its index.
        /// </summary>
        /// <param name="index">The index of the item in the list.</param>
        /// <returns>The item at the specified index in the list.</returns>
        public T GetItemFromList(int index)
        {
            return list[index];
        }

        /// <summary>
        /// Prints all the items in the list to the console.
        /// </summary>
        public void PrintList()
        {
            foreach (T item in list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Prints all key-value pairs in the dictionary to the console.
        /// </summary>
        public void PrintDict()
        {
            foreach (KeyValuePair<T, T> pair in dict)
            {
                Console.WriteLine(pair.Key.ToString() + ": " + pair.Value.ToString());
            }
        }
    }
}
