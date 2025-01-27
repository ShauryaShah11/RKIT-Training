using System;

namespace Advance_Of_C_.Classes
{
    /// <summary>
    /// The Singleton class ensures that only one instance of the class can exist.
    /// </summary>
    public class Singleton
    {
        // The single instance of the Singleton class.
        public static Singleton instance;

        // Private constructor to prevent instantiation from outside.
        private Singleton() { }

        /// <summary>
        /// The Instance property provides access to the single instance of the Singleton class.
        /// If the instance does not exist, it is created.
        /// </summary>
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    return new Singleton();
                }
                return instance;
            }
        }

        /// <summary>
        /// Displays a message indicating that the Singleton instance is being used.
        /// </summary>
        public void ShowMessage()
        {
            Console.WriteLine("Singleton Instance");
        }
    }
}
