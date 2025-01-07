using System;

namespace Advance_Of_C_.Classes
{
    public class Singleton
    {
        public static Singleton instance;
        private Singleton() { }
        public static Singleton Insatnce
        {
            get
            {
                if(instance == null)
                {
                    return new Singleton();
                }
                return instance;
            }
        }
        public void ShowMessage()
        {
            Console.WriteLine("Singleton Instance");
        }
    }
}
