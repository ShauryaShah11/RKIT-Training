using System;

public class Vehicle
{
    public int Speed { get; set; }

    public void Move()
    {
        Console.WriteLine("Vehicle is moving.");
    }
}
public class Car : Vehicle
{
    // Fields
    private string _brand;
    private int _speed;

    // Constructor
    public Car(string brandName, int initialSpeed)
    {
        _brand = brandName;
        _speed = initialSpeed;
    }

    // Method
    public void Accelerate(int increment)
    {
        _speed += increment;
        Console.WriteLine($"{_brand} accelerated to {_speed} km/h.");
    }
}
