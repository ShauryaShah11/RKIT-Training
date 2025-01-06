-- Specify the database to use
USE practiceDB;

-- Alter the field name using CHANGE
-- Rename 'UserId' to 'CustomerId' and change its datatype to INT with NOT NULL constraint
ALTER TABLE Orders
CHANGE UserId CustomerId INT NOT NULL;

-- Modify the datatype or size of an existing column without changing its name
-- For example, change the size of 'Amount' to DECIMAL(10, 2) for better precision
ALTER TABLE Orders
MODIFY Amount DECIMAL(10, 2);

-- Modify the datatype or size of a VARCHAR column
-- For example, increase the size of 'FirstName' in the Users table to 100 characters
ALTER TABLE Users
MODIFY FirstName VARCHAR(100);

-- Drop the existing unique constraint on Email before renaming
ALTER TABLE Users DROP INDEX Email;

-- Rename the column and reapply the UNIQUE constraint
ALTER TABLE Users
CHANGE Email UserEmail VARCHAR(150) UNIQUE;

