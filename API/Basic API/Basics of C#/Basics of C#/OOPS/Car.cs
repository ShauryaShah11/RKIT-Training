using System;

/// <summary>
/// The Vehicle class is the parent class, and Car is a child class.
/// The Car class inherits from the Vehicle class.
/// It helps to demonstrate the concept of simple inheritance.
/// In Inhertiance, the child class inherits the properties and methods of the parent class.
/// </summary>
public class Vehicle
{
    public int Speed { get; set; }

    public void Move()
    {
        Console.WriteLine("Vehicle is moving.");
    }
}

/// <summary>
/// The Car class is a derived class (child) that inherits from the Vehicle class.
/// </summary>
public class Car : Vehicle
{
    // Fields
    #region Private Members
    private string _brand;
    private int _speed;

    #endregion

    #region Constructors
    // Constructor
    public Car(string brandName, int initialSpeed)
    {
        _brand = brandName;
        _speed = initialSpeed;
    }
    #endregion

    // Method
    #region Public method
    public void Accelerate(int increment)
    {
        _speed += increment;
        Console.WriteLine($"{_brand} accelerated to {_speed} km/h.");
    }
    #endregion 
}
