-- Aggregate functions are used to perform calculations on multiple rows of a table's column and return a single value.

-- Specify the database to use
USE practiceDB;

-- Step 1: Create a new table for transactions
CREATE TABLE Transactions (
    TransactionID INT PRIMARY KEY AUTO_INCREMENT,
    StaffID INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    TransactionDate DATE NOT NULL,
    FOREIGN KEY (StaffID) REFERENCES Staff(EmployeeID) ON DELETE CASCADE ON UPDATE CASCADE
);

-- Step 2: Insert data into the Transactions table
INSERT INTO Transactions (StaffID, Amount, TransactionDate) VALUES
(1, 1500.00, '2024-12-20'),
(1, 2000.00, '2024-12-21'),
(2, 500.00, '2024-12-20'),
(2, 700.00, '2024-12-22'),
(3, 1200.00, '2024-12-20'),
(3, 1300.00, '2024-12-22'),
(1, 800.00, '2024-12-21');

-- Step 3: Count the total number of transactions
SELECT 
    COUNT(*) AS TotalTransactions
FROM
    Transactions;

SELECT 
    COUNT(StaffId) AS TotalTransactions
FROM
    Transactions;

-- Step 4: Calculate the total amount of all transactions
SELECT 
    SUM(Amount) AS TotalAmount
FROM
    Transactions;

-- Step 5: Find the average transaction amount
SELECT 
    AVG(Amount) AS AverageAmount
FROM
    Transactions;

-- Step 6: Retrieve the minimum transaction amount
SELECT 
    MIN(Amount) AS MinimumAmount
FROM
    Transactions;

-- Step 7: Retrieve the maximum transaction amount
SELECT 
    MAX(Amount) AS MaximumAmount
FROM
    Transactions;

-- Step 8: Group by StaffID to get total transactions per staff
SELECT 
    StaffID,
    COUNT(TransactionID) AS TransactionsCount,
    SUM(Amount) AS TotalAmount
FROM
    Transactions
GROUP BY StaffID;

-- Step 9: Group by TransactionDate to calculate daily totals
SELECT 
    TransactionDate,
    COUNT(TransactionID) AS TransactionsCount,
    SUM(Amount) AS DailyTotal
FROM
    Transactions
GROUP BY TransactionDate;

-- Step 10: Filter groups using HAVING (e.g., staff with total transactions > 2000)
SELECT 
    StaffID, SUM(Amount) AS TotalAmount
FROM
    Transactions
GROUP BY StaffID
HAVING SUM(Amount) > 2000;

