using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_Of_C_.Extension_Methods
{
    public static class IntExtensions
    {
        public static void Increment(this int num) => num++;

        public static void RefIncrement(ref this int num) => num++;
        
    }
}
