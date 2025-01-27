using System;

namespace Advance_Of_C_.Generic_Classes
{
    /// <summary>
    /// A generic class that enforces the constraint that T must be a reference type (class).
    /// </summary>
    /// <typeparam name="T">The type parameter which must be a reference type.</typeparam>
    public class GenericClassWithWhere<T> where T : class
    {
        /// <summary>
        /// Displays the name of the type T.
        /// </summary>
        public void DisplayType()
        {
            Console.WriteLine($"Type is: {typeof(T).Name}");
        }
    }

    /// <summary>
    /// A generic class that enforces the constraint that T must be a value type (struct).
    /// </summary>
    /// <typeparam name="T">The type parameter which must be a value type.</typeparam>
    public class GenericClassWithStruct<T> where T : struct
    {
        /// <summary>
        /// Adds two values of type T and returns the result.
        /// This method works by using dynamic type to perform the addition operation.
        /// </summary>
        /// <param name="a">First value of type T.</param>
        /// <param name="b">Second value of type T.</param>
        /// <returns>The sum of the two values.</returns>
        public T Add(T a, T b)
        {
            dynamic x = a;
            dynamic y = b;
            return x + y; // Works due to dynamic
        }
    }

    /// <summary>
    /// A generic class that enforces the constraint that T must have a parameterless constructor.
    /// </summary>
    /// <typeparam name="T">The type parameter which must have a parameterless constructor.</typeparam>
    public class GenericClassWithConstructor<T> where T : new()
    {
        /// <summary>
        /// Creates an instance of type T using its parameterless constructor.
        /// </summary>
        /// <returns>A new instance of type T.</returns>
        public T CreateInstance()
        {
            return new T();
        }
    }

    /// <summary>
    /// A generic class that enforces the constraint that T must inherit from SomeBaseClass.
    /// </summary>
    /// <typeparam name="T">The type parameter which must inherit from SomeBaseClass.</typeparam>
    public class GenericClassWithBase<T> where T : SomeBaseClass
    {
        /// <summary>
        /// Displays the BaseProperty of the given object of type T.
        /// </summary>
        /// <param name="obj">The object of type T.</param>
        public void DisplayBaseProperty(T obj)
        {
            Console.WriteLine($"Base Property: {obj.BaseProperty}");
        }
    }

    /// <summary>
    /// A generic class that enforces the constraint that T must implement the IDisplayable interface.
    /// </summary>
    /// <typeparam name="T">The type parameter which must implement IDisplayable interface.</typeparam>
    public class GenericClassWithInterface<T> where T : IDisplayable
    {
        /// <summary>
        /// Calls the Display method of the given object of type T.
        /// </summary>
        /// <param name="obj">The object of type T.</param>
        public void Display(T obj)
        {
            obj.Display();
        }
    }

    /// <summary>
    /// An example base class with a property that can be inherited by other classes.
    /// </summary>
    public class SomeBaseClass
    {
        /// <summary>
        /// A property in the base class that can be accessed by derived classes.
        /// </summary>
        public string BaseProperty { get; set; } = "Base Class Property";
    }

    /// <summary>
    /// An example interface that enforces a Display method to be implemented by any class that implements this interface.
    /// </summary>
    public interface IDisplayable
    {
        /// <summary>
        /// Displays the object information.
        /// </summary>
        void Display();
    }

    /// <summary>
    /// An example class that implements the IDisplayable interface and provides a definition for the Display method.
    /// </summary>
    public class DisplayableClass : IDisplayable
    {
        /// <summary>
        /// A property to hold the name of the displayable object.
        /// </summary>
        public string Name { get; set; } = "Displayable Object";

        /// <summary>
        /// Displays the name of the object.
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"Displaying: {Name}");
        }
    }
}
