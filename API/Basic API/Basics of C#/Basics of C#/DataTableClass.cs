using System;
using System.Data;

/// <summary>
/// The DataTableClass class demonstrates the usage of DataTable and DataSetin C#.
/// </summary>
class DataTableClass
{
    /// <summary>
    /// The DataTablePractice method demonstrates the usage of DataTable and DataSet in C#.
    /// It includes creating a DataTable, adding columns, adding rows, adding constraints, filtering data, and handling constraint violations.
    /// </summary>
    public void DataTablePractice()
    {
        // Create a new DataSet
        DataSet ds = new DataSet("EmployeeDataSet");

        // Create a new DataTable
        DataTable dt = new DataTable("Employee");

        // Add columns to the DataTable
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Age", typeof(int));

        // Adding constraints
        dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] }; // Primary Key on ID
        dt.Columns["Name"].Unique = true;                     // Unique constraint on Name

        // Add rows to the DataTable
        dt.Rows.Add(1, "John Doe", 30);
        dt.Rows.Add(2, "Jane Smith", 16);
        dt.Rows.Add(3, "Alice Johnson", 35);

        // Add the DataTable to the DataSet
        ds.Tables.Add(dt);

        // Find the number of rows in the Employee table
        Console.WriteLine($"Number of rows in Employee table: {dt.Rows.Count}");

        // Accessing data through DataSet
        Console.WriteLine("Data in the DataSet:");
        foreach (DataRow row in ds.Tables["Employee"].Rows)
        {
            Console.WriteLine($"ID: {row["ID"]}, Name: {row["Name"]}, Age: {row["Age"]}");
        }

        // Example: Filter data using DataTable's Select method
        DataRow[] rowsFilteredByAge = ds.Tables["Employee"].Select("Age >= 18");
        Console.WriteLine("\nEmployees of legal age (18 or older):");
        foreach (DataRow row in rowsFilteredByAge)
        {
            Console.WriteLine($"ID: {row["ID"]}, Name: {row["Name"]}, Age: {row["Age"]}");
        }

        // Adding a new DataTable to the DataSet
        DataTable deptTable = new DataTable("Department");
        deptTable.Columns.Add("DeptID", typeof(int));
        deptTable.Columns.Add("DeptName", typeof(string));

        // Add rows to the Department table
        deptTable.Rows.Add(1, "HR");
        deptTable.Rows.Add(2, "Engineering");

        // Add Department table to the DataSet
        ds.Tables.Add(deptTable);

        // Accessing data from multiple tables in the DataSet
        Console.WriteLine("\nDepartments in the DataSet:");
        foreach (DataRow row in ds.Tables["Department"].Rows)
        {
            Console.WriteLine($"DeptID: {row["DeptID"]}, DeptName: {row["DeptName"]}");
        }

        //Console.WriteLine(ds.Tables);

        // Example: Handling constraint violations
        try
        {
            dt.Rows.Add(4, "John Doe", 40); // Duplicate Name will cause an exception
        }
        catch (ConstraintException ex)
        {
            Console.WriteLine("\nError: " + ex.Message);
        }
    }
}
