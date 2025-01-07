using System;

namespace Advance_Of_C_.Generic_Classes
{
    // T must be a reference type (class)
    public class GenericClassWithWhere<T> where T : class
    {
        public void DisplayType()
        {
            Console.WriteLine($"Type is: {typeof(T).Name}");
        }
    }

    // T must be a value type (struct)
    public class GenericClassWithStruct<T> where T : struct
    {
        public T Add(T a, T b)
        {
            dynamic x = a;
            dynamic y = b;
            return x + y; // Works due to dynamic
        }
    }

    // T must have a parameterless constructor
    public class GenericClassWithConstructor<T> where T : new()
    {
        public T CreateInstance()
        {
            return new T();
        }
    }

    // T must inherit from SomeBaseClass
    public class GenericClassWithBase<T> where T : SomeBaseClass
    {
        public void DisplayBaseProperty(T obj)
        {
            Console.WriteLine($"Base Property: {obj.BaseProperty}");
        }
    }

    // T must implement an interface
    public class GenericClassWithInterface<T> where T : IDisplayable
    {
        public void Display(T obj)
        {
            obj.Display();
        }
    }

    // Example base class
    public class SomeBaseClass
    {
        public string BaseProperty { get; set; } = "Base Class Property";
    }

    // Example interface
    public interface IDisplayable
    {
        void Display();
    }

    // Example class implementing IDisplayable
    public class DisplayableClass : IDisplayable
    {
        public string Name { get; set; } = "Displayable Object";

        public void Display()
        {
            Console.WriteLine($"Displaying: {Name}");
        }
    }
}
