using System;

namespace Advance_Of_C_.Classes
{
    /// <summary>
    /// The Animal class is an abstract class that represents a general animal.
    /// It contains an abstract method MakeSound, which must be implemented by derived classes,
    /// and a concrete method Breathe, which all animals share.
    /// </summary>
    public abstract class Animal
    {
        /// <summary>
        /// Abstract method that forces derived classes to implement their own version of making sound.
        /// </summary>
        public abstract void MakeSound();

        /// <summary>
        /// Method that simulates the action of breathing for all animals.
        /// </summary>
        public void Breathe()
        {
            Console.WriteLine("Breathing...");
        }
    }

    /// <summary>
    /// The Dog class is a derived class of Animal that provides its own implementation of MakeSound.
    /// </summary>
    public class Dog : Animal
    {
        /// <summary>
        /// Overrides the MakeSound method to provide the specific sound made by a dog (Bark).
        /// </summary>
        public override void MakeSound()
        {
            Console.WriteLine("Bark");
        }
    }
}
