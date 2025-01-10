using System.Collections.Generic;

namespace Advance_Of_C_.Extension_Methods
{
    public static class EnumerableCollectionExtensionMethods
    {
        public static IEnumerable<Employee> GetHighSalariedEmployee(this IEnumerable<Employee> employees, int salaryThreshold)
        {
            foreach (var emp in employees)
            {
                if (emp.Salary >= salaryThreshold)
                {
                    yield return emp;
                }
            }
        }
    }
}
