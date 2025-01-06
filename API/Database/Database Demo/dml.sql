-- DML commands are used to manipulate data within tables. These include INSERT, UPDATE, DELETE, and SELECT statements. They deal with the actual data stored in the database.

-- Specify the database to use
USE practiceDB; 

-- Step 1: Insert three employees
INSERT INTO Staff (FirstName, LastName, Email, HireDate, Salary) 
VALUES 
('David', 'Clark', 'david.clark@example.com', '2023-02-15', 60000.00),
('Emma', 'Watson', 'emma.watson@example.com', '2022-11-10', 65000.00),
('Oliver', 'Stone', 'oliver.stone@example.com', '2024-01-01', 58000.00);

-- Step 2: Update the Salary of Emma Watson
UPDATE Staff 
SET 
    Salary = 70000.00
WHERE
    Email = 'emma.watson@example.com';

-- Step 3: Delete the record of David Clark
DELETE FROM Staff 
WHERE
    Email = 'david.clark@example.com';

-- Step 4: Select all remaining records
SELECT 
    *
FROM
    Staff;
