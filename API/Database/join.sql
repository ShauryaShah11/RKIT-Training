-- Joins in SQL are used to combine rows from two or more tables based on a related column between them.

-- Specify the database to use
USE practiceDB;

-- Step 1: INNER JOIN - Fetch staff and their transactions (only those who have made transactions)
SELECT 
    Sta.EmployeeID,
    Sta.FirstName,
    Sta.LastName,
    Tra.Amount,
    Tra.TransactionDate
FROM
    Staff AS Sta
        INNER JOIN
    Transactions AS Tra ON Sta.EmployeeID = Tra.StaffID;

-- Step 2: LEFT JOIN - Fetch all staff and their transactions (including staff with no transactions)
SELECT 
    Sta.EmployeeID,
    Sta.FirstName,
    Sta.LastName,
    Tra.Amount,
    Tra.TransactionDate
FROM
    Staff AS Sta
        LEFT JOIN
    Transactions AS Tra ON Sta.EmployeeID = Tra.StaffID;

-- Step 3: RIGHT JOIN - Fetch all transactions and their staff details (including transactions without staff)
SELECT 
    Tra.TransactionID,
    Tra.Amount,
    Tra.TransactionDate,
    Sta.FirstName,
    Sta.LastName
FROM
    Transactions AS Tra
        RIGHT JOIN
    Staff AS Sta ON Tra.StaffID = Sta.EmployeeID;

-- Step 4: FULL OUTER JOIN - Simulate FULL OUTER JOIN by combining LEFT JOIN and RIGHT JOIN using UNION
SELECT 
    Sta.EmployeeID,
    Sta.FirstName,
    Sta.LastName,
    Tra.Amount,
    Tra.TransactionDate
FROM
    Staff AS Sta
        LEFT JOIN
    Transactions AS Tra ON Sta.EmployeeID = Tra.StaffID 
UNION 
SELECT 
    Sta.EmployeeID,
    Sta.FirstName,
    Sta.LastName,
    Tra.Amount,
    Tra.TransactionDate
FROM
    Transactions AS Tra
        RIGHT JOIN
    Staff AS Sta ON Tra.StaffID = Sta.EmployeeID;

-- Step 5: CROSS JOIN - Generate a Cartesian product (every combination of staff and transactions)
SELECT 
    Sta.EmployeeID,
    Sta.FirstName,
    Sta.LastName,
    Tra.Amount,
    Tra.TransactionDate
FROM
    Staff AS Sta
        CROSS JOIN
    Transactions AS Tra; 
