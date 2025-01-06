-- Specify the database to use
USE practiceDB;

-- Sort Users table by FirstName in ascending order
-- Retrieves all columns and sorts rows alphabetically by the FirstName column
SELECT 
    *
FROM
    Users
ORDER BY FirstName ASC;

-- Sort Users table by CreatedAt in descending order
-- Retrieves all columns and sorts rows by the CreatedAt timestamp, newest first
SELECT 
    *
FROM
    Users
ORDER BY CreatedAt DESC;

-- Sort Orders table by Amount in ascending order
-- Retrieves all columns and sorts rows from smallest to largest Amount
SELECT 
    *
FROM
    Orders
ORDER BY Amount ASC;

-- Sort Orders table by OrderDate in descending order
-- Retrieves all columns and sorts rows with the latest orders first
SELECT 
    *
FROM
    Orders
ORDER BY OrderDate DESC;

-- Sort Orders table by UserId in ascending order and Amount in descending order
-- Sorts first by UserId (smallest to largest) and then by Amount (largest to smallest) for each UserId
SELECT 
    *
FROM
    Orders
ORDER BY UserId ASC , UserId DESC;

-- Sort Users by FullName (Concatenation of FirstName and LastName) in ascending order
-- Combines FirstName and LastName, then sorts alphabetically
SELECT 
    UserId, CONCAT(FirstName, ' ', LastName) AS FullName, Email
FROM
    Users
ORDER BY FullName ASC;

-- Retrieve the 5 largest orders by Amount
SELECT 
    *
FROM
    Orders
ORDER BY Amount DESC
LIMIT 5;

-- Prioritize orders based on Amount: high-value orders appear first
-- Secondary sorting by OrderDate ensures most recent high-value orders appear first
SELECT 
    *
FROM
    Orders
ORDER BY CASE
    WHEN Amount >= 200 THEN 1
    ELSE 2
END , OrderDate DESC;



