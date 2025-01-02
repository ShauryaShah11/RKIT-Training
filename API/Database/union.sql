-- Specify the database to use
USE practiceDB;

-- Step 1: Combine the list of all staff who have made transactions and the list of all staff who have not made any transactions
-- Select staff who have made transactions
SELECT 
    Sta.EmployeeID, Sta.FirstName, Sta.LastName
FROM
    Staff AS Sta
WHERE
    Sta.EmployeeID IN (SELECT DISTINCT
            Tra.StaffID
        FROM
            Transactions AS Tra) 
UNION 
SELECT 
    Sta.EmployeeID, Sta.FirstName, Sta.LastName
FROM
    Staff AS Sta
WHERE
    Sta.EmployeeID NOT IN (SELECT DISTINCT
            Tra.StaffID
        FROM
            Transactions AS Tra);

-- Combine the list of all staff who have made transactions and the list of all staff who have not made any transactions (with duplicates)
SELECT 
    Sta.EmployeeID, 
    Sta.FirstName, 
    Sta.LastName
FROM
    Staff AS Sta
WHERE
    Sta.EmployeeID IN (
        SELECT DISTINCT Tra.StaffID 
        FROM Transactions AS Tra
    ) 
UNION ALL 
SELECT 
    Sta.EmployeeID, 
    Sta.FirstName, 
    Sta.LastName
FROM
    Staff AS Sta
WHERE
    Sta.EmployeeID NOT IN (
        SELECT DISTINCT Tra.StaffID 
        FROM Transactions AS Tra
    );

-- group_concat usage 
SELECT 
    GROUP_CONCAT(Sta.EmployeeID ORDER BY Sta.EmployeeID ASC SEPARATOR ', ') AS EmployeeIds
FROM
    Staff AS Sta;
