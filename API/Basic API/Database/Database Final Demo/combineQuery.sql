-- The following SQL query is designed to retrieve book borrowing information along with 
-- the total number of books borrowed by each member, as well as the latest and oldest book borrowed.
-- The query is divided into two parts:
-- 1. The first part lists individual books borrowed by each member with their return status.
-- 2. The second part aggregates the total number of books borrowed by each member, 
--    along with the latest and oldest book borrowed. 

-- First part of the query:
SELECT 
    ymb.b01f02 AS BookTitle,   -- Book title from ymb01 table
    ymm.m01f02 AS MemberName,  -- Member name from ymm01 table
    IFNULL(CASE 
                WHEN ymh.h01f05 IS NULL THEN 'Not Returned'   -- If the return date is NULL, it's 'Not Returned'
                ELSE 'Returned'   -- Otherwise, it's 'Returned'
            END, 'Not Returned') AS BookReturnStatus,  -- If no return status, default to 'Not Returned'
    0 AS TotalBooksBorrowed,  -- This field is a placeholder since we're not summarizing total books here
    '----' AS LatestBookBorrowed,  -- Placeholder for the latest book borrowed
    '----' AS OldestBookBorrowed  -- Placeholder for the oldest book borrowed
FROM 
    ymb01 ymb  -- Table containing book information
JOIN 
    ymh01 ymh ON ymb.b01f01 = ymh.h01f02  -- Join on borrowing record (h01f02 = b01f01)
JOIN 
    ymm01 ymm ON ymh.h01f03 = ymm.m01f01  -- Join on member information (h01f03 = m01f01)
GROUP BY 
    ymm.m01f02, ymb.b01f02  -- Grouping by member and book title

-- Union to aggregate data:
UNION ALL

-- Second part of the query:
SELECT 
    '----' AS BookTitle,   -- Placeholder for the book title in the summary row
    ymm.m01f02 AS MemberName,  -- Member name from ymm01 table
    'Total Books Borrowed' AS BookReturnStatus,  -- Label for the summary row indicating total books borrowed
    COUNT(ymh.h01f01) AS TotalBooksBorrowed,  -- Aggregate function to count the total number of books borrowed by each member
    MAX(ymb.b01f02) AS LatestBookBorrowed,  -- Aggregate function to get the most recent book borrowed
    MIN(ymb.b01f02) AS OldestBookBorrowed  -- Aggregate function to get the oldest book borrowed
FROM 
    ymh01 ymh  -- Borrowing records table
JOIN 
    ymm01 ymm ON ymh.h01f03 = ymm.m01f01  -- Join with the member table
JOIN
    ymb01 ymb ON ymh.h01f02 = ymb.b01f01  -- Join with the book table to retrieve book details
GROUP BY 
    ymm.m01f02  -- Grouping by member to get summary for each member

-- The results are sorted by member name:
ORDER BY 
    MemberName;