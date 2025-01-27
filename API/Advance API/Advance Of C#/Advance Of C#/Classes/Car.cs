using System;

namespace Advance_Of_C_.Classes
{
    /// <summary>
    /// Represents a Car with properties for Make and Model and a method to simulate driving.
    /// This is a normal class used to model a basic Car object.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Gets or sets the Make of the car (e.g., Ford, Toyota).
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Gets or sets the Model of the car (e.g., Mustang, Corolla).
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Simulates driving the car by printing a message to the console.
        /// </summary>
        public void Drive()
        {
            Console.WriteLine("The Car is Driving...");
        }
    }
}
