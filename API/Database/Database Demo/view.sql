-- Specify the database to use
USE practiceDB;

-- Step 1: Create a view that combines Staff details with their transaction amounts
CREATE VIEW StaffTransactions AS
    SELECT 
        Staff.EmployeeID,
        Staff.FirstName,
        Staff.LastName,
        Transactions.Amount AS TransactionAmount
    FROM
        Staff
            JOIN
        Transactions ON Staff.EmployeeID = Transactions.StaffID;

-- Step 2: Query the view to get Staff transaction details
SELECT 
    *
FROM
    StaffTransactions;
    
CREATE VIEW StaffView AS
    SELECT 
        EmployeeID, FirstName, LastName
    FROM
        Staff;
        
UPDATE StaffView 
SET 
    FirstName = 'John'
WHERE
    EmployeeID = 1;


