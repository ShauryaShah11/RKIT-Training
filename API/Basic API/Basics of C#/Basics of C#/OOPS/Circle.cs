using System;

/// <summary>
/// The Shape class is an abstract class that defines a method for calculating the area of a shape.
/// It helps to demonstate the concept of abstraction.
/// Abstarct classes cannot be instantiated, but can be used as base classes for other classes.
/// </summary>
public abstract class Shape
{
    public abstract double GetArea();
}

/// <summary>
/// The Circle class is a derived class (child) that inherits from the Shape class.
/// It implements the GetArea method to calculate the area of a circle.
/// </summary>
public class Circle : Shape
{
    #region Public Members
    private double _radius;

    #endregion

    #region Constructors
    public Circle(double r)
    {
       _radius = r;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// The GetArea method calculates the area of a circle using the formula: πr^2.
    /// </summary>
    /// <returns>It returns the area of circle value in double datatypes</returns>
    public override double GetArea()
    {
        return Math.PI * _radius * _radius;
    }
    #endregion

}
