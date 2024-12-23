using System;

namespace MuiltiLevelInheritance
{
    /// <summary>
    /// The Animal class is the base class.
    /// It helps to demonstrate the concept of inheritance.
    /// </summary>
    public class Animal
    {
        /// <summary>
        /// The Eat method outputs a message indicating that the animal is eating.
        /// </summary>
        public void Eat()
        {
            Console.WriteLine("The animal is eating.");
        }
    }

    /// <summary>
    /// The Mammal class is an intermediate class that inherits from Animal.
    /// </summary>
    public class Mammal : Animal
    {
        /// <summary>
        /// The Breathe method outputs a message indicating that the mammal is breathing.
        /// </summary>
        public void Breathe()
        {
            Console.WriteLine("The mammal is breathing.");
        }
    }

    /// <summary>
    /// The Dog class is a derived class that inherits from Mammal.
    /// </summary>
    public class Dog : Mammal
    {
        /// <summary>
        /// The Bark method outputs the sound a dog makes.
        /// </summary>
        public void Bark()
        {
            Console.WriteLine("The dog says: bow wow");
        }
    }
}