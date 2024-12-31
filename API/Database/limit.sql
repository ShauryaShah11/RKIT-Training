-- The LIMIT clause is used to restrict the number of rows returned by a query. It helps in handling large datasets and is commonly used for pagination or sampling.

-- Specify the database to use
USE practiceDB;

-- Step 1: Retrieve the first 3 rows from the Staff table
SELECT 
    *
FROM
    Staff
LIMIT 3;

-- Step 2: Retrieve 2 rows starting from the 3rd row
SELECT 
    *
FROM
    Staff
LIMIT 2 OFFSET 2;

-- Step 3: Retrieve the top 2 highest-paid employees
SELECT 
    *
FROM
    Staff
ORDER BY Salary DESC
LIMIT 2;

-- Step 4: Retrieve only one row where Salary is greater than 50,000
SELECT 
    *
FROM
    Staff
WHERE
    Salary > 50000
LIMIT 1;

-- Step 5: Paginate data: Retrieve 5 rows for the first page
SELECT 
    *
FROM
    Staff
LIMIT 5 OFFSET 0;

-- Step 6: Paginate data: Retrieve 5 rows for the second page
SELECT 
    *
FROM
    Staff
LIMIT 5 OFFSET 5;

SELECT 
    *
FROM
    Staff
LIMIT 5, 10;
