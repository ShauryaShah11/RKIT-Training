using System;

/// <summary>
/// The Animal class is the parent class, and Pig and Dog are child classes.
/// These classes are used to demonstrate the concept of polymorphism.
/// </summary>
class Animal  // Base class (parent) 
{
    /// <summary>
    /// The AnimalSound method outputs a generic animal sound.
    /// This method can be overridden by derived classes to provide specific animal sounds.
    /// </summary>
    public virtual void AnimalSound()
    {
        Console.WriteLine("The animal makes a sound");
    }
}

/// <summary>
/// The Pig class is a derived class (child) that inherits from the Animal class.
/// It demonstrates polymorphism by providing a specific implementation of the AnimalSound method.
/// </summary>
class Pig : Animal  // Derived class (child) 
{
    /// <summary>
    /// The AnimalSound method outputs the sound a pig makes.
    /// This method overrides the base class method to provide a specific implementation.
    /// </summary>
    public override void AnimalSound()
    {
        Console.WriteLine("The pig says: wee wee");
    }
}

/// <summary>
/// The Dog class is a derived class (child) that inherits from the Animal class.
/// It demonstrates polymorphism by providing a specific implementation of the AnimalSound method.
/// </summary>
class Dog : Animal  // Derived class (child) 
{
    /// <summary>
    /// The AnimalSound method outputs the sound a dog makes.
    /// This method overrides the base class method to provide a specific implementation.
    /// </summary>
    public override void AnimalSound()
    {
        Console.WriteLine("The dog says: bow wow");
    }
}