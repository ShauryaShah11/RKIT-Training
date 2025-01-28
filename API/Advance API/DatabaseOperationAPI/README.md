# Database With C# (CRUD)

## **Index**

1. [Introduction](#introduction)
2. [Necessary Packages and Installation](#necessary-packages-and-installation)
    1. [Install MySQL.Data Package](#install-mysqldata-package)
3. [Tools and Technologies](#tools-and-technologies)
    1. [MySQL Database](#mysql-database)
    2. [MySQL Connector for .NET](#mysql-connector-for-net)
    3. [Configuration Management](#configuration-management)
4. [CRUD Operations](#crud-operations)
    1. [Create (Insert)](#create-insert)
    2. [Read (Select)](#read-select)
    3. [Update](#update)
    4. [Delete](#delete)
5. [Error Handling and Logging](#error-handling-and-logging)
6. [Data Conversion](#data-conversion)
7. [Best Practices](#best-practices)
8. [Conclusion](#conclusion)

---

## **1. Necessary Packages and Installation**

### **1.1. Install MySQL.Data Package**

To connect to MySQL from C#, we will use the `MySql.Data` package. This package provides the necessary ADO.NET classes for interacting with MySQL databases.

To install the MySQL.Data package, run the following command in the **NuGet Package Manager Console** or use the **NuGet Manager** in Visual Studio:

```bash
Install-Package MySql.Data
```

This package will allow you to establish a connection, execute SQL queries, and manage the MySQL database directly from your C# application.

### **1.2. Configuration**

Once the package is installed, you'll need to configure your connection string in the `App.config` file to store your database credentials securely.

```xml
<configuration>
    <connectionStrings>
        <add name="MyConnectionString" connectionString="Server=localhost;Port=3306;Database=mydatabase;Uid=myusername;Pwd=mypassword;" />
    </connectionStrings>
</configuration>
```

Replace `localhost`, `mydatabase`, `myusername`, and `mypassword` with your actual database connection information.

---

## **2. Introduction**

This documentation outlines the process of performing CRUD (Create, Read, Update, Delete) operations with a MySQL database in C# using the `MySql.Data.MySqlClient` library for database interaction. The repository demonstrates object-oriented practices in the context of database access, including the use of the repository pattern, error handling, and data conversion between C# objects and MySQL database records.

### **Key Objectives:**

- Interacting with a MySQL database using C#.
- Implementing CRUD operations with repository pattern.
- Handling exceptions and retrieving data efficiently.
- Converting database rows to C# objects and vice versa.

---

## **3. Tools and Technologies**

### **3.1. MySQL Database**

MySQL is a popular open-source relational database management system. It is used to store and retrieve data in structured tables and is the chosen database for this project.

### **3.2. MySQL Connector for .NET**

To communicate between C# applications and MySQL, we use the `MySql.Data.MySqlClient` library. This library provides the necessary API for connecting to a MySQL database, executing queries, and retrieving results in a C# environment.

### **3.3. Configuration Management**

Connection strings and database configurations are stored securely in the `App.config` file. This approach ensures that credentials and other sensitive information are not hardcoded within the code.

---

## **4. CRUD Operations**

### **4.1. Create (Insert)**

The `AddDepartment` method is responsible for inserting a new department record into the `YMD01` table. It uses the `INSERT INTO` SQL statement to add a row to the table.

```csharp

public Response AddDepartment(YMD01 department)
{
    try
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            var query = "INSERT INTO YMD01 (DepartmentName, Location) VALUES (@DepartmentName, @Location)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            cmd.Parameters.AddWithValue("@Location", department.Location);
            conn.Open();
            cmd.ExecuteNonQuery();
            return new Response { Message = "Department added successfully", IsError = false };
        }
    }
    catch (MySqlException ex)
    {
        return new Response { Message = $"Database error: {ex.Message}", IsError = true };
    }
}

```

### **4.2. Read (Select)**

- The `GetDepartmentData` method retrieves all department records.
- The `GetDepartmentById` method retrieves a department record by its ID.

```csharp

public List<YMD01> GetDepartmentData()
{
    var departments = new List<YMD01>();
    try
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            var query = "SELECT * FROM YMD01";
            var cmd = new MySqlCommand(query, conn);
            conn.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    departments.Add(new YMD01
                    {
                        Id = reader.GetInt32("Id"),
                        DepartmentName = reader.GetString("DepartmentName"),
                        Location = reader.GetString("Location")
                    });
                }
            }
        }
        return departments;
    }
    catch (MySqlException ex)
    {
        throw new Exception($"Error reading data: {ex.Message}");
    }
}

```

### **4.3. Update**

The `UpdateDepartment` method modifies an existing department record based on the provided ID. The `UPDATE` SQL statement is used.

```csharp

public Response UpdateDepartment(YMD01 department)
{
    try
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            var query = "UPDATE YMD01 SET DepartmentName = @DepartmentName, Location = @Location WHERE Id = @Id";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", department.Id);
            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            cmd.Parameters.AddWithValue("@Location", department.Location);
            conn.Open();
            cmd.ExecuteNonQuery();
            return new Response { Message = "Department updated successfully", IsError = false };
        }
    }
    catch (MySqlException ex)
    {
        return new Response { Message = $"Database error: {ex.Message}", IsError = true };
    }
}

```

### **4.4. Delete**

The `DeleteDepartment` method removes a department record based on the provided ID using the `DELETE` SQL statement.

```csharp

public Response DeleteDepartment(int id)
{
    try
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            var query = "DELETE FROM YMD01 WHERE Id = @Id";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            return new Response { Message = "Department deleted successfully", IsError = false };
        }
    }
    catch (MySqlException ex)
    {
        return new Response { Message = $"Database error: {ex.Message}", IsError = true };
    }
}
```

---

## **6. Error Handling and Logging**

### **Error Handling**

Error handling is achieved using `try-catch` blocks to manage exceptions such as database connection errors or SQL errors.

```csharp
catch (MySqlException ex)
{
    return new Response { Message = $"Database error: {ex.Message}", IsError = true };
}
catch (Exception ex)
{
    return new Response { Message = ex.Message, IsError = true };
}

```

### **Logging**

You can implement a logging mechanism using third-party libraries like **Serilog** or **NLog** for better monitoring and debugging.

---

## **7. Data Conversion**

The repository pattern includes a data conversion utility for converting C# objects to `DataTable` and vice versa.

```csharp
private DataTable ConvertToDataTable(List<YMD01> departments)
{
    DataTable table = new DataTable("YMD01");
    table.Columns.Add("Id");
    table.Columns.Add("DepartmentName");
    table.Columns.Add("Location");

    foreach (var dept in departments)
    {
        table.Rows.Add(dept.Id, dept.DepartmentName, dept.Location);
    }

    return table;
}
```

---

## **8. Best Practices**

- **SQL Injection Prevention:** Always use parameterized queries to prevent SQL injection attacks.
- **Resource Management:** Use `using` statements to ensure proper disposal of database connections.
- **Error Handling:** Always catch specific exceptions before general ones.

---

## **9. Conclusion**

This documentation covers the implementation of CRUD operations in C# with a MySQL database. The repository pattern, error handling, and best practices help ensure that the code is clean, maintainable, and secure. The provided examples demonstrate how to perform data operations effectively while handling potential issues and ensuring efficient resource management.