-- DQL commands are used to query and retrieve data from a database. The main command used in DQL is SELECT.

-- Specify the database to use
USE practiceDB;

-- Step 1: Retrieve all columns and rows from the Staff table
SELECT 
    *
FROM
    Staff;

-- Step 2: Retrieve specific columns (FirstName, Email) from the Staff table
SELECT 
    FirstName, Email
FROM
    Staff;

-- Step 3: Filter records where Salary is greater than 50,000
SELECT 
    *
FROM
    Staff
WHERE
    Salary > 50000;

-- Step 4: Filter records where HireDate is after '2024-12-25'
SELECT 
    *
FROM
    Staff
WHERE
    HireDate > '2024-12-25';

-- Step 5: Sort the result by Salary in descending order
SELECT 
    *
FROM
    Staff
ORDER BY Salary DESC;

-- Step 6: Retrieve records with a specific Email
SELECT 
    *
FROM
    Staff
WHERE
    Email = 'alice.brown@example.com';

-- Step 7: Limit the number of rows in the result to 2
SELECT 
    *
FROM
    Staff
LIMIT 2;

-- Step 8: Use an alias for the Salary column
SELECT 
    FirstName, Salary AS MonthlyIncome
FROM
    Staff;

-- Step 9: Combine conditions using AND and OR
SELECT 
    *
FROM
    Staff
WHERE
    Salary > 50000
        AND HireDate > '2024-12-25';
