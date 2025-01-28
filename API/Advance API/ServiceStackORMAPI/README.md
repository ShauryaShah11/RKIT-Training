# Service Stack ORM Lite

### **Index**

1. [What is an ORM Tool?](#what-is-an-orm-tool)
2. [ServiceStack ORM Lite vs Entity Framework](#servicestack-orm-lite-vs-entity-framework)
3. [ServiceStack ORM Lite Documentation](#servicestack-orm-lite-documentation)
    - [Installation](#installation)
    - [Basic Configuration](#basic-configuration)
    - [Creating Models](#creating-models)
    - [CreateTableIfNotExists](#createtableifnotexists)
    - [CRUD Operations](#crud-operations)
    - [Join Operation](#join-operation)
4. [Advanced Features](#advanced-features)
    - [Custom Queries](#custom-queries)
    - [Transactions](#transactions)
5. [Error Handling](#error-handling)
6. [Performance Tips](#performance-tips)
7. [Additional Resources](#additional-resources)
8. [Data Annotation in Service Stack](#data-annotation-in-service-stack)


---

### **What is an ORM Tool?**

An **Object-Relational Mapper (ORM)** is a programming tool that facilitates the interaction between object-oriented programming languages and relational databases. It automates the mapping of database tables to objects, eliminating the need for manual SQL queries and improving productivity.

Common benefits of using an ORM tool:

- Reduces boilerplate code.
- Simplifies database interaction.
- Increases development speed.

---

### **ServiceStack ORM Lite vs Entity Framework**

| **Feature** | **ServiceStack ORM Lite** | **Entity Framework** |
| --- | --- | --- |
| **Complexity** | Lightweight, simple API | Feature-rich, larger learning curve |
| **Performance** | Fast for basic CRUD operations | Slower for simple operations due to abstraction layers |
| **Database Support** | Supports a wide range of databases including SQL Server, MySQL, SQLite, etc. | Primarily optimized for SQL Server but supports others with limitations |
| **Querying** | Supports SQL and LINQ-like queries | Full LINQ support and includes a rich query-building mechanism |
| **Configuration** | Minimal configuration required | Requires more configuration and setup |
| **Migrations** | Does not include automatic migrations, manual approach needed | Supports automatic migrations and migrations tools |
| **Flexibility** | More flexible for lightweight applications | More suited for enterprise applications with complex requirements |

---

### **ServiceStack ORM Lite Documentation**

### **Installation**

1. Install ServiceStack ORM Lite via NuGet:
    
    ```bash
    Install-Package ServiceStack.OrmLite
    ```
    
2. Alternatively, use the **.NET CLI**:
    
    ```bash
    dotnet add package ServiceStack.OrmLite
    ```
    

---

### **Basic Configuration**

To set up ServiceStack ORM Lite:

```csharp
using ServiceStack.OrmLite;
using System.Data;

string connectionString = "your_connection_string_here";
IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

using (var db = dbFactory.Open())
{
    // ORM operations here
}
```

---

### **Creating Models**

ORM Lite models map to database tables. Properties map to columns.

Example model:

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

### **CreateTableIfNotExists**

To create a table only if it does not already exist, use the `CreateTableIfNotExists` method.

```csharp
using (var db = dbFactory.Open())
{
		// Create the User table if it doesn't exist
		db.CreateTableIfNotExists<User>();
		Console.WriteLine("User table is created if it did not already exist.");
}
```

---

### **CRUD Operations**

### **Create**

To insert a new record:

```csharp
using (var db = dbFactory.Open())
{
    var user = new User { Name = "Shaurya", Email = "example@example.com" };
    db.Insert(user);
}
```

### **Read**

To fetch data:

```csharp
using (var db = dbFactory.Open())
{
    var user = db.Single<User>(x => x.Id == 1);  // Retrieve a user by Id
}
```

### **Update**

To update an existing record:

```csharp
using (var db = dbFactory.Open())
{
    var user = db.Single<User>(x => x.Id == 1);
    user.Name = "Updated Name";
    db.Update(user);
}
```

### **Delete**

To delete a record:

```csharp
using (var db = dbFactory.Open())
{
    db.Delete<User>(x => x.Id == 1);
}
```

---

### **Join Operation**

In ORM Lite, you can perform **JOIN** operations between tables using LINQ-like syntax or SQL queries.

### **Join Example:**

Consider two models: `User` and `Order`. A user can have many orders, so the tables are related by the `UserId` field.

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Product { get; set; }
}
```

To perform a **JOIN** between `User` and `Order`, you can use the following query:

```csharp
using (var db = dbFactory.Open())
{
    var joinQuery = db.From<User>()
                      .Join<Order>((u, o) => u.Id == o.UserId)
                      .Select<User, Order>((u, o) => new { u.Name, o.Product });

    var result = db.Select(joinQuery);

    foreach (var item in result)
    {
        Console.WriteLine($"User: {item.Name}, Product: {item.Product}");
    }
}
```

Here, the `Join` method links the `User` and `Order` tables using the `UserId` field. The `Select` method then selects the user’s name and their corresponding product from the `Order` table.

### **Join with Additional Filtering:**

You can also add additional conditions to filter your results:

```csharp
using (var db = dbFactory.Open())
{
    var joinQuery = db.From<User>()
                      .Join<Order>((u, o) => u.Id == o.UserId)
                      .Where<Order>(o => o.Product.Contains("Laptop"))
                      .Select<User, Order>((u, o) => new { u.Name, o.Product });

    var result = db.Select(joinQuery);

    foreach (var item in result)
    {
        Console.WriteLine($"User: {item.Name}, Product: {item.Product}");
    }
}
```

This query will return users who have purchased a product containing the word "Laptop".

---

### **Advanced Features**

### **Custom Queries**

Execute raw SQL queries:

```csharp
using (var db = dbFactory.Open())
{
    var users = db.Select<User>("SELECT * FROM Users WHERE Name = {0}", "Shaurya");
}
```

### **Transactions**

To handle transactions:

```csharp
using (var db = dbFactory.Open())
{
    using (var transaction = db.OpenTransaction())
    {
        db.Insert(new User { Name = "John", Email = "john@example.com" });
        transaction.Commit();  // Commit changes
    }
}
```

---

### **Error Handling**

Always handle errors during database operations:

```csharp
try
{
    using (var db = dbFactory.Open())
    {
        var user = db.Single<User>(x => x.Id == 1);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

---

### **Performance Tips**

- **Batch Insertions:** Use `InsertAll` for bulk inserts.
- **Query Optimization:** Use indexes on frequently queried columns.
- **Connection Pooling:** Ensure efficient database connections.

---

### **Additional Resources**

- [ServiceStack ORM Lite GitHub](https://github.com/ServiceStack/ServiceStack.OrmLite)
- [Official ORM Lite Documentation](https://docs.servicestack.net/)

---

### Data Annotation in Service Stack

### **1. AddColumnAttribute**

- **Purpose**: Adds a new column to an existing table in the database. This attribute automatically updates the schema when new properties are added to the POCO (Plain Old CLR Object) class.
- **Usage**:
    - If you add a new property to a class and use the `AddColumn` attribute, ServiceStack ORM will automatically add a column for this property to the table when the schema is updated.

```csharp
public class Employee
{
    public int Id { get; set; }

    [AddColumn]  // Adds a new column 'Email' to the table.
    public string Email { get; set; }
}
```

---

### **2. AliasAttribute**

- **Purpose**: Defines an alias for a property in your POCO class. This is used to map the class property to a different column name in the database.
- **Usage**:
    - Helps when the property name does not match the database column name.

```csharp
public class Employee
{
    [Alias("Emp_Name")]  // Maps 'Name' property to 'Emp_Name' column in the database.
    public string Name { get; set; }
}
```

---

### **3. AlterColumnAttribute**

- **Purpose**: Used to update the schema of an existing table by altering an existing column. The column type or other attributes can be modified automatically when the schema is updated.
- **Usage**:
    - Modify the properties of an existing column, such as changing its type or size.

```csharp
public class Employee
{
    [AlterColumn]  // Alters the column properties in the database when the schema is updated.
    public string Name { get; set; }
}
```

---

### **4. AutoIdAttribute**

- **Purpose**: Automatically generates the value for an ID field, typically for primary key columns. It's commonly used for auto-incremented fields.
- **Usage**:
    - Typically applied to an `int` or `long` field that should automatically increment in the database.

```csharp
public class Employee
{
    [AutoId]  // Automatically increments the 'Id' field in the database.
    public int Id { get; set; }
}
```

---

### **5. AutoIncrement vs AutoId**

- **AutoIdAttribute** and **AutoIncrementAttribute** are similar but differ in their context.
    - **AutoId** automatically generates the ID value when a new record is inserted.
    - **AutoIncrement** ensures the column will auto-increment with each new row inserted.

```csharp
public class Employee
{
    [AutoId]  // Auto-generated ID value
    public int Id { get; set; }

    [AutoIncrement]  // Ensures auto-increment behavior in DB.
    public int EmployeeId { get; set; }
}
```

---

### **6. BelongToAttribute**

- **Purpose**: Defines a relationship between two tables where one table has a foreign key that refers to another table. It specifies that the current class belongs to another table.
- **Usage**:
    - Used to set up relationships between entities.

```csharp
public class Employee
{
    [BelongTo(typeof(Department))]  // Links Employee to Department table via foreign key.
    public int DepartmentId { get; set; }
}
```

---

### **7. CheckConstraintAttribute**

- **Purpose**: Defines a check constraint in the database for a column, ensuring that values inserted into the column meet a specified condition.
- **Usage**:
    - Example: Ensuring an age column must be greater than 18.

```csharp
public class Employee
{
    [CheckConstraint("Age > 18")]  // Ensures Age must be greater than 18.
    public int Age { get; set; }
}
```

---

### **8. CompositeIndexAttribute**

- **Purpose**: Used to create a composite index for multiple columns in a table. This improves query performance by indexing multiple columns.
- **Usage**:
    - Example: Creating an index on both `FirstName` and `LastName` columns.

```csharp
public class Employee
{
    [CompositeIndex("FirstName", "LastName")]  // Creates a composite index on both fields.
    public string FirstName { get; set; }

    public string LastName { get; set; }
}
```

---

### **9. CompositeKeyAttribute**

- **Purpose**: Used to define a composite key, which consists of multiple columns, as the primary key for a table.
- **Usage**:
    - Example: A table where a combination of `DepartmentId` and `EmployeeId` serves as the primary key.

```csharp
public class Employee
{
    [CompositeKey("DepartmentId", "EmployeeId")]  // Composite primary key
    public int DepartmentId { get; set; }

    public int EmployeeId { get; set; }
}
```

---

### **10. ComputeAttribute**

- **Purpose**: Used to define computed properties that are calculated based on other fields or columns.
- **Usage**:
    - Example: A computed field for `FullName` derived from `FirstName` and `LastName`.

```csharp
public class Employee
{
    [Compute("FirstName + ' ' + LastName")]  // Computes FullName based on First and Last names.
    public string FullName { get; set; }
}
```

---

### **11. CustomFieldAttribute**

- **Purpose**: Used to specify custom fields that may not have a direct column mapping or require special handling.
- **Usage**:
    - Example: A field used for temporary or computed data.

```csharp
public class Employee
{
    [CustomField]  // Specifies a custom field.
    public string TempField { get; set; }
}
```

---

### **12. CustomInsertAttribute**

- **Purpose**: Customizes how records are inserted into the database.
- **Usage**:
    - Example: Use custom logic to modify insert behavior.

```csharp
public class Employee
{
    [CustomInsert]  // Apply custom insert behavior.
    public string Name { get; set; }
}
```

---

### **13. CustomSelectAttribute**

- **Purpose**: Customizes how records are selected from the database.
- **Usage**:
    - Example: Custom logic to fetch data with conditions.

```csharp
public class Employee
{
    [CustomSelect]  // Apply custom select behavior.
    public string Name { get; set; }
}
```

---

### **14. CustomUpdateAttribute**

- **Purpose**: Customizes how records are updated in the database.
- **Usage**:
    - Example: Custom logic to update data based on certain conditions.

```csharp
public class Employee
{
    [CustomUpdate]  // Apply custom update behavior.
    public string Name { get; set; }
}
```

---

### **15. DecimalLengthAttribute**

- **Purpose**: Specifies the precision and scale for decimal fields in the database.
- **Usage**:
    - Example: Defining a `Salary` field with two decimal places.

```csharp
public class Employee
{
    [DecimalLength(10, 2)]  // Specifies precision and scale for Salary.
    public decimal Salary { get; set; }
}
```

---

### **16. DefaultAttribute**

- **Purpose**: Specifies a default value for a column when no value is provided.
- **Usage**:
    - Example: Default value for `IsActive`.

```csharp
public class Employee
{
    [Default(1)]  // Default value for IsActive is 1.
    public int IsActive { get; set; }
}
```

---

### **17. DescriptionAttribute**

- **Purpose**: Adds a description to a field or class, providing extra metadata.
- **Usage**:
    - Example: Describing the `Age` field.

```csharp
public class Employee
{
    [Description("Employee's age in years.")]  // Description for Age field.
    public int Age { get; set; }
}
```

---

### **18. EnumAsCharAttribute**

- **Purpose**: Maps an enum to a single character column in the database.
- **Usage**:
    - Example: Mapping the `Status` enum to a single-character column.

```csharp
public enum Status
{
    Active = 'A',
    Inactive = 'I'
}

public class Employee
{
    [EnumAsChar]  // Maps enum to a single-character column.
    public Status Status { get; set; }
}
```

---

### **19. EnumAsIntAttribute**

- **Purpose**: Maps an enum to an integer column in the database.
- **Usage**:
    - Example: Mapping the `Status` enum to an integer column.

```csharp
public enum Status
{
    Active = 1,
    Inactive = 0
}

public class Employee
{
    [EnumAsInt]  // Maps enum to an integer column.
    public Status Status { get; set; }
}
```

---

### **20. ExcludeAttribute**

- **Purpose**: Excludes a property from being mapped to a database column.
- **Usage**:
    - Example: Excluding `Age` from being stored in the database.

```csharp
public class Employee
{
    [Exclude]  // Excludes the Age field from the database.
    public int Age { get; set; }
}
```

---

### **21. ExcludeMetadataAttribute**

- **Purpose**: Excludes a field from metadata operations, such as querying or inserting.
- **Usage**:
    - Example: Excluding `Age` from metadata processing.

```csharp
public class Employee
{
    [ExcludeMetadata]  // Excludes Age from metadata handling.
    public int Age { get; set; }
}
```

---

### **22. ForeignKeyAttribute**

- **Purpose**: Defines a foreign key relationship between two tables.
- **Usage**:
    - Example: `Employee` has a foreign key referring to `Department`.

```csharp
public class Employee
{
    [ForeignKey]  // Links Employee to Department.
    public int DepartmentId { get; set; }
}
```

---

### **23. IgnoreAttribute**

- **Purpose**: Ignores the property during database operations (inserts, selects, updates).
- **Usage**:
    - Example: Ignoring `TempField` during database operations.

```csharp
public class Employee
{
    [Ignore]  // Ignores TempField for DB operations.
    public string TempField { get; set; }
}
```

---

### **24. IndexAttribute**

- **Purpose**: Adds an index on one or more columns to speed up queries.
- **Usage**:
    - Example: Indexing `LastName` to speed up search operations.

```csharp
public class Employee
{
    [Index]  // Adds an index to LastName column.
    public string LastName { get; set; }
}
```

---

### **25. StringLengthAttribute**

- **Purpose**: Defines the maximum length for string columns.
- **Usage**:
    - Example: Limiting `Name` to 100 characters.

```csharp
public class Employee
{
    [StringLength(100)]  // Limits Name to 100 characters.
    public string Name { get; set; }
}
```

---

### **26. UniqueAttribute**

- **Purpose**: Enforces a uniqueness constraint on a column.
- **Usage**:
    - Example: Ensuring `Email` is unique.

```csharp
public class Employee
{
    [Unique]  // Ensures Email is unique.
    public string Email { get; set; }
}
```

---

### **27. RequiredAttribute**

- **Purpose**: Specifies that a field is required and cannot be null or empty.
- **Usage**:
    - Example: Ensuring `Name` is always provided.

```csharp
public class Employee
{
    [Required]  // Marks Name as a required field.
    public string Name { get; set; }
}
```

---