using System.Text.RegularExpressions;

namespace AdvanceC_FinalDemo.Extension_Methods
{
    public static class StringExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
    }
}