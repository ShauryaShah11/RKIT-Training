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
