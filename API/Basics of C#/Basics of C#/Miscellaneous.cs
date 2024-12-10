using System;

/// <summary>
/// The Miscellaneous class demonstrates various concepts in C#.
/// It includes methods such as Boxing, Unboxing, and StringToArray.
/// </summary>
class Miscellaneous
{
    /// <summary>
    /// The Boxing method demonstrates boxing in C#.
    /// Boxing converts a value type to a reference type.
    /// It stores the value type in an object type implicitly.
    /// </summary>
    public void Boxing()
    {
        int num = 10; // Value type
        object obj = num; // Boxing: converting value type to reference type
        Console.WriteLine(obj); // Output: 10
        Console.WriteLine(string.Format("{0} Type is : {1}", obj, obj.GetType())); // Output: 10 Type is : System.Int32
    }

    /// <summary>
    /// The UnBoxing method demonstrates unboxing in C#.
    /// Unboxing converts a reference type to a value type.
    /// It extracts the value type from the object type explicitly.
    /// </summary>
    public void UnBoxing()
    {
        object obj = 20; // Reference type containing a value type
        try
        {
            int num = (int)obj; // Unboxing: converting reference type back to value type
            Console.WriteLine(string.Format("{0} Type is : {1}", num, num.GetType())); // Output: 20 Type is : System.Int32
        }
        catch (InvalidCastException ex)
        {
            Console.WriteLine("Unboxing failed: " + ex.Message);
        }
    }

    /// <summary>
    /// The StringToArray method demonstrates converting a string to an array in C#.
    /// </summary>
    public void StringToArray()
    {
        string str = "Ahmedabad,Surat,Vadodara,Rajkot";
        string[] cities = str.Split(','); // Split method will split string based on delimiter

        for (int i = 0; i < cities.Length; i++)
        {
            Console.Write(cities[i] + " ");
        }
    }
}

