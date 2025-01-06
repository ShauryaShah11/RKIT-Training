-- Specify the database to use
USE practiceDB;

-- Step 1: Single-row subquery
-- Find the transaction amount of the transaction with the highest ID
SELECT 
    Amount
FROM
    Transactions
WHERE
    TransactionID = (SELECT 
            MAX(TransactionID)
        FROM
            Transactions);

-- Step 2: Multi-row subquery
-- Find all the StaffIDs who have made transactions greater than 1000
SELECT 
    StaffID, Amount
FROM
    Transactions
WHERE
    Amount > (SELECT 1000);

-- Step 3: Subquery with IN (multi-row subquery)
-- Find all transactions made by Staff members who have a salary greater than 2000
SELECT 
    TransactionID, Amount
FROM
    Transactions
WHERE
    StaffID IN (SELECT 
            EmployeeID
        FROM
            Staff
        WHERE
            Salary > 2000);

-- Step 4: Correlated subquery
-- Find all transactions where the amount is greater than the average transaction amount for that specific Staff member
SELECT 
    TransactionID, Amount
FROM
    Transactions T
WHERE
    Amount > (SELECT 
            AVG(Amount)
        FROM
            Transactions
        WHERE
            StaffID = T.StaffID);

-- Step 5: Subquery in SELECT clause
-- Find the number of transactions for each Staff member along with their total transaction amount
SELECT 
    StaffID,
    (SELECT 
            COUNT(*)
        FROM
            Transactions
        WHERE
            StaffID = S.EmployeeID) AS TotalTransactions,
    (SELECT 
            SUM(Amount)
        FROM
            Transactions
        WHERE
            StaffID = S.EmployeeID) AS TotalAmount
FROM
    Staff S;

-- Step 6: Subquery in FROM clause
-- Find the total transactions for each Staff member by first calculating the total amount in a subquery
SELECT 
    StaffID, TotalAmount
FROM
    (SELECT 
        StaffID, SUM(Amount) AS TotalAmount
    FROM
        Transactions
    GROUP BY StaffID) AS SubQuery;
