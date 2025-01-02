-- Specify the database to use
USE practiceDB;

SELECT 
    Sta1.EmployeeID,
    Sta1.FirstName,
    Sta1.LastName
    -- Uncomment if you need transactions details
    -- Tra.Amount,
    -- Tra.TransactionDate
FROM
    Staff AS Sta1
        LEFT JOIN
    Staff AS Sta2 ON Sta1.EmployeeID = Sta2.EmployeeID;
