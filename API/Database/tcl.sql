-- TCL commands are used to manage transactions in a database, ensuring data consistency and integrity.
-- These commands allow you to commit or rollback a group of DML operations.

-- Specify the database to use
USE practiceDB;

-- Step 1: Start a transaction and insert data into the Staff table
START TRANSACTION;

-- Insert a new staff member
INSERT INTO Staff (FirstName, LastName, Email, HireDate, Salary) 
VALUES ('John', 'Doe', 'john.doe@example.com', '2024-12-25', 45000.00);

-- Insert another staff member
INSERT INTO Staff (FirstName, LastName, Email, HireDate, Salary) 
VALUES ('Jane', 'Smith', 'jane.smith@example.com', '2024-12-26', 55000.00);

-- Step 2: Roll back the transaction (undo all operations in this transaction)
ROLLBACK;

-- Step 3: Start another transaction and insert data
START TRANSACTION;

-- Insert a new staff member
INSERT INTO Staff (FirstName, LastName, Email, HireDate, Salary) 
VALUES ('Alice', 'Brown', 'alice.brown@example.com', '2024-12-27', 60000.00);

-- Commit the transaction (save all changes permanently)
COMMIT;

-- Step 4: Verify the data in the Staff table
SELECT 
    *
FROM
    Staff;
