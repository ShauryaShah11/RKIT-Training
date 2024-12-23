using System;

/// <summary>
/// The IAnimal interface defines a method for making animal sounds.
/// The Cat class implements this interface to provide a specific implementation of the MakeSound method.
/// </summary>
public class Cat : IAnimal
{
    /// <summary>
    /// The MakeSound method outputs the sound a cat makes.
    /// </summary>
    public void MakeSound()
    {
        Console.WriteLine("Meow Meow");
    }
}

/// <summary>
/// The Cow class implements the IAnimal interface to provide a specific implementation of the MakeSound method.
/// </summary>
public class Cow : IAnimal
{
    /// <summary>
    /// The MakeSound method outputs the sound of a cow makes.
    /// </summary>
    public void MakeSound()
    {
        Console.WriteLine("Moo Moo");
    }
}