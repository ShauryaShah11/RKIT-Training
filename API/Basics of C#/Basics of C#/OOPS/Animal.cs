using System;

class Animal  // Base class (parent) 
{
    public void AnimalSound()
    {
        Console.WriteLine("The animal makes a sound");
    }
}

class Pig : Animal  // Derived class (child) 
{
    public new void AnimalSound()
    {
        Console.WriteLine("The pig says: wee wee");
    }
}

class Dog : Animal  // Derived class (child) 
{
    public new void AnimalSound()
    {
        Console.WriteLine("The dog says: bow wow");
    }
}