# Advance C#

These Documentation Include Various Topics:

1. [Types Of Classes](#types-of-classes)
2. [Generics](#generics)
3. [FileSystem and Streams in C#](#filesystem-and-streams-in-c)
4. [Data Serialization in C# (Binary, Json, XML)](#data-serialization-in-cbinary-json-xml)
5. [Base Class Library Features in C#](#base-class-library-features-in-c)
6. [Lambda Expressions in C#](#lambda-expressions-in-c)
7. [Extension Methods in C#](#extension-methods-in-c)
8. [LINQ](#linq)
9. [Security and Cryptography in C#](#security-and-cryptography-in-c)

## Types Of Classes

### 1. **Abstract Class**

- **Definition**: An abstract class is a class that cannot be instantiated directly. It is meant to be a base class for other classes to inherit from. It may contain abstract methods (without a body) that derived classes are required to implement.
- **Usage**: Used to define a common interface or shared functionality for derived classes, but prevents direct instantiation.

**Example**:

```csharp
public abstract class Animal
{
    public abstract void MakeSound(); // Abstract method to be implemented by derived classes

    public void Breathe()  // Non-abstract method, can be used as-is in derived classes
    {
        Console.WriteLine("Breathing...");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Bark");
    }
}

public class Program
{
    public static void Main()
    {
        // Animal animal = new Animal(); // Error: Cannot instantiate abstract class
        Dog dog = new Dog();
        dog.MakeSound(); // Output: Bark
        dog.Breathe();  // Output: Breathing...
    }
}
```

**Key Points**:

- Cannot instantiate an abstract class.
- Can have abstract and non-abstract methods.
- Derived classes must implement the abstract methods.

---

### 2. **Sealed Class**

- **Definition**: A sealed class is a class that cannot be inherited from. It can be instantiated like a normal class, but no other class can derive from it.
- **Usage**: Used to prevent further inheritance, ensuring that the class’s functionality is final and cannot be modified by other classes.

**Example**:

```csharp
public sealed class FinalClass
{
    public void DisplayMessage()
    {
        Console.WriteLine("This is a final class.");
    }
}

// The following will cause an error because FinalClass is sealed
// public class DerivedClass : FinalClass { }

public class Program
{
    public static void Main()
    {
        FinalClass obj = new FinalClass();
        obj.DisplayMessage(); // Output: This is a final class.
    }
}
```

**Key Points**:

- Cannot be inherited.
- Can be instantiated normally.
- Useful when you want to prevent further extensions of the class.

---

### 3. **Static Class**

- **Definition**: A static class is a class that cannot be instantiated. All its members must be static, meaning they belong to the class itself rather than to instances of the class.
- **Usage**: Used to organize utility or helper methods that don't require object state (e.g., `Math` class).

**Example**:

```csharp
public static class MathUtilities
{
    public static int Add(int a, int b) => a + b;
    public static int Multiply(int a, int b) => a * b;
}

public class Program
{
    public static void Main()
    {
        int sum = MathUtilities.Add(5, 10); // No instance needed, directly call static method
        Console.WriteLine(sum); // Output: 15
    }
}
```

**Key Points**:

- Cannot be instantiated.
- All members must be static.
- Useful for utility or helper functions.

---

### 4. **Normal (Non-Sealed, Non-Abstract) Class**

- **Definition**: A regular class that can be instantiated and used in any way without restrictions on inheritance.
- **Usage**: The most common type of class, used for general-purpose object creation and functionality.

**Example**:

```csharp
public class Car
{
    public string Make { get; set; }
    public string Model { get; set; }

    public void Drive()
    {
        Console.WriteLine("The car is driving...");
    }
}

public class Program
{
    public static void Main()
    {
        Car myCar = new Car { Make = "Toyota", Model = "Corolla" };
        myCar.Drive(); // Output: The car is driving...
    }
}
```

**Key Points**:

- Can be instantiated.
- Can be inherited from.
- Allows full flexibility in object-oriented design.

---

### 5. **Partial Class**

- **Definition**: A partial class is a class that can be split into multiple files. Each part of the class contains a part of the class's methods, properties, etc., but when compiled, they are treated as one single class.
- **Usage**: Used for organizing large classes or auto-generated code (e.g., in designer files in ASP.NET).

**Example**:

- **File1.cs**:
    
    ```csharp
    public partial class Employee
    {
        public string Name { get; set; }
    }
    ```
    
- **File2.cs**:
    
    ```csharp
    public partial class Employee
    {
        public int Age { get; set; }
    }
    ```
    

**Key Points**:

- Allows splitting a class into multiple files.
- All parts of the class are merged during compilation.
- Useful for large classes or when working with auto-generated code.

---

### 6. **Inner (Nested) Class**

- **Definition**: An inner (or nested) class is a class that is defined inside another class. It is used to logically group classes that are only used within the containing class.
- **Usage**: Typically used when the nested class is closely related to the outer class, and it helps to encapsulate implementation details.

**Example**:

```csharp
public class OuterClass
{
    public class InnerClass
    {
        public void DisplayMessage()
        {
            Console.WriteLine("This is an inner class.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        OuterClass.InnerClass inner = new OuterClass.InnerClass();
        inner.DisplayMessage(); // Output: This is an inner class.
    }
}
```

**Key Points**:

- Defined within another class.
- Can be used for logically grouped functionality.
- Can access private members of the outer class (if declared as `inner` class).

---

### 7. **Singleton Class**

- **Definition**: A Singleton is a design pattern that ensures a class has only one instance and provides a global point of access to it.
- **Usage**: Typically used when you need to control access to a shared resource, like a database connection.

**Example**:

```csharp
public class Singleton
{
    private static Singleton instance;
    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }

    public void ShowMessage()
    {
        Console.WriteLine("Singleton Instance");
    }
}

public class Program
{
    public static void Main()
    {
        Singleton singleton = Singleton.Instance;
        singleton.ShowMessage(); // Output: Singleton Instance
    }
}
```

**Key Points**:

- Ensures only one instance of the class is created.
- Useful for controlling shared resources.
- Instance is accessed via a static property.

---

### Summary of Class Types:

| Class Type | Can be Instantiated | Can be Inherited | Key Characteristics |  |
| --- | --- | --- | --- | --- |
| **Abstract** | No | Yes | Cannot instantiate, may contain abstract methods |  |
| **Sealed** | Yes | No | Cannot be inherited from |  |
| **Static** | No | No | All members are static |  |
| **Normal** | Yes | Yes | Regular class with no restrictions |  |
| **Partial** | Yes | Yes | Split across multiple files |  |
| **Inner (Nested)** | Yes | Yes | Defined inside another class |  |
| **Singleton** | Yes (only once) | Yes | Ensures a single instance globally |  |

## Generics

**Generics** in C# are a way to define classes, methods, and data structures with type parameters. This allows the code to be written without committing to a specific data type, making it flexible, reusable, and type-safe. Generics enable the creation of methods and classes that can work with any data type, while still enforcing type safety at compile-time.

---

### **Why Use Generics?**

1. **Code Reusability**: Write a single method or class that works with any data type.
2. **Type Safety**: With generics, types are checked at compile time, reducing runtime errors.
3. **Performance**: Avoid unnecessary boxing/unboxing for value types and optimize memory usage.

---

### **Syntax of Generics in C#**

### 1. **Generic Methods**

A generic method is a method that can operate on objects of various data types without knowing the data type at the time of writing the method.

**Syntax:**

```csharp

public T GenericMethod<T>(T param)
{
    // Your logic here
    return param;
}

```

**Example:**

```csharp

public T GetFirstElement<T>(T[] array)
{
    return array[0];
}

```

In this case, `T` is a placeholder for any data type, and it allows `GetFirstElement` to work with any type of array.

### 2. **Generic Classes**

A generic class is a class that can work with any data type, defined with a type parameter.

**Syntax:**

```csharp

public class ClassName<T>
{
    // Use T as a data type
    private T item;

    public void SetItem(T value)
    {
        item = value;
    }

    public T GetItem()
    {
        return item;
    }
}

```

**Example:**

```csharp

public class Box<T>
{
    private T value;

    public Box(T value)
    {
        this.value = value;
    }

    public T GetValue()
    {
        return value;
    }
}

var intBox = new Box<int>(10); // Box for int
var stringBox = new Box<string>("Hello!"); // Box for string

```

### 3. **Generic Interfaces**

Just like classes and methods, interfaces can also be generic, which allows creating flexible and reusable contracts for different data types.

**Syntax:**

```csharp

public interface IContainer<T>
{
    void Add(T item);
    T Get(int index);
}

```

---

## Constraints In Generics

In C#, **generics** allow you to create classes, methods, or interfaces with type parameters. Sometimes, you may want to restrict the types that can be used with a generic class or method. This is where **constraints** come into play.

Constraints allow you to specify rules on the types that can be used as arguments for type parameters. These constraints are applied using the `where` keyword.

### Types of Constraints

1. **`where T : struct`** - Restricts `T` to value types (cannot be a reference type).
2. **`where T : class`** - Restricts `T` to reference types (cannot be a value type).
3. **`where T : new()`** - Ensures that `T` has a parameterless constructor.
4. **`where T : SomeBaseClass`** - Restricts `T` to be a specific class or a subclass of a class.
5. **`where T : IComparable`** - Ensures that `T` implements the `IComparable` interface (which provides a method to compare instances of the type).

---

### 1. **Reference Type Constraint (`where T : class`)**

This constraint ensures that the type `T` must be a reference type (e.g., a class, interface, or delegate).

### Example:

```csharp

public class GenericClassWithReferenceType<T> where T : class
{
    public void DisplayType()
    {
        Console.WriteLine($"Type is: {typeof(T).Name}");
    }
}

```

**Explanation:**

- The `where T : class` constraint enforces that only reference types (such as `string`, custom classes, etc.) can be used for `T`.
- This would not work for value types like `int`, `double`, or `bool`.

**Usage:**

```csharp

var obj = new GenericClassWithReferenceType<string>();
obj.DisplayType();  // Output: Type is: String

```

---

### 2. **Value Type Constraint (`where T : struct`)**

This constraint ensures that the type `T` must be a value type (e.g., primitive types, structs).

### Example:

```csharp

public class GenericClassWithValueType<T> where T : struct
{
    public T Add(T a, T b)
    {
        dynamic x = a;
        dynamic y = b;
        return x + y;  // Uses dynamic to perform addition on value types
    }
}

```

**Explanation:**

- The `where T : struct` constraint allows only value types to be used for `T` (such as `int`, `float`, `DateTime`).
- `dynamic` is used here to perform addition because it can handle various value types.

**Usage:**

```csharp

var obj = new GenericClassWithValueType<int>();
int result = obj.Add(5, 10);  // Output: 15

```

---

### 3. **Parameterless Constructor Constraint (`where T : new()`)**

This constraint ensures that the type `T` must have a parameterless (default) constructor.

### Example:

```csharp

public class GenericClassWithConstructor<T> where T : new()
{
    public T CreateInstance()
    {
        return new T();  // Creates a new instance of T using its parameterless constructor
    }
}

```

**Explanation:**

- The `where T : new()` constraint restricts the type `T` to types that have a parameterless constructor.
- This is useful when you want to instantiate objects of type `T` without needing to pass parameters to a constructor.

**Usage:**

```csharp

public class MyClass
{
    public MyClass() { }  // Parameterless constructor
}

var obj = new GenericClassWithConstructor<MyClass>();
MyClass instance = obj.CreateInstance();  // Successfully creates a new instance of MyClass

```

---

### 4. **Base Class Constraint (`where T : SomeBaseClass`)**

This constraint ensures that the type `T` must inherit from a specific base class or must be that base class itself.

### Example:

```csharp

public class SomeBaseClass
{
    public string BaseProperty { get; set; } = "Base Class Property";
}

public class DerivedClass : SomeBaseClass { }

public class GenericClassWithBase<T> where T : SomeBaseClass
{
    public void DisplayBaseProperty(T obj)
    {
        Console.WriteLine($"Base Property: {obj.BaseProperty}");
    }
}

```

**Explanation:**

- The `where T : SomeBaseClass` constraint ensures that `T` must be either `SomeBaseClass` or a derived class of `SomeBaseClass`.
- This allows accessing properties and methods defined in the base class.

**Usage:**

```csharp

var obj = new GenericClassWithBase<DerivedClass>();
obj.DisplayBaseProperty(new DerivedClass());  // Output: Base Property: Base Class Property

```

---

### 5. **Interface Implementation Constraint (`where T : IComparable`)**

This constraint ensures that the type `T` implements a specific interface, such as `IComparable`, which is used for comparing instances of the type.

### Example:

```csharp

public class GenericClassWithInterface<T> where T : IComparable
{
    public bool Compare(T x, T y)
    {
        return x.CompareTo(y) > 0;  // Compares x and y using IComparable's CompareTo method
    }
}

```

**Explanation:**

- The `where T : IComparable` constraint ensures that `T` implements the `IComparable` interface, which provides a method for comparing instances of the type.
- This is useful when you want to perform comparisons between instances of the generic type.

**Usage:**

```csharp

var obj = new GenericClassWithInterface<int>();
bool result = obj.Compare(5, 3);  // Output: true

```

---

## Combining Multiple Constraints

You can also combine multiple constraints for a generic class or method. For example, you can require that a type `T` is both a reference type and implements an interface.

### Example:

```csharp

public class GenericClassWithMultipleConstraints<T> where T : class, IComparable
{
    public bool Compare(T x, T y)
    {
        return x.CompareTo(y) > 0;  // Compares x and y using IComparable
    }
}

```

**Explanation:**

- The type `T` is constrained to be a `class` (reference type) and must implement `IComparable`.
- This allows using comparison functionality on reference types that implement the interface.

**Usage:**

```csharp

var obj = new GenericClassWithMultipleConstraints<string>();
bool result = obj.Compare("apple", "banana");  // Output: false

```

---

### Conclusion

Constraints in generics are a powerful feature in C# that help ensure that the types used with generic classes and methods meet specific requirements. By using constraints, you can make your code more flexible while maintaining type safety and improving the overall structure of your application.

---

## **Benefits of Generics**

1. **Type Safety**: Errors related to type mismatches can be caught at compile-time, reducing bugs.
2. **Reduced Code Duplication**: Instead of writing multiple methods or classes for each data type, you can write a single method/class that works with any type.
3. **Better Performance**: Since generics use the actual type, it avoids the overhead of boxing/unboxing and type conversions, which enhances performance.
4. **Maintainability**: Generic code is easier to maintain since it can be reused for multiple types and avoids duplicate code.

---

## **Limitations of Generics**

1. **No Support for Non-Reference Types**: Some complex operations are restricted in generics (like using certain operators) unless constrained.
2. **Cannot Instantiate a Type Parameter**: You cannot create an instance of the type parameter `T` inside the class (e.g., you can’t do `new T()` unless constrained).
3. **Cannot Use `sizeof` Operator**: You cannot apply `sizeof` to a generic type directly.

---

## **Example Use Cases for Generics**

1. **Generic Collections**
    - Collections such as `List<T>`, `Queue<T>`, and `Dictionary<TKey, TValue>` are examples of generic types in C#.
    - They provide flexibility by allowing elements to be added in a type-safe manner.
    
    **Example**:
    
    ```csharp
    
    List<int> numbers = new List<int>();
    numbers.Add(1);
    numbers.Add(2);
    ```
    
2. **Custom Generic Classes**
    - You can build custom data structures such as stacks, queues, or linked lists using generics.
    
    **Example**: Generic Stack
    
    ```csharp
    
    public class Stack<T>
    {
        private List<T> items = new List<T>();
    
        public void Push(T item)
        {
            items.Add(item);
        }
    
        public T Pop()
        {
            var lastIndex = items.Count - 1;
            var item = items[lastIndex];
            items.RemoveAt(lastIndex);
            return item;
        }
    }
    
    ```
    
3. **Working with Algorithms**
    - Many algorithms can be written generically to work with different data types (sorting, searching, etc.).
    
    **Example**: Generic Swap Method
    
    ```csharp
    
    public void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
    ```
    

---

## **Advanced Topics in Generics**

### 1. **Covariance and Contravariance**

- **Covariance** allows you to use a more derived type than originally specified.
- **Contravariance** allows using a more generic type.

**Example**:

```csharp

public delegate T CovariantDelegate<out T>(int x);
```

### 2. **Generic Delegates**

- You can define delegates using generics, making them flexible for any method signature.

**Example**:

```csharp

public delegate TResult Func<in T, out TResult>(T arg);
```

---

## **Key Takeaways**

- Generics allow creating type-safe, reusable code that can work with any data type.
- **Type Safety** and **Code Reusability** are the primary benefits.
- **Constraints** on generics ensure that the type parameters meet certain criteria.
- Generics are extensively used in .NET libraries, such as collections and LINQ queries.

---

## FileSystem and Streams in C#

In C#, the **System.IO** namespace provides a rich set of APIs for working with the filesystem, including **streams** that allow you to read and write data to files, memory, or even encrypted data. Below, we'll explore how to use different types of streams to handle data efficiently.

---

### 1. **FileStream**

`FileStream` is used to read from and write to a file. It allows you to work with files on a byte-by-byte basis, giving you low-level control over file operations.

### Example: FileStream for Reading and Writing

```csharp

using System;
using System.IO;

class FileStreamExample
{
    public void WriteToFile(string filePath, string content)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
            fs.Write(data, 0, data.Length);
        }
    }

    public void ReadFromFile(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
        }
    }
}

```

**Explanation:**

- `FileStream` opens or creates a file and allows you to write or read byte data.
- The `Write` method is used to write to a file, while `Read` is used for reading.
- It’s efficient for large files or binary data.

**Usage:**

```csharp

var fsExample = new FileStreamExample();
fsExample.WriteToFile("example.txt", "Hello, FileStream!");
fsExample.ReadFromFile("example.txt");  // Output: Hello, FileStream!

```

---

### 2. **BufferedStream**

`BufferedStream` improves the performance of file reading and writing by buffering the data before performing the actual I/O operations. It is typically used with other streams (like `FileStream`) to reduce the number of read and write operations.

### Example: BufferedStream for Efficient File Operations

```csharp

using System;
using System.IO;

class BufferedStreamExample
{
    public void WriteToFile(string filePath, string content)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (BufferedStream bs = new BufferedStream(fs))
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
            bs.Write(data, 0, data.Length);
        }
    }

    public void ReadFromFile(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (BufferedStream bs = new BufferedStream(fs))
        {
            byte[] data = new byte[fs.Length];
            bs.Read(data, 0, data.Length);
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
        }
    }
}

```

**Explanation:**

- `BufferedStream` wraps another stream (`FileStream` in this case) and provides a memory buffer to store data temporarily.
- It minimizes the number of disk accesses by reading larger chunks into memory before processing them.
- This is useful when working with large files where efficiency is important.

**Usage:**

```csharp

var bufferedStreamExample = new BufferedStreamExample();
bufferedStreamExample.WriteToFile("buffered_example.txt", "Hello, BufferedStream!");
bufferedStreamExample.ReadFromFile("buffered_example.txt");  // Output: Hello, BufferedStream!

```

---

### 3. **CryptoStream**

`CryptoStream` is used for working with data encryption and decryption. It wraps around other streams and allows you to encrypt or decrypt data as it's being read from or written to the stream.

### Example: CryptoStream for Encryption

```csharp

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class CryptoStreamExample
{
    public void EncryptFile(string inputFile, string outputFile, string key, string iv)
    {
        using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
        using (Aes aesAlg = Aes.Create())
        using (CryptoStream cs = new CryptoStream(fsOutput, aesAlg.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Write))
        {
            byte[] buffer = new byte[fsInput.Length];
            fsInput.Read(buffer, 0, buffer.Length);
            cs.Write(buffer, 0, buffer.Length);
        }
    }

    public void DecryptFile(string inputFile, string outputFile, string key, string iv)
    {
        using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
        using (Aes aesAlg = Aes.Create())
        using (CryptoStream cs = new CryptoStream(fsInput, aesAlg.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Read))
        {
            byte[] buffer = new byte[fsInput.Length];
            cs.Read(buffer, 0, buffer.Length);
            fsOutput.Write(buffer, 0, buffer.Length);
        }
    }
}

```

**Explanation:**

- `CryptoStream` is used in combination with cryptographic algorithms like `Aes` to encrypt or decrypt data as it passes through the stream.
- In the example, we encrypt data from a file and write the encrypted data to another file.
- Similarly, data can be decrypted while reading from a file.

**Usage:**

```csharp

var cryptoStreamExample = new CryptoStreamExample();
cryptoStreamExample.EncryptFile("example.txt", "encrypted_example.txt", "your-256-bit-key", "your-128-bit-iv");
cryptoStreamExample.DecryptFile("encrypted_example.txt", "decrypted_example.txt", "your-256-bit-key", "your-128-bit-iv");

```

---

### 4. **StreamWriter**

`StreamWriter` is a higher-level class that simplifies writing text data to a stream (such as a file). It is typically used for writing strings to files or other streams.

### Example: StreamWriter for Writing Text to a File

```csharp

using System;
using System.IO;

class StreamWriterExample
{
    public void WriteTextToFile(string filePath, string content)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(content);
        }
    }
}

```

**Explanation:**

- `StreamWriter` is designed to write character data to a stream. It makes writing text files simple.
- The `WriteLine` method writes a line of text to the file, while `Write` can be used to write without a newline.

**Usage:**

```csharp

var streamWriterExample = new StreamWriterExample();
streamWriterExample.WriteTextToFile("output.txt", "Hello, StreamWriter!");

```

---

### 5. **MemoryStream**

`MemoryStream` is used for working with data in memory rather than in files. It acts like a stream but operates on a byte array in memory, which is useful for scenarios where you don’t need to interact with the file system.

### Example: MemoryStream for In-Memory Operations

```csharp

using System;
using System.IO;
using System.Text;

class MemoryStreamExample
{
    public void UseMemoryStream(string content)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            byte[] data = Encoding.UTF8.GetBytes(content);
            ms.Write(data, 0, data.Length);

            ms.Position = 0;  // Reset position to read from the beginning
            byte[] buffer = new byte[ms.Length];
            ms.Read(buffer, 0, buffer.Length);

            Console.WriteLine(Encoding.UTF8.GetString(buffer));
        }
    }
}

```

**Explanation:**

- `MemoryStream` allows you to treat an in-memory byte array like a stream. It's useful for scenarios like manipulating data in memory without needing to interact with files.
- You can write to it and read from it, just like a normal stream, but everything stays in memory.

**Usage:**

```csharp

var memoryStreamExample = new MemoryStreamExample();
memoryStreamExample.UseMemoryStream("Hello, MemoryStream!");  // Output: Hello, MemoryStream!

```

---

## Conclusion

In C#, streams from the **System.IO** namespace allow for efficient handling of data, whether it's from files, memory, or even encrypted data. You can combine different streams for enhanced performance or additional functionality (e.g., buffering or encryption). By using the appropriate stream class, you can optimize your application for better performance and security.

---

## Data Serialization in C# (Binary, Json, XML)

Serialization is the process of converting an object into a format that can be easily stored or transmitted (for example, to a file, over a network, or to a database). In C#, we have different types of serialization: **Binary**, **JSON**, and **XML**. These formats are widely used for data exchange, storage, and transfer.

---

### 1. **Binary Serialization**

Binary serialization converts an object into a binary format that is efficient for storage and transmission. The `System.Runtime.Serialization.Formatters.Binary` namespace is used for binary serialization.

### Example: Binary Serialization and Deserialization

```csharp

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class BinarySerializationExample
{
    public void Serialize(string filePath, Person person)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, person);
        }
    }

    public Person Deserialize(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (Person)formatter.Deserialize(fs);
        }
    }
}

```

**Explanation:**

- The `[Serializable]` attribute marks the class to be serializable.
- `BinaryFormatter.Serialize` converts the object into a binary format and writes it to a stream.
- `BinaryFormatter.Deserialize` reads the binary data and converts it back into the object.

**Usage:**

```csharp

var person = new Person { Name = "John Doe", Age = 30 };
var binaryExample = new BinarySerializationExample();
binaryExample.Serialize("person.dat", person);
Person deserializedPerson = binaryExample.Deserialize("person.dat");
Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");

```

---

### 2. **JSON Serialization**

JSON (JavaScript Object Notation) is a popular data-interchange format that is both human-readable and machine-readable. C# provides powerful libraries for JSON serialization, such as `Newtonsoft.Json` or `System.Text.Json`.

### Example: JSON Serialization and Deserialization

```csharp

using System;
using System.IO;
using Newtonsoft.Json;

class JsonSerializationExample
{
    public void Serialize(string filePath, Person person)
    {
        string jsonString = JsonConvert.SerializeObject(person);
        File.WriteAllText(filePath, jsonString);
    }

    public Person Deserialize(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<Person>(jsonString);
    }
}

```

**Explanation:**

- `JsonConvert.SerializeObject` converts the object into a JSON-formatted string.
- `File.WriteAllText` writes the JSON string to a file.
- `JsonConvert.DeserializeObject` converts the JSON string back into an object.

**Usage:**

```csharp
var person = new Person { Name = "Jane Doe", Age = 28 };
var jsonExample = new JsonSerializationExample();
jsonExample.Serialize("person.json", person);
Person deserializedPerson = jsonExample.Deserialize("person.json");
Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");

```

To use **Newtonsoft.Json**, you need to install the NuGet package:

```bash
dotnet add package Newtonsoft.Json
```

Alternatively, for **System.Text.Json** (built-in in .NET Core 3.0+):

```csharp
using System.Text.Json;

class JsonSerializationExample
{
    public void Serialize(string filePath, Person person)
    {
        string jsonString = JsonSerializer.Serialize(person);
        File.WriteAllText(filePath, jsonString);
    }

    public Person Deserialize(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Person>(jsonString);
    }
}
```

---

### 3. **XML Serialization**

XML (Extensible Markup Language) is a flexible, structured format for data exchange. C# provides XML serialization using the `System.Xml.Serialization` namespace.

### Example: XML Serialization and Deserialization

```csharp
using System;
using System.IO;
using System.Xml.Serialization;

class XmlSerializationExample
{
    public void Serialize(string filePath, Person person)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Person));
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(fs, person);
        }
    }

    public Person Deserialize(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Person));
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            return (Person)serializer.Deserialize(fs);
        }
    }
}
```

**Explanation:**

- `XmlSerializer` is used to convert an object to and from XML.
- `serializer.Serialize` converts the object into XML and writes it to a stream.
- `serializer.Deserialize` reads the XML data and converts it back into an object.

**Usage:**

```csharp
var person = new Person { Name = "Alex", Age = 35 };
var xmlExample = new XmlSerializationExample();
xmlExample.Serialize("person.xml", person);
Person deserializedPerson = xmlExample.Deserialize("person.xml");
Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
```

---

### Conclusion

Serialization in C# provides a convenient way to store and transfer objects in various formats. You can choose the appropriate serialization format based on the specific needs of your application (e.g., human-readable JSON, structured XML, or efficient Binary serialization).

---

## Base Class Library Features in C#

This document provides a demonstration of key features in C#'s Base Class Library (BCL). The following sections cover practical examples of common tasks that are easily performed using these built-in libraries.

---

### Table of Contents:

1. [Collections](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
2. [DateTime](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
3. [File I/O](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
4. [HTTP Requests with HttpClient](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
5. [StringBuilder](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
6. [String Operations](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)

---

### Collections

C# provides powerful collection types that allow for flexible data storage and manipulation. In this section, we will demonstrate two commonly used collections: **List** and **Dictionary**.

### List Example

The `List<T>` is a generic collection that can store any data type and allows for dynamic resizing.

```csharp
List<string> fruits = new List<string> { "Apple", "Banana", "Orange" };
fruits.Add("Grapes");

Console.WriteLine("Fruits List:");
foreach (var fruit in fruits)
{
    Console.WriteLine(fruit);
}
```

### Dictionary Example

The `Dictionary<TKey, TValue>` is a generic collection that stores key-value pairs, making it easy to retrieve values based on a unique key.

```csharp
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
```

---

### DateTime

The `DateTime` structure in C# provides methods and properties to handle dates and times effectively. In this section, we will demonstrate some common operations such as getting the current date, adding days, and formatting dates.

```csharp
DateTime currentDate = DateTime.Now;
Console.WriteLine("Current Date and Time: " + currentDate);

// Adding Days
DateTime futureDate = currentDate.AddDays(7);
Console.WriteLine("Date after 7 days: " + futureDate);

// Formatting Dates
string formattedDate = currentDate.ToString("dd/MM/yyyy");
Console.WriteLine("Formatted Date: " + formattedDate);

// Day of Week
Console.WriteLine("Day of the Week: " + currentDate.DayOfWeek);
```

---

### File I/O

C# provides a variety of methods for file operations such as reading and writing. Here we will demonstrate **File.WriteAllText()** and **File.ReadAllText()** methods.

```csharp
string path = "example.txt";

// Writing to a file
File.WriteAllText(path, "Hello, this is a test file!");
Console.WriteLine("File written successfully!");

// Reading from a file
if (File.Exists(path))
{
    string content = File.ReadAllText(path);
    Console.WriteLine("File Content: " + content);
}
```

---

### HTTP Requests with HttpClient

C# allows for easy communication with web services using `HttpClient`. In this section, we will demonstrate how to send an HTTP GET request to an external API (GitHub API in this case).

```csharp
using (HttpClient client = new HttpClient())
{
    HttpResponseMessage response = await client.GetAsync("https://api.github.com");
    string content = await response.Content.ReadAsStringAsync();
    Console.WriteLine("HTTP Response Content: " + content);
}
```

---

### StringBuilder

The `StringBuilder` class in C# is optimized for modifying strings. It is much more efficient than concatenating strings directly, especially in loops.

```csharp
StringBuilder sb = new StringBuilder();

// Efficiently appending to the string
sb.Append("Hello");
sb.Append(" ");
sb.Append("World");

Console.WriteLine("Built String: " + sb.ToString());
```

---

### String Operations

C# provides a wide variety of methods for string manipulation. This section demonstrates string concatenation, interpolation, length checking, and substring extraction.

### Concatenation

```csharp
string str1 = "Hello";
string str2 = "World";
string result = string.Concat(str1, " ", str2);
Console.WriteLine("Concatenated String: " + result);
```

### String Interpolation

```csharp
string interpolatedResult = $"{str1}, {str2}!";
Console.WriteLine("Interpolated String: " + interpolatedResult);
```

### String Length

```csharp
Console.WriteLine("Length of str1: " + str1.Length);
```

### Substring

```csharp
string substring = str1.Substring(1, 3);
Console.WriteLine("Substring of str1: " + substring);
```

---

### Summary

The **Base Class Library (BCL)** in C# provides a rich set of features to handle common programming tasks such as data storage, date manipulation, file handling, HTTP requests, and string manipulation. By using these built-in classes, C# developers can save time and avoid reinventing the wheel.

---

## Lambda Expressions in C#

Lambda expressions provide a concise way to represent anonymous methods (or inline methods). They are often used in LINQ queries and other scenarios where a small function is required. Lambda expressions can simplify code, making it more readable and maintainable.

This document covers basic and advanced usage of **Lambda Expressions** in C# with examples.

---

### Table of Contents:

1. [What is a Lambda Expression?](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
2. [Types of Lambda Expressions](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [Func](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [Action](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [Predicate](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
3. [Nested Lambda Expressions](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)

---

### What is a Lambda Expression?

A **lambda expression** is a shorthand way of writing anonymous methods in C#. It uses the `=>` syntax to separate parameters from the body of the function. The lambda expression can take one or more input parameters and return a value or perform an action.

Syntax:

```csharp

(parameters) => expression
```

---

### Types of Lambda Expressions

There are three commonly used types of delegates that lambda expressions can work with in C#: **Func**, **Action**, and **Predicate**.

---

### Func

A `Func<T, TResult>` is a delegate type that represents a function that takes one or more parameters and returns a value. It must always return a value.

Example: **Calculating the square of a number**

```csharp
Func<int, int> square = x => x * x;
Console.WriteLine("Square of 5 is: " + square(5));  // Output: Square of 5 is: 25
```

Explanation:

- `Func<int, int>` defines a function that takes an `int` as an input and returns an `int`.
- The lambda expression `x => x * x` calculates the square of the input.

---

### Action

An `Action<T>` is a delegate type that represents a method that performs an action but does not return a value. It can take parameters, but it cannot return a result.

Example: **Printing a message**

```csharp
Action<string> print = msg => Console.WriteLine(msg);
print("Hello!");  // Output: Hello!
```

Explanation:

- `Action<string>` defines a method that takes a `string` parameter and does not return a value.
- The lambda expression `msg => Console.WriteLine(msg)` prints the provided message to the console.

---

### Predicate

A `Predicate<T>` is a delegate type that represents a method that takes a single parameter and returns a boolean value. It is commonly used for checking conditions.

Example: **Checking if a number is positive**

```csharp
Predicate<int> isPositive = x => x > 0;
Console.WriteLine(isPositive(-1));  // Output: False
```

Explanation:

- `Predicate<int>` represents a method that takes an `int` and returns a `bool`.
- The lambda expression `x => x > 0` checks whether the number is greater than zero.

---

### Nested Lambda Expressions

Lambda expressions can be nested inside other lambda expressions. This can be useful when you want to create more complex functionality in a compact format.

Example: **Adding two numbers using nested lambda functions**

```csharp
Func<int, int> addTwo = x => x + 2;
Func<int, int> addThree = x => x + 3;

Func<int, Func<int, int>> addFunc = x => y => x + y;
Console.WriteLine(addFunc(2)(3));  // Output: 5
```

Explanation:

- `Func<int, int>` represents a function that takes an `int` and returns an `int`.
- `addFunc(2)(3)` is a **curried function** that returns another function which can accept a second parameter. In this case, the outer function takes `x = 2`, and the inner function takes `y = 3`. The result is `2 + 3 = 5`.

---

### Summary

Lambda expressions in C# provide a powerful and concise way to write methods inline. They can simplify your code by removing the need for explicit method declarations. The key types of lambda expressions are **Func**, **Action**, and **Predicate**, each serving a different purpose:

- **Func**: For methods that return a value.
- **Action**: For methods that perform an action but do not return a value.
- **Predicate**: For methods that return a boolean based on a condition.

Lambda expressions also support **nested expressions** and **currying**, allowing for more flexible and compact code.

---

## Extension Methods in C#

**Extension methods** in C# allow you to add functionality to existing types without modifying the original type or creating a new derived type. They are static methods but are called as if they were instance methods on the extended type.

This document explains the concept of **Extension Methods** in C# with real-life examples.

---

### Table of Contents:

1. [What are Extension Methods?](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
2. [How Extension Methods Work](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
3. [Examples of Extension Methods](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [Employee Extension](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [EnumerableCollectionExtensionMethods](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [Generic Filter Method](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [Int Extensions](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [Object Reset Extension](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
    - [String Extensions](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
4. [Advantages of Extension Methods](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)
5. [Limitations of Extension Methods](https://www.notion.so/Advance-C-173d69d3025280fc976aee788a26dc3c?pvs=21)

---

### What are Extension Methods?

Extension methods allow you to "add" methods to existing types without modifying their source code or creating new derived classes. This is done by defining static methods in a static class, and the method is called using the instance of the type it extends.

Syntax:

```csharp
public static class ClassNameExtensions
{
    public static ReturnType MethodName(this TargetType instance, parameters)
    {
        // Method body
    }
}
```

The `this` keyword before the first parameter indicates that the method is an extension method.

---

### How Extension Methods Work

- Extension methods are **static methods** defined in static classes.
- They work by **"extending" existing types**. They appear as if they are instance methods of the type being extended.
- You use the `this` keyword in the first parameter to specify the type being extended.

---

### Examples of Extension Methods

### Employee Extension

In this example, we extend the `IEnumerable<Employee>` collection to add a method `GetHighSalariedEmployee`, which filters employees based on a salary threshold.

```csharp
public static class EnumerableCollectionExtensionMethods
{
    public static IEnumerable<Employee> GetHighSalariedEmployee(this IEnumerable<Employee> employees, int salaryThreshold)
    {
        foreach (var emp in employees)
        {
            if (emp.Salary >= salaryThreshold)
            {
                yield return emp;
            }
        }
    }
}
```

**Explanation:**

- The method `GetHighSalariedEmployee` extends the `IEnumerable<Employee>` collection and filters employees based on the salary threshold.
- It uses `yield return` to return matching employees lazily.

---

### EnumerableCollectionExtensionMethods

Another example of an extension method for filtering collections based on a given condition:

```csharp
public static class Extension
{
    public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
    {
        List<T> filteredList = new List<T>();

        foreach (T record in records)
        {
            if(func(record))
            {
                filteredList.Add(record);
            }
        }
        return filteredList;
    }
}
```

**Explanation:**

- The `Filter` extension method takes a `List<T>` and a filter function (`Func<T, bool>`).
- It returns a new list with elements that satisfy the condition defined in the `func` predicate.

---

### Int Extensions

Here, two extension methods are added to the `int` type for incrementing values:

```csharp
public static class IntExtensions
{
    public static void Increment(this int num) => num++;

    public static void RefIncrement(ref this int num) => num++;
}
```

**Explanation:**

- `Increment`: Increments the `int` by 1 (Note: The value won’t persist because integers are passed by value).
- `RefIncrement`: Increments the `int` by 1 but uses the `ref` keyword so the change persists outside the method.

---

### Object Reset Extension

This extension method sets an object reference to `null`:

```csharp
public static class ObjectExtensions
{
    public static void ResetObject(ref object obj)
    {
        obj = null;
    }
}
```

**Explanation:**

- The `ResetObject` method sets the `object` reference to `null` using the `ref` keyword, effectively resetting the object.

---

### String Extensions

The following extension method calculates the number of words in a string:

```csharp
public static class StringExtensions
{
    public static int WordCount(this string str)
    {
        if (String.IsNullOrEmpty(str))
        {
            return 0;
        }
        return str.Split(new char[] { ' ', ',', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
```

**Explanation:**

- The `WordCount` extension method splits a string by spaces, commas, or question marks, and returns the number of words by counting non-empty entries.

---

### Advantages of Extension Methods

- **Increased readability**: You can call extension methods on existing objects, making the code more intuitive.
- **Separation of concerns**: You can add functionality to a type without modifying the original code.
- **Reusability**: Extension methods promote the reuse of code by providing common operations.

---

### Limitations of Extension Methods

- **Cannot override existing methods**: You cannot override methods in a class with extension methods. They can only add new functionality.
- **Static nature**: Extension methods are static, which means you cannot use them for instance-specific state or behavior.
- **Discovery**: Extension methods are harder to discover if not properly documented, as they are defined in separate static classes.

---

### Conclusion

Extension methods are a powerful feature in C# that allows you to add functionality to existing types without modifying their source code. By using extension methods, you can create reusable, concise, and clean code that operates on standard types and collections.

---

## LINQ

### **LINQ Operations with List and DataTable in C#**

LINQ (Language Integrated Query) is a powerful feature in C# that allows you to query collections like `List`, `Array`, `IEnumerable`, and more in a declarative manner. Below is a breakdown of various LINQ operations demonstrated with `List` and `DataTable` examples.

### **1. Inner Join**

An **Inner Join** combines data from two collections based on matching keys. It only returns items that exist in both collections.

```csharp

var innerJoin = from emp in employees
                join dep in departments
                on emp.DepartmentId equals dep.Id
                select new
                {
                    EmployeeId = emp.Id,
                    EmployeeName = emp.Name,
                    EmployeeSalary = emp.Salary,
                    Manager = emp.IsManager,
                    DepartmentName = dep.DepartmentName
                };

```

- **Output:** Matches employees to their departments and lists their details.

### **2. One-to-Many Join**

A **One-to-Many Join** returns multiple results for a single item in the "one" collection that matches multiple items in the "many" collection.

```csharp

var oneToManyResult = from department in departments
                      join employee in employees
                      on department.Id equals employee.DepartmentId
                      select new
                      {
                          DepartmentName = department.DepartmentName,
                          EmployeeName = employee.Name,
                          IsManager = employee.IsManager,
                          Salary = employee.Salary
                      };

```

- **Output:** Associates multiple employees with a single department.

### **3. Left Join (Left Outer Join)**

A **Left Join** includes all items from the left collection (employees), and those from the right collection (departments) where matches exist. If there is no match, the right collection returns a default value (`null` or custom).

```csharp

var leftJoin = from emp in employees
               join dept in departments
               on emp.DepartmentId equals dept.Id into deptGroup
               from d in deptGroup.DefaultIfEmpty()
               select new
               {
                   DepartmentName = d?.DepartmentName ?? "No Department",
                   EmployeeName = emp.Name,
               };

```

- **Output:** All employees are shown, including those without departments.

### **4. Right Join (Right Outer Join)**

A **Right Join** is similar to a Left Join, but it returns all items from the right collection (departments), even if no matching employees exist.

```csharp

var rightJoin = from dept in departments
                join emp in employees
                on dept.Id equals emp.DepartmentId into empGroup
                from e in empGroup.DefaultIfEmpty()
                select new
                {
                    DepartmentName = dept.DepartmentName,
                    EmployeeName = e?.Name ?? "No Employee"
                };

```

- **Output:** All departments are shown, including those without employees.

### **5. Full Outer Join**

A **Full Outer Join** combines both Left and Right joins, ensuring all records from both collections are included.

```csharp

var fullJoin = leftJoin.Union(rightJoin);

```

- **Output:** Includes employees without departments and departments without employees.

### **6. Cross Join**

A **Cross Join** returns all possible combinations of two collections (also known as a Cartesian product).

```csharp

var crossJoin = from emp in employees
                from dept in departments
                select new
                {
                    EmployeeName = emp.Name,
                    DepartmentName = dept.DepartmentName
                };

```

- **Output:** Every employee is shown with every department.

### **7. Ordering Join Results**

You can apply sorting (ordering) on the results of a `Join` operation using `OrderBy` and `ThenBy`.

```csharp

var orderedResult = employees.Join(departments, e => e.DepartmentId, d => d.Id,
    (emp, dept) => new
    {
        Id = emp.Id,
        Name = emp.Name,
        IsManager = emp.IsManager,
        Salary = emp.Salary,
        DepartmentId = emp.DepartmentId,
        DepartmentName = dept.DepartmentName
    }).OrderBy(o => o.DepartmentId).ThenBy(o => o.Salary);

```

- **Output:** Employees are listed by department and salary.

### **8. Group By**

`Group By` is used to group items in a collection based on a key, like grouping employees by `DepartmentId`.

```csharp

var groupResult = from emp in employees
                  group emp by emp.DepartmentId into empGroup
                  orderby empGroup.Key
                  select empGroup;

```

- **Output:** Employees are grouped by `DepartmentId`, and you can iterate through each group.

### **9. `All`, `Any`, `Contains` Operations**

- `All`: Checks if all elements in the collection satisfy a condition.
- `Any`: Checks if any element in the collection satisfies a condition.
- `Contains`: Checks if a particular element exists in the collection.

```csharp

bool allAboveThreshold = employees.All(e => e.Salary > 25000);
bool anyAboveThreshold = employees.Any(e => e.Salary > 75000);
bool employeeFound = employees.Contains(searchEmployee, new EmployeeComparer());

```

- **Output:** Shows whether all employees have a salary above a certain threshold, if any employee has a high salary, and whether a specific employee exists.

### **10. First, FirstOrDefault, Single, Last, LastOrDefault**

- `First`: Gets the first element that matches the condition.
- `FirstOrDefault`: Similar to `First`, but returns `null` if no match is found.
- `Single`: Gets exactly one element, throws an exception if there’s more than one.
- `Last`: Gets the last element that matches the condition.
- `LastOrDefault`: Similar to `Last`, but returns `null` if no match is found.

```csharp

var firstEmployee = employees.FirstOrDefault(e => e.Salary > 50000);
var singleElement = new List<int>() { 1 }.Single();
var lastElement = new List<int>() { 10, 20, 30, 40, 50 }.Last();

```

- **Output:** Demonstrates the retrieval of the first, last, single, or default element.

### **11. Concatenation**

`Concat` joins two collections together into one.

```csharp

var concatenatedList = list1.Concat(list2);

```

- **Output:** Combines two lists into one.

### **12. `SequenceEqual`**

Checks whether two collections are equal by comparing their elements.

```csharp

var isEqual = list1.SequenceEqual(list2);

```

- **Output:** Shows whether two lists are identical.

### **13. Aggregate**

`Aggregate` performs an aggregation operation on a collection, like calculating a total or concatenating strings.

```csharp

decimal totalSalary = employees.Aggregate<Employee, decimal>(0, (total, e) =>
{
    decimal bonus = (e.IsManager == "Yes") ? e.Salary * 0.04m : e.Salary * 0.02m;
    total += e.Salary + bonus;
    return total;
});

```

- **Output:** Calculates the total salary with bonuses.

### **14. DefaultIfEmpty**

Returns a default value if the collection is empty.

```csharp
var defaultIfEmptyList = emptyList.DefaultIfEmpty(9999);
```

- **Output:** Provides a default value for an empty collection.

### **15. Transformation (Where, ToList, ToDictionary)**

You can transform collections to different types using LINQ methods like `Where`, `ToList`, and `ToDictionary`.

```csharp

var employeesAboveSalary = employees.Where(e => e.Salary > 50000).ToList();
var employeeDictionary = employees.ToDictionary(e => e.Id, e => e.Name);
```

- **Output:** Filters employees by salary and converts the collection to a dictionary.

---

### How Can We Apply LINQ Operations on a DataTable and Why Can't We Use It Like a List?

### Why We Can't Use a DataTable Like a List

A `DataTable` is not inherently compatible with LINQ because it does not implement the `IEnumerable<T>` interface required for LINQ operations. LINQ queries rely on collections that support deferred execution and type-safe access, which are not features of a `DataTable`. To make a `DataTable` LINQ-compatible, it needs to be converted into an enumerable collection of `DataRow` objects.

---

### Applying LINQ Operations on a DataTable

To use LINQ on a `DataTable`, you must first convert it into an enumerable collection. This is achieved using the `AsEnumerable()` method, which is part of the `System.Data.DataTableExtensions` namespace. Below are the key steps and methods involved:

---

### 1. **Converting DataTable to Enumerable (AsEnumerable)**

The `AsEnumerable()` method converts a `DataTable` into an `IEnumerable<DataRow>` so that LINQ queries can be applied.

```
var result = from row in employeeTable.AsEnumerable()
             where row.Field<int>("Salary") > 50000
             select row;
```

**Why Use `AsEnumerable()`:**

- Enables LINQ compatibility by treating the `DataTable` as a collection of `DataRow` objects.
- Makes the `DataTable` queryable using LINQ methods like `Where`, `Select`, `GroupBy`, etc.

---

### 2. **Accessing Columns with `Field<T>`**

Once the `DataTable` is converted to an enumerable collection, you can access column values using the strongly-typed `Field<T>` extension method.

```
var result = from emp in employeeTable.AsEnumerable()
             select new
             {
                 Name = emp.Field<string>("Name"),
                 Salary = emp.Field<decimal>("Salary")
             };
```

**Why Use `Field<T>`:**

- Provides type safety when accessing column values.
- Prevents runtime errors by ensuring the correct type is retrieved.

---

### 3. **Handling NULLs with `IsNull`**

`DataTable` columns can contain `DBNull` values, which need to be handled explicitly to avoid exceptions.

```
int? departmentId = row.IsNull("DepartmentId") ? (int?)null : row.Field<int>("DepartmentId");
```

**Why Handle NULLs Explicitly:**

- `DBNull` values cannot be directly used in LINQ queries.
- Ensures robust code that handles nullable database fields gracefully.

---

### 4. **Joining Tables**

LINQ supports joining multiple `DataTable` objects. Both tables must be converted to `IEnumerable<DataRow>` using `AsEnumerable()`.

```
var leftJoin = from emp in employeeTable.AsEnumerable()
               join dept in departmentTable.AsEnumerable()
               on emp.Field<int>("DepartmentId") equals dept.Field<int>("Id") into deptGroup
               from d in deptGroup.DefaultIfEmpty()
               select new
               {
                   EmployeeName = emp.Field<string>("Name"),
                   DepartmentName = d?.Field<string>("DepartmentName") ?? "No Department"
               };
```

**Key Points:**

- Use `DefaultIfEmpty()` for left joins to include unmatched rows.
- Handle nulls to avoid errors when there are no matches.

---

### 5. **Grouping Results**

You can group rows based on column values, similar to SQL `GROUP BY`.

```
var grouped = from emp in employeeTable.AsEnumerable()
              group emp by emp.Field<int>("DepartmentId") into deptGroup
              select deptGroup;
```

**Why Grouping is Useful:**

- Organizes data for aggregation or reporting.
- Makes it easy to apply aggregate functions like `Sum`, `Average`, etc.

---

### 6. **Aggregates, Concat, Union**

LINQ supports operations like summing salaries, concatenating tables, and finding unions.

```
decimal totalSalary = employeeTable.AsEnumerable().Sum(row => row.Field<decimal>("Salary"));
```

**Key Benefits:**

- Simplifies aggregate calculations.
- Provides declarative syntax for complex operations.

---

### Performance Considerations

While LINQ adds flexibility and readability, it may be slower than using traditional methods like `DataTable.Select()` for large datasets. This is because LINQ queries operate in memory and involve type conversion.

**When to Use LINQ with DataTable:**

- For small to medium datasets where code readability and maintainability are priorities.
- When complex operations like joins, grouping, and projections are required.

---

## Security and Cryptography in C#

Security and cryptography are essential aspects of modern applications to ensure data confidentiality, integrity, and authenticity. In C#, the .NET framework provides robust libraries for implementing cryptographic techniques, including encryption, hashing, and password management.

---

### **Symmetric Encryption with AES**

Symmetric encryption is a method where the same key is used for both encryption and decryption. Below is an example using AES (Advanced Encryption Standard) in C#.

### **Code Example: Symmetric Encryption**

```
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    public class SymmetricEncryptionExample
    {
        public string GetKey()
        {
            string key;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateKey(); // Generate a random key
                key = Convert.ToBase64String(aesAlg.Key); // Convert key to Base64 string
            }
            return key;
        }

        public string GetIV()
        {
            string iv;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV(); // Generate a random IV
                iv = Convert.ToBase64String(aesAlg.IV); // Convert IV to Base64 string
            }
            return iv;
        }

        public string Encrypt(string plainText, string key, string iv)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(csEncrypt))
                        {
                            sw.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText, string key, string iv)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
```

### **Explanation**

- **Key**: A random, secret key used for encryption and decryption.
- **IV (Initialization Vector)**: Ensures that the same plaintext encrypted multiple times results in different ciphertexts.
- **Encrypt Method**: Converts plaintext into ciphertext.
- **Decrypt Method**: Converts ciphertext back to plaintext.

---

## **Hashing with SHA256**

Hashing is a one-way cryptographic function, commonly used to store passwords securely. Unlike encryption, hashing is irreversible.

### **Code Example: Hashing**

```
using System;
using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    public class HashingExample
    {
        public string ComputeHash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
```

### **Explanation**

- **SHA256**: A secure hashing algorithm that produces a 256-bit hash.
- **Use Case**: Storing passwords securely by hashing them before saving to a database.
- **Irreversible**: Hashing cannot be reversed, providing an added layer of security.

---

## **Password Management with Salting**

Salting is the process of adding random data to a password before hashing to prevent attacks like rainbow table attacks.

### **Code Example: Salting and Verifying Passwords**

```
using System;
using System.Security.Cryptography;
using System.Text;

namespace Advance_Of_C_.Security_And_CryptoGraphy
{
    public class PasswordExample
    {
        public byte[] GenerateSalt(int size = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[size];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public string HashedPassword(string password, byte[] salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, passwordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, salt.Length, passwordBytes.Length);

                byte[] hashBytes = sha256.ComputeHash(passwordWithSalt);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public bool VerifyPassword(string inputPassword, byte[] storedSalt, string storedHashedPassword)
        {
            string hashedInputPassword = HashedPassword(inputPassword, storedSalt);
            return hashedInputPassword == storedHashedPassword;
        }
    }
}
```

### **Explanation**

- **Salt**: A unique random value added to the password.
- **Hashed Password**: Combines the password and salt before hashing to produce a unique hash.
- **Verification**: Compares the hashed input password with the stored hashed password.

---

## **Key Concepts**

1. **Symmetric Encryption**: Uses the same key for encryption and decryption (e.g., AES).
2. **Hashing**: A one-way cryptographic operation for secure password storage (e.g., SHA256).
3. **Salting**: Adds randomness to passwords to defend against rainbow table attacks.
4. **Key and IV**: Essential components in symmetric encryption for securing data.
5. **Password Verification**: Ensures that user inputs match stored, salted, and hashed passwords.

---

This comprehensive guide demonstrates the practical implementation of cryptographic techniques in C#. These techniques ensure robust application security, making it harder for attackers to compromise sensitive data.

---