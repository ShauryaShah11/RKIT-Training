use final_library_db;

WITH AveragePublishedYear AS (
    -- CTE to calculate the average published year
    SELECT 
        AVG(ymb.b01f04) AS 'Average Published Year'  -- Average of Published Year
    FROM 
        ymb01 as ymb
)
-- Main query to select the result from the CTE
SELECT * 
FROM AveragePublishedYear;

WITH RECURSIVE FactorialCTE (n, factorial) AS (
    -- Base case: n = 1
    SELECT 1, 1
    UNION ALL
    -- Recursive case: factorial of n is n * factorial of (n-1)
    SELECT n+1, factorial * (n+1)
    FROM FactorialCTE
    WHERE n < 5  -- Define stopping condition
)
SELECT * FROM FactorialCTE;

