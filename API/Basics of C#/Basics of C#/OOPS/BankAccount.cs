using System;

public class BankAccount
{
    #region Private Members
    private double balance; // Encapsulated field

    #endregion

    #region Public Methods
    public double GetBalance() // Public method to access private field
    {
        return balance;
    }

    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount}, New Balance: {balance}");
        }
    }
    #endregion
}
