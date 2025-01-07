using System;
using System.Collections.Generic;

namespace Advance_Of_C_
{
    public class GenericClass<T>
    {
        private List<T> list = new List<T>();
        private Dictionary<T, T> dict = new Dictionary<T, T>();

        public void AddItemInList(T item)
        {
            list.Add(item);
        }

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

        public T GetItemFromDictByKey(T key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            throw new KeyNotFoundException();
        }

        public T GetItemFromList(int index)
        {
            return list[index];
        }

        public void PrintList()
        {
            foreach (T item in list)
            {
                Console.WriteLine(item);
            }
        }

        public void PrintDict()
        {
            foreach (KeyValuePair<T, T> pair in dict)
            {
                Console.WriteLine(pair.Key.ToString() + ": " + pair.Value.ToString());
            }
        }
    }
}
