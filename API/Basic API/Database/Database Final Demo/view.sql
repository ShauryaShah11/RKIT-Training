-- Specify database to use
USE final_library_db;

-- Create a view to show books by category
CREATE VIEW vwy_ymb01 AS
SELECT 
    ymb.b01f01 AS 'Book ID',            -- Book ID
    ymb.b01f02 AS 'Book Title',         -- Book Title
    ymb.b01f03 AS 'Author Name',        -- Author Name
    ymb.b01f04 AS 'Published Year',     -- Published Year
    ymb.b01f05 AS 'Category'            -- Category
FROM 
    ymb01 as ymb
ORDER BY 
    ymb.b01f05;  -- Sort by Category


SELECT 
    *
FROM
    vwy_ymb01;
    
-- Create a view that shows active book rentals (books that haven't been returned yet)
-- vwy_view_name -> vw => view y => year or system
CREATE VIEW vwy_ymh01 AS
SELECT 
    ymh.h01f01 AS 'Book History ID',  -- Book History ID
    ymb.b01f02 AS 'Book Title',       -- Book Title
    ymm.m01f02 AS 'Member Name',      -- Member Name
    ymh.h01f04 AS 'Issue Date',       -- Issue Date
    ymh.h01f05 AS 'Return Date'       -- Return Date (NULL means not returned yet)
FROM 
    ymh01 as ymh
JOIN 
    ymb01 as ymb ON ymh.h01f02 = ymb.b01f01  -- Join with Books table to get book title
JOIN 
    ymm01 ON ymh.h01f03 = ymm.m01f01  -- Join with Members table to get member name
WHERE 
    ymh.h01f05 IS NULL;  -- Only books that have not been returned

SELECT 
    *
FROM
    vwy_ymh01;

