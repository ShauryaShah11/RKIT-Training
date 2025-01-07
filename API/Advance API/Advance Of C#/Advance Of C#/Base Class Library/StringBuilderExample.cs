using System;
using System.Text;

namespace Advance_Of_C_.Base_Class_Library
{
    class StringBuilderExample
    {
        public void DemonstrateStringBuilder()
        {
            StringBuilder sb = new StringBuilder();

            // Efficiently appending to the string
            sb.Append("Hello");
            sb.Append(" ");
            sb.Append("World");

            Console.WriteLine("Built String: " + sb.ToString());
        }
    }
}
