using System;
using System.Data;

class DataTableClass
{
    public void DataTablePractice()
    {
        // Create a new DataTable
        DataTable dt = new DataTable();

        // Add columns to the DataTable
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Age", typeof(int));

        // Adding constraints

        // Add a Primary Key constraint on the "ID" column
        dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };

        // Add a Unique Constraint on the "Name" column
        dt.Columns["Name"].Unique = true;

        // Add rows to the DataTable
        dt.Rows.Add(1, "John Doe", 30);
        dt.Rows.Add(2, "Jane Smith", 16);
        dt.Rows.Add(3, "Alice Johnson", 35);

        // Access data from the DataTable
        Console.WriteLine("All rows in DataTable:");
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"ID: {row["ID"]}, Name: {row["Name"]}, Age: {row["Age"]}");
        }

        // Example: Selecting rows where age is 18 or greater
        DataRow[] legalAge = dt.Select("Age >= 18");
        Console.WriteLine("\nRows with legal age:");
        foreach (DataRow row in legalAge)
        {
            Console.WriteLine($"ID: {row["ID"]}, Name: {row["Name"]}, Age: {row["Age"]}");
        }

        // Example: Handling violation of the unique constraint
        try
        {
            dt.Rows.Add(4, "John Doe", 40);  // Duplicate Name will cause an exception
        }
        catch (ConstraintException ex)
        {
            Console.WriteLine("\nError: " + ex.Message);
        }
    }
}
