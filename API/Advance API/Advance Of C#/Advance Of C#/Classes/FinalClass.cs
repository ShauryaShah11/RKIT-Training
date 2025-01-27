using System;

namespace Advance_Of_C_.Classes
{
    /// <summary>
    /// The FinalClass is a sealed class that cannot be inherited.
    /// </summary>
    public sealed class FinalClass
    {
        /// <summary>
        /// This method prints a message to the console indicating that it is a final class.
        /// </summary>
        public void FinalMessage()
        {
            Console.WriteLine("This is Final Class");
        }
    }
    //public class DerivedClass : FinalClass
    //{
    //}
}
