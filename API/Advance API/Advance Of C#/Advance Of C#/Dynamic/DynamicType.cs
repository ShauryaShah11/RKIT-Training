using System;

namespace Advance_Of_C_.Dynamic
{
    public class DynamicType
    {
        public dynamic DynamicDemo()
        {
            dynamic value1;
            value1 = 10;
            dynamic value2 = 20;
            Add(value1, value2);
            Console.WriteLine("Value1 + Value2 = " + (value1 + value2));
            value1 = "shaurya";
            Console.WriteLine(value1);

            return value1 + value2;
        }

        public void Add(int a, int b)
        {
            Console.WriteLine("A is :" + a);
            Console.WriteLine("B is :" + b);
        }
    }
}
