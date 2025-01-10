using System.Collections.Generic;

namespace Advance_Of_C_.Extension_Methods
{
    public static class Data
    {
        public static List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "John Doe", IsManager = "Yes", Salary = 75000, DepartmentId = 1 },
                new Employee() { Id = 2, Name = "Jane Smith", IsManager = "No", Salary = 55000, DepartmentId = 2 },
                new Employee() { Id = 3, Name = "Samuel Green", IsManager = "Yes", Salary = 80000, DepartmentId = 3 },
                new Employee() { Id = 4, Name = "Emily Johnson", IsManager = "No", Salary = 48000, DepartmentId = 4 },
                //new Employee() { Id = 5, Name = "Michael Brown", IsManager = "Yes", Salary = 90000, DepartmentId = 5 }
            };
            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>()
            {
                new Department() { Id = 1, DepartmentName = "Human Resources", EmployeeId = 1 },
                new Department() { Id = 2, DepartmentName = "Finance", EmployeeId = 2 },
                new Department() { Id = 3, DepartmentName = "IT", EmployeeId = 3 },
                new Department() { Id = 4, DepartmentName = "Marketing", EmployeeId = 4 },
                new Department() { Id = 5, DepartmentName = "Sales", EmployeeId = 5 }
            };
            return departments;
        }
    }
}
