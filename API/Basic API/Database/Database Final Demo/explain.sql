USE final_library_db;

-- Fetching book details from the ymb01 (Books) table
SELECT 
    ymb.b01f01 AS 'Book ID (Primary Key)',     -- Unique identifier for each book
    ymb.b01f02 AS 'Book Title',               -- Title of the book
    ymb.b01f03 AS 'Author Name',              -- Author's name
    ymb.b01f04 AS 'Published Year',           -- Year the book was published
    ymb.b01f05 AS 'Category'                  -- Category of the book
FROM
    final_library_db.ymb01 ymb;

-- Fetching member details from the ymm01 (Members) table
SELECT 
    ymm.m01f01 AS 'Member ID (Primary Key)',     -- Unique identifier for each member
    ymm.m01f02 AS 'Member Name',                -- Name of the member
    ymm.m01f03 AS 'Email Address (Unique)',     -- Member's unique email address
    ymm.m01f04 AS 'Membership Start Date'       -- Membership start date (only date)
FROM 
    final_library_db.ymm01 ymm;

-- Fetching book rental history details from the ymh01 (History) table
SELECT 
    ymh.h01f01 AS 'Book History ID (Primary Key)', -- Unique identifier for each rental record
    ymh.h01f02 AS 'Book ID (Foreign Key)',         -- Reference to the Book ID in ymb01
    ymh.h01f03 AS 'Member ID (Foreign Key)',       -- Reference to the Member ID in ymm01
    ymh.h01f04 AS 'Issue Date',                    -- Date the book was issued
    ymh.h01f05 AS 'Return Date'                    -- Date the book was returned
FROM 
    final_library_db.ymh01 ymh;

-- Fetching all rows from active_book_rentals view
SELECT 
    *
FROM
    active_book_rentals;

-- Fetching all rows from books_by_category view
SELECT 
    *
FROM
    books_by_category;

-- Dense Index: Creating an index on the author name column in the ymb01 table
CREATE INDEX idx_b01f03 ON ymb01(b01f03);

-- Sparse Index: Creating a partial index on the Return Date column for NULL values
-- Uncomment the below query to simulate sparse index
-- CREATE INDEX idx_h01f05 ON ymh01(h01f05) WHERE h01f05 IS NULL;

-- Dropping the previously created index on the author name column
ALTER TABLE ymb01
DROP INDEX idx_b01f03;

-- Explain query to analyze the execution plan for searching books by author name
EXPLAIN SELECT 
    *
FROM
    ymb01 ymb
WHERE
    ymb.b01f03 LIKE 'Harp%'; -- Fetch books where the author's name starts with 'Harp'

-- Explain query to analyze the execution plan for fetching book rental details
EXPLAIN SELECT 
    ymh.h01f01 AS 'Book History ID',  -- Rental record ID
    ymb.b01f02 AS 'Book Title',       -- Book title
    ymm.m01f02 AS 'Member Name',      -- Member name
    ymh.h01f04 AS 'Issue Date',       -- Date the book was issued
    ymh.h01f05 AS 'Return Date'       -- Date the book was returned
FROM 
    ymh01 ymh
JOIN 
    ymb01 ymb ON ymh.h01f02 = ymb.b01f01  -- Join history with books table
JOIN 
    ymm01 ymm ON ymh.h01f03 = ymm.m01f01  -- Join history with members table
WHERE 
    ymh.h01f05 IS NULL;                   -- Fetch only books that haven't been returned

-- Grouping books by category and filtering categories with books published after 2015
SELECT 
    ymb.b01f05 AS 'Category',               -- Book category
    COUNT(ymb.b01f01) AS 'Total Books'     -- Total number of books in the category
FROM 
    ymb01 ymb
GROUP BY 
    ymb.b01f05                              -- Grouping by book category
HAVING 
    MAX(ymb.b01f04) > 2015;                -- Include categories where the latest book was published after 2015

-- Optimized version: Applying filter before grouping
SELECT 
    ymb.b01f05 AS 'Category',               -- Book category
    COUNT(ymb.b01f01) AS 'Total Books'     -- Total number of books in the category
FROM 
    ymb01 ymb
WHERE 
    ymb.b01f04 > 2015                       -- Filter books published after 2015 before grouping
GROUP BY 
    ymb.b01f05;                             -- Grouping by book category
