using System.Collections.Generic;

namespace Advance_Of_C_.LINQOperations
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsManager { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
    }
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x != null && y != null &&
                   x.Id == y.Id &&
                   x.Name == y.Name &&
                   x.IsManager == y.IsManager &&
                   x.Salary == y.Salary &&
                   x.DepartmentId == y.DepartmentId;
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Id.GetHashCode();
        }
    }

}
