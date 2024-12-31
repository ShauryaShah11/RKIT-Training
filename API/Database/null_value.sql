-- Specify the database to use
USE practiceDB;

-- Example: Inserting Null Values into Users table
INSERT INTO Users (FirstName, LastName, Email) 
VALUES ('John', 'Doe', NULL);  -- Email is NULL

-- Example: Inserting Null Values into Orders table
INSERT INTO Orders (OrderDate, Amount, UserId) 
VALUES ('2024-12-25', 150.00, 1);  -- No NULL values here but an example of a valid insert

-- Example: Updating Users table to set NULL Email
UPDATE Users 
SET 
    Email = NULL
WHERE
    UserID = 1;

-- Example: Selecting rows with NULL values (Checking NULL Email)
SELECT 
    *
FROM
    Users
WHERE
    Email IS NULL;

-- Example: Selecting rows where Email is not NULL
SELECT 
    *
FROM
    Users
WHERE
    Email IS NOT NULL;

-- Example: Using COALESCE to replace NULL with a default value in Orders table
SELECT 
    OrderId, COALESCE(Amount, "shaurya") AS OrderAmount
FROM
    Orders;
-- This will replace NULL Amount with 0 (if any NULL exists)
