-- Specify the database to use
USE practiceDB;

SELECT 
    Staff.EmployeeID,
    Staff.FirstName,
    Staff.LastName
    -- Transactions.Amount,
--     Transactions.TransactionDate
FROM
    Staff
        LEFT JOIN
    Staff ON Staff.EmployeeID = Staff.EmployeeID;