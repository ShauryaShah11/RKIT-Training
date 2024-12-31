-- Explain Keyword in MySQL

-- Specify the database to use
USE practiceDB;

-- Step 1: Understand a Simple Query Execution
SELECT * FROM Staff;

ALTER TABLE Staff
DROP INDEX idx_salary;

-- Step 2: Analyze a Query with a WHERE Clause
EXPLAIN SELECT * FROM Staff WHERE Salary > 50000;

-- Step 3: Use EXPLAIN with a Join Query
EXPLAIN 
SELECT Staff.FirstName, Transactions.Amount 
FROM Staff 
JOIN Transactions ON Staff.EmployeeID = Transactions.StaffID;

-- Step 4: Use EXPLAIN with an Index
-- Add an index for the Salary column (if not already present)
CREATE INDEX idx_salary ON Staff(Salary);

-- Re-run EXPLAIN to see the impact of the index
EXPLAIN SELECT * FROM Staff WHERE Salary > 50000;

-- Step 5: Understanding the Output
-- Columns in EXPLAIN output
-- id, select_type, table, partitions, type, possible_keys, key, key_len, ref, rows, filtered, Extra
