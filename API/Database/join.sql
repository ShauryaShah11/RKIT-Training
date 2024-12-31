-- Joins in SQL are used to combine rows from two or more tables based on a related column between them.

-- Specify the database to use
USE practiceDB;

-- Step 1: INNER JOIN - Fetch staff and their transactions (only those who have made transactions)
SELECT 
    Staff.EmployeeID,
    Staff.FirstName,
    Staff.LastName,
    Transactions.Amount,
    Transactions.TransactionDate
FROM
    Staff
        INNER JOIN
    Transactions ON Staff.EmployeeID = Transactions.StaffID;

-- Step 2: LEFT JOIN - Fetch all staff and their transactions (including staff with no transactions)
SELECT 
    Staff.EmployeeID,
    Staff.FirstName,
    Staff.LastName,
    Transactions.Amount,
    Transactions.TransactionDate
FROM
    Staff
        LEFT JOIN
    Transactions ON Staff.EmployeeID = Transactions.StaffID;

-- Step 3: RIGHT JOIN - Fetch all transactions and their staff details (including transactions without staff)
SELECT 
    Transactions.TransactionID,
    Transactions.Amount,
    Transactions.TransactionDate,
    Staff.FirstName,
    Staff.LastName
FROM
    Transactions
        RIGHT JOIN
    Staff ON Transactions.StaffID = Staff.EmployeeID;

-- Step 4: FULL OUTER JOIN - Simulate FULL OUTER JOIN by combining LEFT JOIN and RIGHT JOIN using UNION
SELECT 
    Staff.EmployeeID,
    Staff.FirstName,
    Staff.LastName,
    Transactions.Amount,
    Transactions.TransactionDate
FROM
    Staff
        LEFT JOIN
    Transactions ON Staff.EmployeeID = Transactions.StaffID 
UNION SELECT 
    Staff.EmployeeID,
    Staff.FirstName,
    Staff.LastName,
    Transactions.Amount,
    Transactions.TransactionDate
FROM
    Transactions
        LEFT JOIN
    Staff ON Transactions.StaffID = Staff.EmployeeID;

-- Step 5: CROSS JOIN - Generate a Cartesian product (every combination of staff and transactions)
SELECT 
    Staff.EmployeeID,
    Staff.FirstName,
    Staff.LastName,
    Transactions.Amount,
    Transactions.TransactionDate
FROM
    Staff
        CROSS JOIN
    Transactions; 
    
