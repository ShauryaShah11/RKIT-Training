using System;

/// <summary>
/// The BankAccount class demonstrates encapsulation by hiding the balance field and providing public methods to access and modify it.
/// </summary>
public class BankAccount
{
    #region Private Members
    // Encapsulated field to store the balance
    private double _balance;
    #endregion 

    #region Public Methods
    /// <summary>
    /// The GetBalance method returns the current balance of the bank account.
    /// </summary>
    /// <returns>The balance value in double datatype.</returns>
    public double GetBalance() // Public method to access private field
    {
        return _balance;
    }

    /// <summary>
    /// The Deposit method allows the user to deposit a specified amount into the bank account.
    /// </summary>
    /// <param name="amount">The amount to be deposited into the bank account.</param>
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            _balance += amount;
            Console.WriteLine($"Deposited {amount}, New Balance: {_balance}");
        }
        else
        {
            Console.WriteLine("Deposit amount must be positive.");
        }
    }

    /// <summary>
    /// The Withdraw method allows the user to withdraw a specified amount from the bank account.
    /// </summary>
    /// <param name="amount">The amount to be withdrawn from the bank account.</param>
    public void Withdraw(double amount)
    {
        if (amount > 0 && amount <= _balance)
        {
            _balance -= amount;
            Console.WriteLine($"Withdrew {amount}, New Balance: {_balance}");
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount.");
        }
    }

    #endregion 
}

