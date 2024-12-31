-- Specify the database to use
USE practiceDB;

-- Step 1: Combine the list of all staff who have made transactions and the list of all staff who have not made any transactions
-- Select staff who have made transactions
SELECT 
    EmployeeID, FirstName, LastName
FROM
    Staff
WHERE
    EmployeeID IN (SELECT DISTINCT
            StaffID
        FROM
            Transactions) 
UNION SELECT 
    EmployeeID, FirstName, LastName
FROM
    Staff
WHERE
    EmployeeID NOT IN (SELECT DISTINCT
            StaffID
        FROM
            Transactions);
            
            
SELECT 
	group_concat(EmployeeId)
FROM
	staff;
    
