using System;
using System.Data;
using System.Linq;

namespace Advance_Of_C_.LINQOperations
{
    public class LINQWithDataTable
    {
        public void Test()
        {
            // Create sample data for Employee and Department tables
            DataTable employeeTable = CreateEmployeeTable();
            DataTable departmentTable = CreateDepartmentTable();

            // 1. Join Operation (Similar to Employee-Department Join)
            var joinResult = from emp in employeeTable.AsEnumerable()
                             join dep in departmentTable.AsEnumerable()
                             on emp.Field<int>("DepartmentId") equals dep.Field<int>("Id")
                             select new
                             {
                                 EmployeeId = emp.Field<int>("Id"),
                                 EmployeeName = emp.Field<string>("Name"),
                                 EmployeeSalary = emp.Field<decimal>("Salary"),
                                 Manager = emp.Field<string>("IsManager"),
                                 DepartmentName = dep.Field<string>("DepartmentName")
                             };

            Console.WriteLine("Join Operation Result:");
            foreach (var item in joinResult)
            {
                Console.WriteLine($"Id: {item.EmployeeId}, Name: {item.EmployeeName}, Salary: {item.EmployeeSalary}, Manager: {item.Manager}, DepartmentName: {item.DepartmentName}");
            }

            // One To Many
            var oneToManyResult = from dept in departmentTable.AsEnumerable()
                                  join emp in employeeTable.AsEnumerable()
                                  on dept.Field<int>("Id") equals emp.Field<int>("DepartmentId")
                                  select new
                                  {
                                      DepartmentName = dept.Field<string>("DepartmentName"),
                                      EmployeeName = emp.Field<string>("Name"),
                                      IsManager = emp.Field<string>("IsManager"),
                                      Salary = emp.Field<decimal>("Salary")
                                  };

            Console.WriteLine("\nOne to Many Join Results:");
            foreach (var item in oneToManyResult)
            {
                Console.WriteLine($"Department: {item.DepartmentName}, Employee: {item.EmployeeName}, Is Manager: {item.IsManager}, Salary: {item.Salary}");
            }

            // Left Join: employees with departments, include employee without departments
            var leftJoin = from emp in employeeTable.AsEnumerable()
                           join dept in departmentTable.AsEnumerable()
                           on emp.Field<int>("DepartmentId") equals dept.Field<int>("Id") into deptGroup
                           from d in deptGroup.DefaultIfEmpty()
                           select new
                           {
                               DepartmentName = d?.Field<string>("DepartmentName") ?? "No Department",
                               EmployeeName = emp.Field<string>("Name") // Handles departments with no employees
                           };

            Console.WriteLine("\nLeft Outer Join Results:");
            foreach (var item in leftJoin)
            {
                Console.WriteLine($"Employee Name: {item.EmployeeName}, Department: {item.DepartmentName}");
            }

            // Right Join
            var rightJoin = from dept in departmentTable.AsEnumerable()
                            join emp in employeeTable.AsEnumerable()
                            on dept.Field<int>("Id") equals emp.Field<int>("DepartmentId") into empGroup
                            from e in empGroup.DefaultIfEmpty()
                            select new
                            {
                                DepartmentName = dept.Field<string>("DepartmentName"),
                                EmployeeName = e?.Field<string>("Name") ?? "No Employee" // Handles departments with no employees
                            };

            Console.WriteLine("\nRight Outer Join Results:");
            foreach (var item in leftJoin)
            {
                Console.WriteLine($"Employee Name: {item.EmployeeName}, Department: {item.DepartmentName}");
            }

            // Full Join
            var fullJoin = leftJoin.Union(rightJoin);
            Console.WriteLine("\nFull Outer Join Results:");
            foreach (var item in fullJoin)
            {
                Console.WriteLine($"Department: {item.DepartmentName}, Employee: {item.EmployeeName}");
            }

            // cross join
            var crossJoin = from emp in employeeTable.AsEnumerable()
                            from dept in departmentTable.AsEnumerable()
                            select new
                            {
                                EmployeeName = emp.Field<string>("Name"),
                                DepartmentName = dept.Field<string>("DepartmentName")
                            };

            Console.WriteLine("\nCross join Results:");
            foreach (var item in crossJoin)
            {
                Console.WriteLine($"Employee: {item.EmployeeName}, Department: {item.DepartmentName}");
            }

            var groupResult = from emp in employeeTable.AsEnumerable()
                              group emp by emp.Field<int>("DepartmentId") into empGroup
                              orderby empGroup.Key
                              select empGroup;

            Console.WriteLine("\nGrouped by DepartmentId:");
            foreach (var empGroup in groupResult)
            {
                Console.WriteLine($"Department Id: {empGroup.Key}");
                foreach (var emp in empGroup)
                {
                    // Corrected: Accessing fields using Field<T>()
                    Console.WriteLine($"Name: {emp.Field<string>("Name"),-20}, IsManager: {emp.Field<string>("IsManager"),-5}, Salary: {emp.Field<decimal>("Salary"),5}");
                }
            }
            // All: Check if all employees have a salary above a threshold
            decimal salaryThreshold = 25000;
            bool allAboveThreshold = employeeTable.AsEnumerable().All(e => e.Field<decimal>("Salary") > salaryThreshold);
            Console.WriteLine($"\nAll employees salary > {salaryThreshold}: {allAboveThreshold}");

            // Any: Check if any employee has a salary above a specific amount
            bool anyAboveThreshold = employeeTable.AsEnumerable().Any(e => e.Field<decimal>("Salary") > 75000);
            Console.WriteLine($"Any employee salary > 75000: {anyAboveThreshold}");

            // Contains: Check if a specific employee exists in the table
            var searchEmployee = new DataRow[] { employeeTable.Rows[0] }; // John Doe
            bool employeeFound = employeeTable.AsEnumerable().Contains(searchEmployee[0]);
            Console.WriteLine($"Employee Found: {employeeFound}");

            // FirstOrDefault: Get the first employee with a salary above 50000
            var firstEmployee = employeeTable.AsEnumerable().FirstOrDefault(e => e.Field<decimal>("Salary") > 50000);
            Console.WriteLine($"\nFirst employee with salary > 50000: {firstEmployee?.Field<string>("Name")}");

            // Single: Get a single element (example: a single department with a specific condition)
            try
            {
                var singleElement = employeeTable.AsEnumerable().Single(e => e.Field<int>("Id") == 1);
                Console.WriteLine($"Single element with Id 1: {singleElement.Field<string>("Name")}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            // Last: Get the last employee in the list
            var lastElement = employeeTable.AsEnumerable().Last();
            Console.WriteLine($"\nLast employee: {lastElement.Field<string>("Name")}");

            // Concat: Concatenate two tables (e.g., adding new rows to the existing table)
            var newEmployee = new DataTable();
            newEmployee.Columns.Add("Id", typeof(int));
            newEmployee.Columns.Add("Name", typeof(string));
            newEmployee.Columns.Add("IsManager", typeof(string));
            newEmployee.Columns.Add("Salary", typeof(decimal));
            newEmployee.Columns.Add("DepartmentId", typeof(int));

            newEmployee.Rows.Add(5, "Alice Green", "Yes", 95000, 1);

            var concatenatedEmployees = employeeTable.AsEnumerable().Concat(newEmployee.AsEnumerable());
            Console.WriteLine("\nConcatenated Employee List:");
            foreach (var emp in concatenatedEmployees)
            {
                Console.WriteLine($"Name: {emp.Field<string>("Name")}, Salary: {emp.Field<decimal>("Salary")}");
            }

            // Aggregate: Calculate total salary with a bonus
            decimal totalSalary = employeeTable.AsEnumerable().Aggregate<DataRow, decimal>(0, (total, e) =>
            {
                decimal bonus = (e.Field<string>("IsManager") == "Yes") ? e.Field<decimal>("Salary") * 0.04m : e.Field<decimal>("Salary") * 0.02m;
                total += e.Field<decimal>("Salary") + bonus;
                return total;
            });
            Console.WriteLine($"Total Salary with Bonus: {totalSalary}");

            // DefaultIfEmpty: Handle an empty table
            var emptyTable = new DataTable();
            emptyTable.Columns.Add("Id", typeof(int)); // Defining the column type as int

            // Create a new row for the DataTable with DBNull for any missing values
            var defaultRow = emptyTable.AsEnumerable().DefaultIfEmpty(employeeTable.NewRow()).First();

            // Safely handle DBNull and cast the value to nullable type
            int? id = defaultRow.IsNull("Id") ? (int?)null : defaultRow.Field<int>("Id");
            Console.WriteLine($"\nDefault if empty: {id ?? 0}");  // Default to 0 if null
        }

        private DataTable CreateEmployeeTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Salary", typeof(decimal));
            dt.Columns.Add("IsManager", typeof(string));
            dt.Columns.Add("DepartmentId", typeof(int));

            dt.Rows.Add(1, "John", 50000, "Yes", 1);
            dt.Rows.Add(2, "Jane", 45000, "No", 2);
            dt.Rows.Add(3, "Mike", 60000, "Yes", 1);
            dt.Rows.Add(4, "Sara", 40000, "No", 3);

            return dt;
        }

        private DataTable CreateDepartmentTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("DepartmentName", typeof(string));

            dt.Rows.Add(1, "HR");
            dt.Rows.Add(2, "Engineering");
            dt.Rows.Add(3, "Marketing");

            return dt;
        }
    }
}
