using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_Of_C_.Extension_Methods
{
    public static class Extension
    {
        public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
        {
            List<T> filterdList = new List<T>();

            foreach (T record in records)
            {
                if(func(record))
                {
                    filterdList.Add(record);
                }
            }
            return filterdList;
        }
    }
}
