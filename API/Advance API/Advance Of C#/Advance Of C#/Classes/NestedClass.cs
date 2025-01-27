using System;

namespace Advance_Of_C_.Classes
{
    /// <summary>
    /// The OuterClass contains an inner class called InnerClass.
    /// </summary>
    public class OuterClass
    {
        /// <summary>
        /// The InnerClass defines a method to display a message to the console.
        /// </summary>
        public class InnerClass
        {
            /// <summary>
            /// Displays a message indicating that this is the Inner Class.
            /// </summary>
            public void DisplayMessage()
            {
                Console.WriteLine("This is Inner Class");
            }
        }
    }
}
