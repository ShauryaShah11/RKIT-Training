using System;

namespace BaseKeywordExample
{
    /// <summary>
    /// The base class Animal.
    /// </summary>
    public class Animal
    {
        public string Name { get; set; }

        public Animal(string name)
        {
            Name = name;
            Console.WriteLine($"Animal constructor called. Name: {Name}");
        }

        public void MakeSound()
        {
            Console.WriteLine("Animal makes a generic sound.");
        }
    }

    /// <summary>
    /// The derived class Dog inherits from Animal.
    /// </summary>
    public class Dog : Animal
    {
        public string Breed { get; set; }

        public Dog(string name, string breed = "Golden Retriever")
            : base(name) // Calls the base class constructor.
        {
            Breed = breed;
            Console.WriteLine($"Dog constructor called. Breed: {Breed}");
        }

        public void Describe()
        {
            Console.WriteLine($"This is {Name}, a {Breed}.");
        }

        public new void MakeSound()
        {
            base.MakeSound(); // Calls the base class MakeSound method.
            Console.WriteLine("Dog says: Woof! Woof!");
        }
    }
}
