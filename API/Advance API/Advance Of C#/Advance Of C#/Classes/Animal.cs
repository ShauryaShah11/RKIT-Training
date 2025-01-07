using System;

namespace Advance_Of_C_.Classes
{
    public abstract class Animal
    {
        public abstract void MakeSound();

        public void Breathe()
        {
            Console.WriteLine("Breathing...");
        }
    }
    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Bark");
        }
    }
}
