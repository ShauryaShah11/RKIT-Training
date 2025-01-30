# Basic Of C#

## **Table of Contents**

1. [**Introduction to C#**](#introduction-to-c)
2. [**Create Your First C# Program ‘Hello World’**](#create-your-first-c-program-hello-world)
3. [**Understanding C# Program Structure**](#understanding-c-program-structure)
4. [**Working with Code Files, Projects & Solutions**](#working-with-code-files-projects-solutions)
5. [**Datatypes & Variables with Conversion**](#datatypes-variables-with-conversion)
6. [**Operators & Expressions**](#operators-expressions)
7. [**Statements**](#statements)
8. [**Understanding Arrays**](#understanding-arrays)
9. [**Define & Calling of Methods**](#define-calling-of-methods)
10. [**Understanding Classes & OOP Concepts**](#understanding-classes-oop-concepts)
11. [**Interface & Inheritance**](#interface-inheritance)
12. [**Scope & Accessibility Modifiers**](#scope-accessibility-modifiers)
13. [**Namespace & .NET Library**](#namespace-net-library)
14. [**Creating & Adding Reference to Assemblies**](#creating-adding-reference-to-assemblies)
15. [**Working with Collections**](#working-with-collections)
16. [**Enumerations**](#enumerations)
17. [**Data Table**](#data-table)
18. [**Exception Handling**](#exception-handling)
19. [**Different Project Types**](#different-project-types)
20. [**Working with String Class**](#working-with-string-class)
21. [**Working with DateTime Class**](#working-with-datetime-class)
22. [**Basic File Operations**](#basic-file-operations)

---

## **Introduction to C#**

### **Overview**

C# is an object-oriented programming language developed by Microsoft as part of the .NET framework. It is used for building a wide variety of applications, including desktop software, web applications, mobile apps, and cloud-based services. C# integrates well with the .NET framework, allowing for seamless development of modern applications.

### **Key Features**

- **Object-Oriented**: Supports concepts like classes, inheritance, polymorphism, and encapsulation.
- **Platform-Independent**: Runs on various platforms with the help of .NET Core (now .NET 5+).
- **Type-Safe**: Ensures that variables are only used in appropriate ways.
- **Garbage-Collected**: Automatically manages memory.

---

## **Create Your First C# Program ‘Hello World’**

Creating a basic "Hello World" program is the first step in learning a new programming language. This program demonstrates the syntax and structure of a C# application.

### **Code Example**:

```csharp
using System;  // Using the System namespace for basic input/output

namespace HelloWorld  // Declaring the namespace
{
    class Program  // Declaring the Program class
    {
        static void Main(string[] args)  // Main method is the entry point
        {
            Console.WriteLine("Hello, World!");  // Outputting "Hello, World!" to console
        }
    }
}

```

### **Explanation**:

- `using System;` imports the standard library for basic functionalities like console input/output.
- `namespace HelloWorld` is a way to group related code.
- The `Main` method is the entry point where the program starts execution.
- `Console.WriteLine("Hello, World!");` prints the text to the console.

---

## **Understanding C# Program Structure**

C# programs have a specific structure, composed of namespaces, classes, methods, and statements.

### **Structure Breakdown**:

```csharp
using System;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Code execution starts here
        }
    }
}
```

- **Namespace**: Used to organize classes and avoid name conflicts.
- **Class**: A blueprint for creating objects (instances of the class).
- **Main Method**: The entry point of a C# application.

---

## **Working with Code Files, Projects & Solutions**

### **Code Files (.cs)**:

A `.cs` file is where your C# code resides. You can have multiple `.cs` files within a project.

### **Projects**:

A project contains code files and configuration files. A project is compiled into a .NET application (either an executable or a library).

### **Solutions**:

A solution is a container for one or more projects. A solution can manage dependencies, configurations, and settings across projects.

---

## **Datatypes & Variables with Conversion**

### **Common Datatypes**:

- **Primitive Types**:
    - `int`: Integer (whole numbers)
    - `double`: Floating-point numbers
    - `bool`: True/False
    - `char`: Single characters
    - `string`: Sequence of characters

### **Variable Declaration**:

```csharp
int number = 10;
double pi = 3.14;
bool isActive = true;
char grade = 'A';
string name = "John";
```

### **Type Conversion**:

C# supports both implicit and explicit type conversions. Implicit conversion happens automatically, while explicit requires a cast.

```csharp
double pi = 3.14;
int piInt = (int)pi;  // Explicit conversion
```

---

## **Operators & Expressions**

### **Arithmetic Operators**:

- `+`, ``, ``, `/`, `%` (addition, subtraction, multiplication, division, modulus)

### **Comparison Operators**:

- `==`, `!=`, `<`, `>`, `<=`, `>=` (check equality, inequality, and ordering)

### **Logical Operators**:

- `&&` (AND), `||` (OR), `!` (NOT)

### **Expression Example**:

```csharp
int x = 10, y = 5;
int result = (x + y) * 2;  // result is 30
```

---

## **Statements**

Statements are instructions that the program executes. They can be conditionals, loops, method calls, and more. Let's explore different types of statements in C#:

### **Conditional Statements**

Conditional statements allow the program to make decisions and execute certain blocks of code based on whether a condition is true or false.

### **`if` Statement**

The `if` statement evaluates a condition, and if the condition is true, the block of code inside the `if` is executed.

```csharp
if (x > y)
{
    Console.WriteLine("x is greater than y");
}
```

### **`else` Statement**

The `else` statement is used in conjunction with `if`. It executes when the condition in the `if` is false.

```csharp
if (x > y)
{
    Console.WriteLine("x is greater than y");
}
else
{
    Console.WriteLine("y is greater than x");
}

```

### **`else if` Statement**

You can chain multiple conditions using `else if` to check for different possibilities.

```csharp

if (x > y)
{
    Console.WriteLine("x is greater than y");
}
else if (x == y)
{
    Console.WriteLine("x is equal to y");
}
else
{
    Console.WriteLine("y is greater than x");
}
```

---

### **Loops**

Loops allow repeated execution of a block of code as long as a certain condition is met.

### **`for` Loop**

The `for` loop is ideal when you know beforehand how many times you want to iterate.

```csharp
for (int i = 0; i < 10; i++)  // Loops from 0 to 9
{
    Console.WriteLine(i);  // Prints numbers 0 to 9
}
```

### **`while` Loop**

The `while` loop continues as long as the condition remains true. It checks the condition before each iteration.

```csharp
int i = 0;
while (i < 10)  // Loops while i is less than 10
{
    Console.WriteLine(i);  // Prints numbers 0 to 9
    i++;
}
```

### **`do-while` Loop**

The `do-while` loop is similar to `while`, but it guarantees at least one iteration, as it checks the condition after executing the code.

```csharp
int i = 0;
do
{
    Console.WriteLine(i);  // Prints numbers 0 to 9
    i++;
} while (i < 10);
```

---

### **Jump Statements**

Jump statements alter the flow of execution by breaking out of loops, continuing to the next iteration, or returning from methods.

### **`break` Statement**

The `break` statement exits a loop or switch statement immediately.

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 5)
    {
        break;  // Exit the loop when i equals 5
    }
    Console.WriteLine(i);
}
```

### **`continue` Statement**

The `continue` statement skips the current iteration of a loop and moves on to the next iteration.

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 5)
    {
        continue;  // Skip the iteration when i equals 5
    }
    Console.WriteLine(i);
}
```

### **`return` Statement**

The `return` statement exits from a method and optionally returns a value.

```csharp
int AddNumbers(int a, int b)
{
    return a + b;  // Returns the sum of a and b
}
```

---

### **Switch Statement**

The `switch` statement evaluates an expression against multiple possible values, making it a cleaner alternative to multiple `if-else` statements.

```csharp
int day = 3;
switch (day)
{
    case 1:
        Console.WriteLine("Monday");
        break;
    case 2:
        Console.WriteLine("Tuesday");
        break;
    case 3:
        Console.WriteLine("Wednesday");
        break;
    default:
        Console.WriteLine("Invalid day");
        break;
}
```

---

### **Exception Handling Statements**

Exception handling ensures that your program can gracefully handle errors that might occur during runtime.

### **`try-catch` Statement**

The `try-catch` statement is used to catch exceptions (errors) that occur inside the `try` block and handle them in the `catch` block.

```csharp
try
{
    int result = 10 / 0;  // Will throw an exception (divide by zero)
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("Error: " + ex.Message);  // Handle the exception
}
```

### **`finally` Block**

The `finally` block is optional and always runs after the `try` and `catch` blocks, regardless of whether an exception was thrown or not. It is typically used to clean up resources (like closing files or database connections).

```csharp
try
{
    int result = 10 / 2;
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("This will always execute.");
}
```

---

### **Foreach Statement**

The `foreach` loop is specifically designed to iterate over collections (like arrays or lists). It simplifies the code by automatically handling the indexing for you.

```csharp
string[] colors = { "Red", "Green", "Blue" };
foreach (string color in colors)
{
    Console.WriteLine(color);  // Prints "Red", "Green", and "Blue"
}
```

---

### **Lambda Expressions (Anonymous Methods)**

Lambda expressions are a shorthand for defining anonymous methods. They are commonly used in LINQ queries and event handling.

```csharp
Func<int, int, int> add = (x, y) => x + y;  // Lambda expression for adding two numbers
Console.WriteLine(add(3, 4));  // Output: 7
```

---

### **Expression Statements**

Expression statements are statements that consist of an expression followed by a semicolon. They are typically used for operations like assignments, method calls, or increments.

```csharp
int x = 10;  // Assignment statement
x++;         // Increment statement
Console.WriteLine(x);  // Method call statement

```

---

## **Understanding Arrays**

An array is a collection of variables of the same type. It is a fixed-size collection, meaning the number of elements is defined when the array is created.

### **Array Example**:

```csharp
int[] numbers = { 1, 2, 3, 4, 5 };
Console.WriteLine(numbers[2]);  // Output: 3
```

Arrays are zero-indexed, meaning the first element is at index `0`.

---

## **Define & Calling of Methods**

Methods are blocks of reusable code that perform specific tasks.

### **Method Definition**:

```csharp
public void SayHello()
{
    Console.WriteLine("Hello!");
}
```

### **Method Calling**:

```csharp
SayHello();  // Calling the SayHello method
```

Methods can have parameters and return values:

```csharp
public int AddNumbers(int a, int b)
{
    return a + b;
}
```

---

## **Understanding Classes & OOP Concepts**

### **Classes**

A **class** is a blueprint or prototype from which objects are created. It can contain:

- **Fields** (variables that hold data).
- **Methods** (functions that define the behaviors or actions).
- **Properties** (attributes that represent the state of an object).

Classes enable **Object-Oriented Programming (OOP)**, which provides a clear structure for software development.

```csharp
class Car
{
    // Field
    public string model;

    // Constructor
    public Car(string model)
    {
        this.model = model;
    }

    // Method
    public void StartEngine()
    {
        Console.WriteLine($"{model} engine started");
    }
}
```

In the above example, the `Car` class defines a field `model`, a constructor to initialize the model, and a method `StartEngine()` that defines a behavior for starting the car engine.

### **OOP Principles**

### **Encapsulation**

Encapsulation is the concept of bundling data (attributes) and methods (functions) that operate on the data into a single unit or class. This ensures that the internal state of an object is protected from outside interference and misuse.

```csharp
class Account
{
    // Private field
    private double balance;

    // Public method to access the balance
    public double GetBalance()
    {
        return balance;
    }

    // Public method to modify the balance
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount;
        }
    }
}
```

In the `Account` class, the balance is a private field, meaning it cannot be accessed directly from outside the class. Instead, access is provided via methods like `GetBalance()` and `Deposit()`.

### **Inheritance**

Inheritance is the mechanism by which one class can derive from another, inheriting its fields and methods. The derived class can also add its own fields and methods or override inherited ones.

```csharp
class Animal
{
    public void Speak()
    {
        Console.WriteLine("Animal speaks");
    }
}

class Dog : Animal  // Inherits from Animal
{
    public new void Speak()  // Polymorphism in action
    {
        Console.WriteLine("Dog barks");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Dog myDog = new Dog();
        myDog.Speak();  // Output: Dog barks
    }
}
```

Here, `Dog` inherits the `Speak` method from `Animal`, but it provides its own implementation (overrides the base class method). This is a classic example of **polymorphism**.

### **Polymorphism**

Polymorphism allows methods to behave differently based on the object that calls them. This means a subclass can provide a specific implementation of a method that is already defined in its superclass.

```csharp
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal speaks");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Dog barks");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Animal animal = new Dog();
        animal.Speak();  // Output: Dog barks (Polymorphism)
    }
}
```

In this case, even though `animal` is of type `Animal`, it calls the `Speak()` method from the `Dog` class because `Dog` overrides the method. This demonstrates **runtime polymorphism**.

### **Abstraction**

Abstraction is the concept of hiding the complex implementation details and showing only the essential features of an object. It helps in reducing complexity and focusing on high-level functionalities.

```csharp
abstract class Animal
{
    public abstract void Speak();  // Abstract method (no implementation)
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Woof!");
    }
}
```

Here, `Animal` is an abstract class with an abstract method `Speak()`. The `Dog` class provides the implementation for the `Speak` method. You cannot instantiate an abstract class directly, but you can create objects of derived classes like `Dog`.

---

## **Interface & Inheritance**

### **Interfaces**

An **interface** defines a contract that classes must adhere to. It can declare methods and properties, but it does not contain any implementation. Classes that implement the interface must provide their own implementations for the methods defined in the interface.

```csharp
interface IAnimal
{
    void Speak();
}

class Dog : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Woof!");
    }
}

class Cat : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Meow!");
    }
}
```

In this example, both `Dog` and `Cat` implement the `IAnimal` interface, meaning they must provide their own implementation of the `Speak()` method.

### **Inheritance**

Inheritance allows a class to inherit members (fields, methods) from another class. It promotes code reuse and helps to create a hierarchy of classes.

```csharp
class Animal
{
    public void Eat()
    {
        Console.WriteLine("Eating...");
    }
}

class Bird : Animal  // Bird inherits from Animal
{
    public void Fly()
    {
        Console.WriteLine("Flying...");
    }
}
```

Here, `Bird` inherits the `Eat()` method from the `Animal` class, and it can also add its own method, `Fly()`.

---

### **Abstract Classes vs Interfaces**

- **Abstract Classes**: Can contain both implemented and unimplemented methods (abstract methods). Useful when you want to provide a common base class with some common implementation.
- **Interfaces**: Only declare method signatures. Classes must implement all methods declared in the interface. Useful when you want to define a contract without imposing any common implementation.

---

---

## **Scope & Accessibility Modifiers**

### **Common Modifiers**:

- **public**: Accessible from anywhere.
- **private**: Accessible only within the class.
- **protected**: Accessible within the class and derived classes.
- **internal**: Accessible within the same assembly.

```csharp
class MyClass
{
    private int myPrivateVar;
    public int myPublicVar;
}
```

---

## **Namespace & .NET Library**

### **Namespaces**:

Namespaces help organize code and avoid name conflicts.

```csharp
namespace MyApplication
{
    class Program
    {
        // code
    }
}
```

The **.NET Library** is a collection of pre-built classes that handle common tasks like file I/O, networking, and UI development.

---

## **Creating & Adding Reference to Assemblies**

Assemblies are compiled libraries used by C# programs. You can add references to external libraries or projects.

### **Example**:

- Right-click in **Solution Explorer** > Add Reference > Choose your library.

---

## **Working with Collections**

In C#, collections are powerful data structures that allow you to store and manage dynamic data efficiently. Collections provide flexibility over arrays as they can grow and shrink in size as required. The main types of collections are `List`, `Dictionary`, `Queue`, and `Stack`. They are part of the `System.Collections` or `System.Collections.Generic` namespaces.

### **List Example**

A `List<T>` is a collection of objects that can be accessed by index, and it provides methods to add, remove, or search elements.

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();

        // Adding elements to the list
        numbers.Add(10);
        numbers.Add(20);
        numbers.Add(30);

        // Accessing elements by index
        Console.WriteLine("First number: " + numbers[0]);

        // Iterating through the list
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

        // Removing an element
        numbers.Remove(20);  // Removes the first occurrence of 20

        Console.WriteLine("After removal:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
```

**Key operations with `List<T>`:**

- `Add()`: Adds an element to the end of the list.
- `Remove()`: Removes the first occurrence of a specific element.
- `Count`: Gets the number of elements in the list.
- `Contains()`: Checks if an element exists in the list.

---

### **Dictionary Example**

A `Dictionary<TKey, TValue>` is a collection of key-value pairs. It allows you to store data where each value is associated with a unique key.

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, int> phoneBook = new Dictionary<string, int>();

        // Adding elements to the dictionary
        phoneBook.Add("Alice", 123456789);
        phoneBook.Add("Bob", 987654321);

        // Accessing elements by key
        Console.WriteLine("Alice's number: " + phoneBook["Alice"]);

        // Iterating through the dictionary
        foreach (var entry in phoneBook)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

        // Checking if a key exists
        if (phoneBook.ContainsKey("Charlie"))
        {
            Console.WriteLine("Charlie's number: " + phoneBook["Charlie"]);
        }
        else
        {
            Console.WriteLine("Charlie not found.");
        }
    }
}
```

**Key operations with `Dictionary<TKey, TValue>`:**

- `Add()`: Adds a new key-value pair to the dictionary.
- `Remove()`: Removes the key-value pair associated with a specific key.
- `ContainsKey()`: Checks if a key exists in the dictionary.
- `TryGetValue()`: Attempts to get the value associated with a specific key.

---

### **Queue Example**

A `Queue<T>` represents a first-in, first-out (FIFO) collection. It is ideal for scenarios where you need to process items in the order they were added (e.g., tasks in a task scheduler).

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Queue<string> taskQueue = new Queue<string>();

        // Adding tasks to the queue
        taskQueue.Enqueue("Task 1");
        taskQueue.Enqueue("Task 2");
        taskQueue.Enqueue("Task 3");

        // Processing tasks in order
        Console.WriteLine("Processing task: " + taskQueue.Dequeue());
        Console.WriteLine("Processing task: " + taskQueue.Dequeue());

        // Checking the next task without dequeuing
        Console.WriteLine("Next task to process: " + taskQueue.Peek());
    }
}
```

**Key operations with `Queue<T>`:**

- `Enqueue()`: Adds an element to the end of the queue.
- `Dequeue()`: Removes and returns the element at the front of the queue.
- `Peek()`: Returns the element at the front without removing it.

---

### **Stack Example**

A `Stack<T>` represents a last-in, first-out (LIFO) collection. It is useful for scenarios where you need to process items in reverse order, such as undo functionality in applications.

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Stack<string> callStack = new Stack<string>();

        // Pushing items onto the stack
        callStack.Push("Function 1");
        callStack.Push("Function 2");
        callStack.Push("Function 3");

        // Popping items from the stack (last in, first out)
        Console.WriteLine("Returning from: " + callStack.Pop());
        Console.WriteLine("Returning from: " + callStack.Pop());

        // Peeking at the top of the stack
        Console.WriteLine("Next function to return from: " + callStack.Peek());
    }
}
```

**Key operations with `Stack<T>`:**

- `Push()`: Adds an element to the top of the stack.
- `Pop()`: Removes and returns the element from the top of the stack.
- `Peek()`: Returns the element at the top without removing it.

---

### **Working with Generic Collections**

The collections shown above, such as `List<T>`, `Dictionary<TKey, TValue>`, `Queue<T>`, and `Stack<T>`, are **generic collections**. Generics provide type safety by allowing you to specify the type of elements in the collection. This ensures that only elements of the specified type can be added to the collection, reducing runtime errors and improving code clarity.

---

### **Other Collections in C#**

1. **LinkedList<T>**: A collection that allows elements to be inserted or removed at both ends with constant time complexity.
2. **SortedList<TKey, TValue>**: A collection of key-value pairs sorted by the key.
3. **HashSet<T>**: A collection that stores unique elements and provides fast lookups.
4. **Concurrent Collections**: Collections like `ConcurrentQueue<T>`, `ConcurrentDictionary<TKey, TValue>`, and `BlockingCollection<T>` are thread-safe and can be used in multi-threaded environments.

---

### **When to Use Collections**

- **List<T>**: When you need a dynamically sized collection of elements and need to perform fast lookups by index.
- **Dictionary<TKey, TValue>**: When you need to store key-value pairs and need fast lookups by key.
- **Queue<T>**: When you need a FIFO (first-in, first-out) processing order.
- **Stack<T>**: When you need a LIFO (last-in, first-out) processing order.

---

## **Enumerations**

An **enumeration (enum)** in C# is a user-defined type that consists of a set of named integer constants. Enums are useful for representing related constant values like days, states, or options.

### **Basic Enumeration Example**

```csharp
enum Day
{
    Sunday,    // 0
    Monday,    // 1
    Tuesday,   // 2
    Wednesday, // 3
    Thursday,  // 4
    Friday,    // 5
    Saturday   // 6
}

class Program
{
    static void Main()
    {
        Day today = Day.Monday;
        Console.WriteLine(today); // Output: Monday
    }
}
```

### **Flags Attribute**

The `[Flags]` attribute allows an enum to be used as a bit field, enabling multiple values to be combined with bitwise operations.

```csharp
[Flags]
enum Permissions
{
    None = 0,      // 0000
    Read = 1,      // 0001
    Write = 2,     // 0010
    Execute = 4,   // 0100
    Delete = 8     // 1000
}
```

### **Using Flags Enum**

Combine values using `|`, and check them using the `&` operator.

```csharp
class Program
{
    static void Main()
    {
        Permissions userPermissions = Permissions.Read | Permissions.Write;
        Console.WriteLine(userPermissions); // Output: Read, Write

        if ((userPermissions & Permissions.Read) == Permissions.Read)
            Console.WriteLine("User has Read permission");
    }
}
```

### **Checking and Modifying Flags**

You can check, add, or remove specific flags:

```csharp
class Program
{
    static void Main()
    {
        Permissions userPermissions = Permissions.Read | Permissions.Write;

        // Remove Execute permission
        userPermissions &= ~Permissions.Execute;
        Console.WriteLine(userPermissions); // Output: Read, Write
    }
}
```

Use `HasFlag()` to check for flags:

```csharp
Console.WriteLine(userPermissions.HasFlag(Permissions.Read)); // True
```

### **Best Practices**

- **Powers of Two**: Ensure each value is a power of two.
- **None Value**: Always include a `None` or `Default` value for zero flags.

### **Example: System Features Enum**

```csharp
[Flags]
enum SystemFeatures
{
    None = 0,
    Authentication = 1,
    Logging = 2,
    DataStorage = 4,
    Backup = 8,
    Notification = 16
}

class Program
{
    static void Main()
    {
        SystemFeatures selectedFeatures = SystemFeatures.Authentication | SystemFeatures.Logging;
        Console.WriteLine(selectedFeatures); // Output: Authentication, Logging
    }
}
```

---

### **Conclusion**

The **Flags** attribute in C# allows for efficient management of multiple options or states using bitwise operations. This makes it easy to combine, check, and manipulate flags within a single variable.

---

## **DataTable**

A `DataTable` is an in-memory representation of a table of data. It allows you to store, manipulate, and query data in a structured format, similar to a table in a database.

### **Creating a DataTable**

```csharp
DataTable table = new DataTable("Employees");

table.Columns.Add("ID", typeof(int));
table.Columns.Add("Name", typeof(string));
table.Columns.Add("Age", typeof(int));

table.Rows.Add(1, "John Doe", 28);
table.Rows.Add(2, "Jane Smith", 34);
```

### **Adding Constraints**

Constraints ensure data integrity within the `DataTable`. You can add primary key, unique, or foreign key constraints.

### **Primary Key Constraint**

A primary key uniquely identifies each row in the table.

```csharp
table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };
```

### **Unique Constraint**

Ensures that all values in a column are unique.

```csharp
table.Constraints.Add(new UniqueConstraint("UniqueName", table.Columns["Name"]));
```

### **Foreign Key Constraint**

Establishes a relationship between tables, ensuring that values in a column correspond to values in another table.

```csharp
DataTable departmentTable = new DataTable("Departments");
departmentTable.Columns.Add("DeptID", typeof(int));
departmentTable.Columns.Add("DeptName", typeof(string));

departmentTable.Rows.Add(1, "HR");
departmentTable.Rows.Add(2, "IT");

ForeignKeyConstraint fkConstraint = new ForeignKeyConstraint("DeptFK", departmentTable.Columns["DeptID"], table.Columns["ID"]);
table.Constraints.Add(fkConstraint);
```

### **Working with DataRows**

```csharp
DataRow row = table.NewRow();
row["ID"] = 3;
row["Name"] = "Alice Green";
row["Age"] = 27;
table.Rows.Add(row);
```

### **Filtering Data**

Use the `Select()` method to filter rows.

```csharp
DataRow[] result = table.Select("Age > 30");
foreach (DataRow r in result)
{
    Console.WriteLine(r["Name"]);
}
```

---

### **DataSet**

A `DataSet` is a collection of `DataTable` objects that can be used to work with multiple tables in memory. It can also contain relationships between tables.

### **Creating a DataSet**

```csharp
DataSet dataSet = new DataSet();

DataTable table1 = new DataTable("Employees");
table1.Columns.Add("ID", typeof(int));
table1.Columns.Add("Name", typeof(string));

DataTable table2 = new DataTable("Departments");
table2.Columns.Add("DeptID", typeof(int));
table2.Columns.Add("DeptName", typeof(string));

dataSet.Tables.Add(table1);
dataSet.Tables.Add(table2);
```

### **Setting Relationships in a DataSet**

You can create relationships between tables in a `DataSet` by using `DataRelation`.

```csharp
DataRelation relation = new DataRelation(
    "EmployeeDepartment",
    dataSet.Tables["Departments"].Columns["DeptID"],
    dataSet.Tables["Employees"].Columns["ID"]);

dataSet.Relations.Add(relation);
```

### **Accessing Data in a DataSet**

To access related data, you can loop through the child rows of a parent row.

```csharp
foreach (DataRow departmentRow in dataSet.Tables["Departments"].Rows)
{
    Console.WriteLine($"Department: {departmentRow["DeptName"]}");

    foreach (DataRow employeeRow in departmentRow.GetChildRows("EmployeeDepartment"))
    {
        Console.WriteLine($"Employee: {employeeRow["Name"]}");
    }
}
```

---

### **Conclusion**

- **DataTable** is ideal for storing and manipulating tabular data.
- **Constraints** like primary keys, unique, and foreign key constraints help enforce data integrity.
- **DataSet** is useful for working with multiple related `DataTable` objects in memory.

---

## **Exception Handling**

Exception handling allows you to deal with runtime errors gracefully using `try`, `catch`, and `finally`.

### **Example**:

```csharp
try
{
    int result = 10 / 0;
}
catch (DivideByZeroException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("Execution finished.");
}
```

---

## **Different Project Types**

C# supports different types of projects, including:

- **Console Application**: For CLI-based programs.
- **Windows Forms**: For GUI-based applications.
- **Web Applications**: Using ASP.NET for building web apps.
- **Class Libraries**: Reusable code that can be shared across projects.

---

## **Working with String Class**

The `String` class in C# provides a wide range of methods for string manipulation, including methods for comparing, searching, formatting, and modifying strings.

### **Common String Methods**

- **Length**
    
    Returns the length of the string.
    
    ```csharp
    string str = "Hello, World!";
    int length = str.Length;  // 13
    ```
    
- **Substring()**
    
    Extracts a substring from the string.
    
    ```csharp
    string str = "Hello, World!";
    string sub = str.Substring(7, 5);  // "World"
    ```
    
- **IndexOf()**
    
    Finds the index of the first occurrence of a specified character or substring.
    
    ```csharp
    string str = "Hello, World!";
    int index = str.IndexOf("World");  // 7
    ```
    
- **Replace()**
    
    Replaces all occurrences of a substring with another substring.
    
    ```csharp
    string str = "Hello, World!";
    string newStr = str.Replace("World", "C#");  // "Hello, C#"
    ```
    
- **ToLower()**
    
    Converts the string to lowercase.
    
    ```csharp
    string str = "HELLO, WORLD!";
    string lower = str.ToLower();  // "hello, world!"
    ```
    
- **ToUpper()**
    
    Converts the string to uppercase.
    
    ```csharp
    string str = "hello, world!";
    string upper = str.ToUpper();  // "HELLO, WORLD!"
    ```
    
- **Trim()**
    
    Removes all leading and trailing white-space characters from the string.
    
    ```csharp
    string str = "  Hello, World!  ";
    string trimmed = str.Trim();  // "Hello, World!"
    ```
    
- **StartsWith()**
    
    Checks if the string starts with a specified substring.
    
    ```csharp
    string str = "Hello, World!";
    bool starts = str.StartsWith("Hello");  // true
    ```
    
- **EndsWith()**
    
    Checks if the string ends with a specified substring.
    
    ```csharp
    string str = "Hello, World!";
    bool ends = str.EndsWith("World!");  // true
    ```
    
- **Split()**
    
    Splits the string into an array of substrings based on a delimiter.
    
    ```csharp
    string str = "apple,banana,grape";
    string[] fruits = str.Split(',');  // ["apple", "banana", "grape"]
    
    ```
    
- **Concat()**
    
    Concatenates two or more strings.
    
    ```csharp
    string str1 = "Hello, ";
    string str2 = "World!";
    string result = string.Concat(str1, str2);  // "Hello, World!"
    ```
    
- **Contains()**
    
    Checks if a substring exists within the string.
    
    ```csharp
    string str = "Hello, World!";
    bool contains = str.Contains("World");  // true
    ```
    
- **CompareTo()**
    
    Compares two strings lexicographically.
    
    ```csharp
    string str1 = "apple";
    string str2 = "banana";
    int comparison = str1.CompareTo(str2);  // Returns a negative number
    ```
    
- **PadLeft() / PadRight()**
    
    Pads the string with spaces or a specified character to a certain length from the left or right.
    
    ```csharp
    string str = "42";
    string paddedLeft = str.PadLeft(5, '0');  // "00042"
    string paddedRight = str.PadRight(5, '0');  // "42000"
    ```
    
- **Insert()**
    
    Inserts a substring at a specified index.
    
    ```csharp
    string str = "Hello World";
    string result = str.Insert(6, "Beautiful ");  // "Hello Beautiful World"
    ```
    
- **Remove()**
    
    Removes a specified number of characters from the string starting at a given index.
    
    ```csharp
    string str = "Hello, World!";
    string result = str.Remove(5, 7);  // "Hello!"
    ```
    
- **ToCharArray()**
    
    Converts the string to an array of characters.
    
    ```csharp
    string str = "Hello";
    char[] charArray = str.ToCharArray();  // ['H', 'e', 'l', 'l', 'o']
    ```
    

### **Conclusion**

The `String` class provides powerful methods for string manipulation, making it easy to perform common tasks such as searching, replacing, formatting, and modifying strings.

---

## **Working with DateTime Class**

The `DateTime` class in C# offers methods for handling dates and times, allowing you to perform operations such as date comparisons, formatting, and manipulation.

### **Common DateTime Methods**

- **Now**
    
    Gets the current date and time.
    
    ```csharp
    
    DateTime now = DateTime.Now;  // Current date and time
    ```
    
- **Today**
    
    Gets the current date with the time set to 00:00:00.
    
    ```csharp
    
    DateTime today = DateTime.Today;  // Current date at midnight
    ```
    
- **AddDays()**
    
    Adds a specified number of days to the current date.
    
    ```csharp
    
    DateTime date = DateTime.Now;
    DateTime newDate = date.AddDays(5);  // Adds 5 days
    ```
    
- **AddMonths()**
    
    Adds a specified number of months to the current date.
    
    ```csharp
    
    DateTime date = DateTime.Now;
    DateTime newDate = date.AddMonths(2);  // Adds 2 months
    ```
    
- **AddYears()**
    
    Adds a specified number of years to the current date.
    
    ```csharp
    
    DateTime date = DateTime.Now;
    DateTime newDate = date.AddYears(1);  // Adds 1 year
    ```
    
- **AddHours()**
    
    Adds hours to the current date and time.
    
    ```csharp
    DateTime date = DateTime.Now;
    DateTime newDate = date.AddHours(3);  // Adds 3 hours
    ```
    
- **AddMinutes()**
    
    Adds minutes to the current date and time.
    
    ```csharp
    DateTime date = DateTime.Now;
    DateTime newDate = date.AddMinutes(30);  // Adds 30 minutes
    ```
    
- **ToString()**
    
    Converts the `DateTime` object to a string in a specified format.
    
    ```csharp
    DateTime date = DateTime.Now;
    string formattedDate = date.ToString("yyyy-MM-dd");  // "2025-01-30"
    ```
    
- **Parse()**
    
    Converts a string to a `DateTime` object.
    
    ```csharp
    string dateStr = "2025-01-30";
    DateTime date = DateTime.Parse(dateStr);  // Converts string to DateTime
    ```
    
- **Compare()**
    
    Compares two `DateTime` objects.
    
    ```csharp
    DateTime date1 = DateTime.Now;
    DateTime date2 = DateTime.Now.AddDays(1);
    int comparison = DateTime.Compare(date1, date2);  // Negative, since date1 is earlier
    ```
    
- **Subtract()**
    
    Subtracts a specified date from another date and returns the difference.
    
    ```csharp
    DateTime date1 = DateTime.Now;
    DateTime date2 = date1.AddDays(5);
    TimeSpan difference = date2.Subtract(date1);  // 5 days
    ```
    
- **IsLeapYear()**
    
    Determines whether a specified year is a leap year.
    
    ```csharp
    bool isLeap = DateTime.IsLeapYear(2024);  // true
    ```
    
- **Date()**
    
    Gets the date component of a `DateTime` object.
    
    ```csharp
    DateTime date = DateTime.Now;
    DateTime dateOnly = date.Date;  // Removes the time component
    ```
    
- **ToShortDateString()**
    
    Converts the `DateTime` object to a short date string.
    
    ```csharp
    DateTime date = DateTime.Now;
    string shortDate = date.ToShortDateString();  // "1/30/2025"
    ```
    
- **ToLongDateString()**
    
    Converts the `DateTime` object to a long date string.
    
    ```csharp
    DateTime date = DateTime.Now;
    string longDate = date.ToLongDateString();  // "Thursday, January 30, 2025"
    ```
    

### **Conclusion**

The `DateTime` class is essential for working with dates and times in C#. It provides various methods to add or subtract time, format dates, and compare `DateTime` objects, making it a powerful tool for time-based operations.

---

## **Basic File Operations**

File handling in C# can be performed using the `System.IO` namespace. This allows you to perform various file operations such as reading, writing, deleting, and checking if a file exists.

### **Using System.IO**

The `System.IO` namespace provides classes like `File`, `FileInfo`, `StreamReader`, `StreamWriter`, etc., to work with files and directories.

### **1. Checking if a File Exists**

The `File.Exists()` method checks if a file exists at the specified path.

```csharp
using System.IO;

string filePath = "example.txt";

if (File.Exists(filePath))
{
    Console.WriteLine("File exists.");
}
else
{
    Console.WriteLine("File does not exist.");
}
```

### **2. Writing to a File**

You can write to a file using `File.WriteAllText()` or `StreamWriter`. If the file doesn't exist, it will be created; if it exists, the content will be overwritten.

### **WriteAllText Example:**

```csharp
using System.IO;

string filePath = "example.txt";
string content = "Hello, this is a test file.";

File.WriteAllText(filePath, content);
Console.WriteLine("Content written to the file.");
```

### **StreamWriter Example:**

```csharp
using System.IO;

string filePath = "example.txt";
using (StreamWriter writer = new StreamWriter(filePath))
{
    writer.WriteLine("Hello, this is a test file.");
    writer.WriteLine("Second line of the file.");
}
Console.WriteLine("Content written to the file.");
```

### **3. Reading from a File**

You can read the entire content of a file using `File.ReadAllText()` or read line-by-line using `StreamReader`.

### **ReadAllText Example:**

```csharp
using System.IO;

string filePath = "example.txt";

if (File.Exists(filePath))
{
    string content = File.ReadAllText(filePath);
    Console.WriteLine("File Content:");
    Console.WriteLine(content);
}
else
{
    Console.WriteLine("File does not exist.");
}
```

### **StreamReader Example:**

```csharp
using System.IO;

string filePath = "example.txt";

if (File.Exists(filePath))
{
    using (StreamReader reader = new StreamReader(filePath))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}
else
{
    Console.WriteLine("File does not exist.");
}
```

### **4. Deleting a File**

The `File.Delete()` method deletes the specified file if it exists.

```csharp
using System.IO;

string filePath = "example.txt";

if (File.Exists(filePath))
{
    File.Delete(filePath);
    Console.WriteLine("File deleted.");
}
else
{
    Console.WriteLine("File does not exist.");
}
```

### **5. Opening a File**

To open a file, you can use methods like `File.Open()` or `StreamReader` for reading, or `StreamWriter` for writing.

### **File.Open Example:**

```csharp
using System.IO;

string filePath = "example.txt";

// Open the file for reading
using (FileStream fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Read))
{
    // File operations like reading can be done here
    Console.WriteLine("File opened for reading.");
}
```

### **StreamReader (Alternative to Open for Reading):**

```csharp
using System.IO;

string filePath = "example.txt";

if (File.Exists(filePath))
{
    using (StreamReader reader = new StreamReader(filePath))
    {
        Console.WriteLine("File opened for reading.");
        string content = reader.ReadToEnd();
        Console.WriteLine(content);
    }
}
else
{
    Console.WriteLine("File does not exist.");
}
```

### **6. Handling Exceptions**

When working with files, it's good practice to handle exceptions, such as file not found or unauthorized access.

```csharp
using System;
using System.IO;

try
{
    string filePath = "example.txt";
    string content = File.ReadAllText(filePath);
    Console.WriteLine(content);
}
catch (FileNotFoundException)
{
    Console.WriteLine("File not found.");
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("Access denied.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
```

### **Conclusion**

The `System.IO` namespace offers a variety of methods to read, write, delete, and manipulate files in C#. It’s important to handle file operations carefully, ensuring that you manage file access, check file existence, and handle exceptions to ensure smooth file handling.