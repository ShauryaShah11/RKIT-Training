using System;

namespace Advance_Of_C_.Extension_Methods
{
    public static class StringExtensions
    {
        public static int WordCount(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }
            return str.Split(new char[] { ' ', ',', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
