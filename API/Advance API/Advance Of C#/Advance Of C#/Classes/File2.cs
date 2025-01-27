using System;

namespace Advance_Of_C_.Classes
{
    /// <summary>
    /// Contains additional properties and methods for Employee.
    /// </summary>
    public partial class Employee
    {
        public int Age { get; set; }

        public void DisplayDetails()
        {
            Console.WriteLine($"Employee Name: {Name}, Age: {Age}");
        }
    }
}
