using System;
using System.Collections.Generic;
using System.Linq;

namespace Advance_Of_C_.LINQOperations
{
    public class LINQWithList
    {
        // Combined LINQ function with multiple operations on lists
        public void Test()
        {
            try
            {
                List<Employee> employees = GetEmployeeData();
                List<Department> departments = GetDepartments();

                // Join Operation: Inner Join between Employees and Departments
                var innerJoin = from emp in employees
                                join dep in departments
                                on emp.DepartmentId equals dep.Id // Adjusted to join on DepartmentId
                                select new
                                {
                                    EmployeeId = emp.Id,
                                    EmployeeName = emp.Name,
                                    EmployeeSalary = emp.Salary,
                                    Manager = emp.IsManager,
                                    DepartmentName = dep.DepartmentName
                                };

                Console.WriteLine("Inner Join Operation Results:");
                foreach (var item in innerJoin)
                {
                    Console.WriteLine($"Id: {item.EmployeeId}, Name: {item.EmployeeName}, Salary: {item.EmployeeSalary}, Manager: {item.Manager}, DepartmentName: {item.DepartmentName}");
                }

                // One-to-Many Join: Departments and Employees relationship
                var oneToManyResult = from department in departments
                                      join employee in employees
                                      on department.Id equals employee.DepartmentId // Adjusted to join on DepartmentId
                                      select new
                                      {
                                          DepartmentName = department.DepartmentName,
                                          EmployeeName = employee.Name,
                                          IsManager = employee.IsManager,
                                          Salary = employee.Salary
                                      };

                Console.WriteLine("\nOne to Many Join Results:");
                foreach (var item in oneToManyResult)
                {
                    Console.WriteLine($"Department: {item.DepartmentName}, Employee: {item.EmployeeName}, Is Manager: {item.IsManager}, Salary: {item.Salary}");
                }

                // Left Join: employees with departments, include employee without departments
                var leftJoin = from emp in employees
                               join dept in departments
                               on emp.DepartmentId equals dept.Id into deptGroup // Group join based on DepartmentId
                               from d in deptGroup.DefaultIfEmpty() // DefaultIfEmpty to handle cases where there's no department
                               select new
                               {
                                   DepartmentName = d?.DepartmentName ?? "No Department", // Handle null departments with the null-conditional operator
                                   EmployeeName = emp.Name,
                               };

                Console.WriteLine("\nLeft Outer Join Results:");
                foreach (var item in leftJoin)
                {
                    Console.WriteLine($"Employee Name: {item.EmployeeName}, Department: {item.DepartmentName}");
                }

                // Right Join: departments with employees, includes departments without employees
                var rightJoin = from dept in departments
                                join emp in employees
                                on dept.Id equals emp.DepartmentId into empGroup // Group join based on DepartmentId
                                from e in empGroup.DefaultIfEmpty() // DefaultIfEmpty to handle departments without employees
                                select new
                                {
                                    DepartmentName = dept.DepartmentName,
                                    EmployeeName = e?.Name ?? "No Employee" // Handle cases where no employee is present
                                };

                Console.WriteLine("\nRight Join Results:");
                foreach (var item in rightJoin)
                {
                    Console.WriteLine($"Department: {item.DepartmentName}, Employee: {item.EmployeeName}");
                }

                var fullJoin = leftJoin.Union(rightJoin);

                Console.WriteLine("\nFull Outer Join Results:");
                foreach (var item in fullJoin)
                {
                    Console.WriteLine($"Department: {item.DepartmentName}, Employee: {item.EmployeeName}");
                }

                // Cross Join => all Possible Combination.
                var crossJoin = from emp in employees
                                from dept in departments
                                select new
                                {
                                    EmployeeName = emp.Name,
                                    DepartmentName = dept.DepartmentName
                                };

                Console.WriteLine("\nCROSS JOIN:");
                foreach (var item in crossJoin)
                {
                    Console.WriteLine($"{item.EmployeeName} works in {item.DepartmentName}");
                }

                // LINQ Operator: Join with Ordering
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

                Console.WriteLine("\nOrdered Join Results:");
                foreach (var item in orderedResult)
                {
                    Console.WriteLine($"Name: {item.Name,-20}, IsManager: {item.IsManager,-5}, Salary: {item.Salary,5}, DepartmentName: {item.DepartmentName,-10}");
                }

                // Group By Example: Group employees by DepartmentId
                var groupResult = from emp in employees
                                  group emp by emp.DepartmentId into empGroup
                                  orderby empGroup.Key
                                  select empGroup;

                Console.WriteLine("\nGrouped by DepartmentId:");
                foreach (var empGroup in groupResult)
                {
                    Console.WriteLine($"Department Id: {empGroup.Key}");
                    foreach (var emp in empGroup)
                    {
                        Console.WriteLine($"Name: {emp.Name,-20}, IsManager: {emp.IsManager,-5}, Salary: {emp.Salary,5}");
                    }
                }

                // All, Any, Contains Example
                int salaryThreshold = 25000;
                bool allAboveThreshold = employees.All(e => e.Salary > salaryThreshold);
                Console.WriteLine($"\nAll employees salary > {salaryThreshold}: {allAboveThreshold}");

                bool anyAboveThreshold = employees.Any(e => e.Salary > 75000);
                Console.WriteLine($"Any employee salary > 75000: {anyAboveThreshold}");

                var searchEmployee = new Employee() { Id = 1, Name = "John Doe", IsManager = "Yes", Salary = 75000, DepartmentId = 1 };
                bool employeeFound = employees.Contains(searchEmployee, new EmployeeComparer());
                Console.WriteLine($"Employee Found: {employeeFound}");

                // LINQ First/FirstOrDefault Example
                var firstEmployee = employees.FirstOrDefault(e => e.Salary > 50000);
                Console.WriteLine($"\nFirst employee with salary > 50000: {firstEmployee?.Name}");

                // LINQ Single Example
                var singleElement = new List<int>() { 1 }.Single();
                Console.WriteLine($"\nSingle element: {singleElement}");

                // LINQ Last/LastOrDefault Example
                var lastElement = new List<int>() { 10, 20, 30, 40, 50 }.Last();
                Console.WriteLine($"Last element: {lastElement}");

                // LINQ Concat Example: Concatenating two lists
                var list1 = new List<int> { 1, 2, 3 };
                var list2 = new List<int> { 4, 5, 6 };
                var concatenatedList = list1.Concat(list2);
                Console.WriteLine("\nConcatenated List:");
                foreach (var item in concatenatedList)
                {
                    Console.WriteLine(item);
                }

                // LINQ SequenceEqual Example
                var isEqual = list1.SequenceEqual(list2);
                Console.WriteLine($"\nList1 equals List2: {isEqual}");

                // LINQ Aggregate Example: Calculate total salary with bonus
                decimal totalSalary = employees.Aggregate<Employee, decimal>(0, (total, e) =>
                {
                    decimal bonus = (e.IsManager == "Yes") ? e.Salary * 0.04m : e.Salary * 0.02m;
                    total += e.Salary + bonus;
                    return total;
                });
                Console.WriteLine($"Total Salary with Bonus: {totalSalary}");

                // LINQ DefaultIfEmpty Example
                var emptyList = new List<int> { };
                var defaultIfEmptyList = emptyList.DefaultIfEmpty(9999);
                Console.WriteLine($"\nDefault if empty: {defaultIfEmptyList.ElementAt(0)}");

                // LINQ Transformation Example: Converting collection to different types
                var employeesAboveSalary = employees.Where(e => e.Salary > 50000).ToList();
                Console.WriteLine("\nEmployees with salary > 50000:");
                foreach (var emp in employeesAboveSalary)
                {
                    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Salary: {emp.Salary}");
                }

                var employeeDictionary = employees.ToDictionary(e => e.Id, e => e.Name);
                Console.WriteLine("\nEmployee Dictionary:");
                foreach (var kvp in employeeDictionary)
                {
                    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private List<Employee> GetEmployeeData()
        {
            return new List<Employee>()
            {
                new Employee() { Id = 1, Name = "John Doe", IsManager = "Yes", Salary = 75000, DepartmentId = 1 },
                new Employee() { Id = 2, Name = "Jane Smith", IsManager = "No", Salary = 55000, DepartmentId = 2 },
                new Employee() { Id = 3, Name = "Samuel Green", IsManager = "Yes", Salary = 80000, DepartmentId = 3 },
                new Employee() { Id = 4, Name = "Emily Johnson", IsManager = "No", Salary = 48000, DepartmentId = 4 },
                new Employee() { Id = 5, Name = "Michael Brown", IsManager = "Yes", Salary = 90000 }
            };
        }

        private List<Department> GetDepartments()
        {
            return new List<Department>()
            {
                new Department() { Id = 1, DepartmentName = "Human Resources"},
                new Department() { Id = 2, DepartmentName = "Finance"},
                new Department() { Id = 3, DepartmentName = "IT"},
                new Department() { Id = 4, DepartmentName = "Marketing"},
                new Department() { Id = 5, DepartmentName = "Sales"}
            };
        }
    }
}
