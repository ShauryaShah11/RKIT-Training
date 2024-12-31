-- DDL(Data Definition Language) commands, which are used to define and modify the structure of a database and its objects, such as tables, indexes, and views.

-- Specify the database to use
USE practiceDB;

-- Step 1: Create the Employees table
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50),
    Email VARCHAR(100) UNIQUE,
    HireDate DATE,
    Salary DECIMAL(10 , 2 )
);

-- Step 2: Add a Department column
ALTER TABLE Employees 
ADD Department VARCHAR(100);

-- Step 3: Modify the Salary column to NOT NULL
ALTER TABLE Employees 
MODIFY Salary DECIMAL(10,2) NOT NULL;

-- Step 4: Drop the Department column
ALTER TABLE Employees 
DROP COLUMN Department;

-- Step 5: Rename the Employees table to Staff
RENAME TABLE Employees TO Staff;
