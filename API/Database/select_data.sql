-- Specify the database to use
-- This ensures all subsequent operations are performed within the 'practiceDB' database
USE practiceDB;

-- Select all data from the Users table
-- Retrieve all columns and rows in the Users table
SELECT 
    *
FROM
    Users;

-- Select all data from the Orders table
-- Retrieve all columns and rows in the Orders table
SELECT 
    *
FROM
    Orders;

-- Select specific columns from the Users table
-- Retrieve the UserId, the full name (combination of FirstName and LastName), and the Email
-- CONCAT function is used to combine FirstName and LastName, separated by a space
SELECT 
    UserId, CONCAT(FirstName, ' ', LastName) AS FullName, Email
FROM
    Users;

-- Select specific row from the Users table
-- Retrieve all columns for the user whose UserId equals 1
SELECT 
    *
FROM
    Users
WHERE
    UserId = 1;

-- Select specific rows from the Orders table
-- Retrieve all columns for orders where the Amount is greater than 200
SELECT 
    *
FROM
    Orders
WHERE
    Amount > 200;

-- Select specific rows from the Orders table
-- Retrieve all columns for orders placed by the user with UserId equals 1
SELECT 
    *
FROM
    Orders
WHERE
    UserId = 1;
