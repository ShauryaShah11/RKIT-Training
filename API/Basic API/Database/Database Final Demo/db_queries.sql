-- Specify database to use
USE final_library_db;

-- Select books where the books are not returned yet and display book title and member name
EXPLAIN SELECT 
    ymh01.h01f01 AS 'Book History ID',  -- Book History ID
    ymb01.b01f02 AS 'Book Title',       -- Book Title
    ymm01.m01f02 AS 'Member Name',      -- Member Name
    ymh01.h01f04 AS 'Issue Date',       -- Issue Date
    ymh01.h01f05 AS 'Return Date'       -- Return Date
FROM 
    ymh01
JOIN 
    ymb01 ON ymh01.h01f02 = ymb01.b01f01  -- Join with Books table to get book title
JOIN 
    ymm01 ON ymh01.h01f03 = ymm01.m01f01  -- Join with Members table to get member name
WHERE 
    h01f05 IS NULL;  -- Books that have not been returned yet
    

    
-- Select books in the 'Science Fiction' category
EXPLAIN SELECT 
    ymb01.b01f01 AS 'Book ID',            -- Book ID
    ymb01.b01f02 AS 'Book Title',         -- Book Title
    ymb01.b01f03 AS 'Author Name',        -- Author Name
    ymb01.b01f04 AS 'Published Year',     -- Published Year
    ymb01.b01f05 AS 'Category'            -- Category
FROM 
    ymb01
WHERE 
    ymb01.b01f05 = 'Science Fiction'     -- Condition for Science Fiction books

UNION  -- Combine this with the next query

-- Select books published in the year 2020
SELECT 
    ymb01.b01f01 AS 'Book ID',            -- Book ID
    ymb01.b01f02 AS 'Book Title',         -- Book Title
    ymb01.b01f03 AS 'Author Name',        -- Author Name
    ymb01.b01f04 AS 'Published Year',     -- Published Year
    ymb01.b01f05 AS 'Category'            -- Category
FROM 
    ymb01
WHERE 
    ymb01.b01f04 = 2020                   -- Condition for books published in 2020

UNION  -- Combine this with the next query

-- Select books that have been borrowed (i.e., Issue Date is not NULL)
SELECT 
    ymb01.b01f01 AS 'Book ID',            -- Book ID
    ymb01.b01f02 AS 'Book Title',         -- Book Title
    ymb01.b01f03 AS 'Author Name',        -- Author Name
    ymb01.b01f04 AS 'Published Year',     -- Published Year
    ymb01.b01f05 AS 'Category'            -- Category
FROM 
    ymb01
JOIN 
    ymh01 ON ymb01.b01f01 = ymh01.h01f02  -- Join with Book History table
WHERE 
    ymh01.h01f04 IS NOT NULL;                -- Condition for borrowed books (Issue Date is not NULL)
    
-- Data sorting demo

-- Sort books by title in ascending order (A-Z)
SELECT 
    ymb01.b01f01 AS 'Book ID',
    ymb01.b01f02 AS 'Book Title',
    ymb01.b01f03 AS 'Author',
    ymb01.b01f04 AS 'Published Year',
    ymb01.b01f05 AS 'Category'
FROM 
    ymb01
ORDER BY 
    ymb01.b01f02 ASC;  -- Sorts by Book Title in ascending order

-- Sort books by published year in descending order (latest first)
SELECT 
    ymb01.b01f01 AS 'Book ID',
    ymb01.b01f02 AS 'Book Title',
    ymb01.b01f03 AS 'Author',
    ymb01.b01f04 AS 'Published Year',
    ymb01.b01f05 AS 'Category'
FROM 
    ymb01
ORDER BY 
    ymb01.b01f04 DESC;  -- Sorts by Published Year in descending order

-- Aggregate Functions Demo

-- Count the total number of books in each category
SELECT 
    ymb01.b01f05 AS 'Category',           -- Category
    COUNT(ymb01.b01f01) AS 'Total Books'  -- Count of books in each category
FROM 
    ymb01
GROUP BY 
    ymb01.b01f05;                         -- Group by category

-- Find the average published year of books in the library
SELECT 
    AVG(ymb01.b01f04) AS 'Average Published Year'  -- Average of Published Year
FROM 
    ymb01;

-- Find the maximum and minimum published year of books in the library
SELECT 
    MAX(ymb01.b01f04) AS 'Most Recent Published Year',  -- Maximum Published Year
    MIN(ymb01.b01f04) AS 'Oldest Published Year'         -- Minimum Published Year
FROM 
    ymb01;

-- Subquery Demo

-- Find books borrowed by the member with ID = 1
EXPLAIN SELECT 
    ymb01.b01f02 AS 'Book Title',         -- Book Title
    ymb01.b01f03 AS 'Author Name'         -- Author Name
FROM 
    ymb01
WHERE 
    ymb01.b01f01 IN (                     -- Subquery to find Book IDs borrowed by member ID = 1
        SELECT ymh01.h01f02                 -- Book ID from the Book History table
        FROM ymh01
        WHERE ymh01.h01f03 = 1              -- Member ID = 1
        AND ymh01.h01f05 IS NULL           -- Books that have not been returned yet
    );

-- LIMIT Demo

-- Select the first 5 books based on their published year (most recent first)
SELECT 
    ymb01.b01f02 AS 'Book Title',         -- Book Title
    ymb01.b01f04 AS 'Published Year'      -- Published Year
FROM 
    ymb01
ORDER BY 
    ymb01.b01f04 DESC                      -- Order books by published year in descending order
LIMIT 5;                                    -- Limit result to first 5 books