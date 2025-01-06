using System;

/// <summary>
/// The ScopeModifier class demonstrates the scope of different access modifiers in C#.
/// </summary>
class ScopeModifier
{
    #region Private field
    // Private field: Only accessible within this class
    private int _age = 25;
    #endregion

    #region Public field
    // Public field: Universally available
    public int value = 10;
    #endregion

    #region Protected field
    // Protected field: Available in derived classes
    protected int protectedValue = 10;
    #endregion

    #region Internal field
    // Internal field: Accessible within the same assembly
    internal int internalValue = 10;
    #endregion

    /// <summary>
    /// The Display method demonstrates the usage of scope modifiers in C#.
    /// </summary>
    public void Display()
    {
        // Block-scoped variable
        int number = 5;
        if (number > 0)
        {
            // Accessible only within this block
            int count = 10;
            Console.WriteLine(count);
        }
        // Console.WriteLine(count); // Error: 'count' is not accessible here
    }

    /// <summary>
    /// The ShowAge method demonstrates access to a private field within the class.
    /// </summary>
    public void ShowAge()
    {
        // Accessible here
        Console.WriteLine(_age);
    }
}

/// <summary>
/// The DerivedClass class demonstrates access to protected members in a derived class.
/// </summary>
class DerivedClass : ScopeModifier
{
    /// <summary>
    /// The Display method demonstrates access to protected members in a derived class.
    /// </summary>
    public new void Display()
    {
         //Console.WriteLine(_age); // Error: '_age' is not accessible here
        // Accessible here
        Console.WriteLine(protectedValue);
    }
}