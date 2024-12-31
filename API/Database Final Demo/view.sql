-- Specify database to use
USE final_library_db;

-- Create a view to show books by category
CREATE VIEW books_by_category AS
SELECT 
    ymb01.ymb01f01 AS 'Book ID',            -- Book ID
    ymb01.ymb01f02 AS 'Book Title',         -- Book Title
    ymb01.ymb01f03 AS 'Author Name',        -- Author Name
    ymb01.ymb01f04 AS 'Published Year',     -- Published Year
    ymb01.ymb01f05 AS 'Category'            -- Category
FROM 
    ymb01
ORDER BY 
    ymb01.ymb01f05;  -- Sort by Category


SELECT 
    *
FROM
    books_by_category;
    
-- Create a view that shows active book rentals (books that haven't been returned yet)
CREATE VIEW active_book_rentals AS
SELECT 
    ymh01.ymh01f01 AS 'Book History ID',  -- Book History ID
    ymb01.ymb01f02 AS 'Book Title',       -- Book Title
    ymm01.ymm01f02 AS 'Member Name',      -- Member Name
    ymh01.ymh01f04 AS 'Issue Date',       -- Issue Date
    ymh01.ymh01f05 AS 'Return Date'       -- Return Date (NULL means not returned yet)
FROM 
    ymh01
JOIN 
    ymb01 ON ymh01.ymh01f02 = ymb01.ymb01f01  -- Join with Books table to get book title
JOIN 
    ymm01 ON ymh01.ymh01f03 = ymm01.ymm01f01  -- Join with Members table to get member name
WHERE 
    ymh01.ymh01f05 IS NULL;  -- Only books that have not been returned
    
SELECT 
    *
FROM
    active_book_rentals

