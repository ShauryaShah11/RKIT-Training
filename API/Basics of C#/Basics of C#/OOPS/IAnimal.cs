using System;

/// <summary>
/// The IAnimal interface defines a method for making animal sounds.
/// It includes the MakeSound method.
/// </summary>
public interface IAnimal
{
    void MakeSound();
}

/// <summary>
/// The IMovable interface defines a method for movement.
/// It includes the Move method.
/// </summary>
public interface IMovable
{
    void Move();
}

/// <summary>
/// The Dog class implements both IAnimal and IMovable interfaces.
/// </summary>
public class Horse : IAnimal, IMovable
{
    public void MakeSound()
    {
        Console.WriteLine("Horse Sound");
    }

    public void Move()
    {
        Console.WriteLine("Horse is running...");
    }
}