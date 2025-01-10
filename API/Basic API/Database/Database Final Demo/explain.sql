USE final_library_db;

SELECT 
    b01f01 AS 'Book ID(Primary Key)',
    b01f02 AS 'Book Title',
    b01f03 AS 'Author Name',
    b01f04 AS 'Published Year',
    b01f05 AS 'Category'
FROM
    final_library_db.ymb01;
    
SELECT 
    m01f01 AS 'Member ID (Primary Key)',    -- Member ID (Primary Key)
    m01f02 AS 'Member Name',                 -- Member Name
    m01f03 AS 'Email Address (Unique)',      -- Email Address (Unique)
    m01f04 AS 'Membership Start Date'        -- Membership Start Date (only date)
FROM 
    final_library_db.ymm01;

SELECT 
    h01f01 AS 'Book History ID (Primary Key)', -- Book History ID (Primary Key)
    h01f02 AS 'Book ID (Foreign Key)',          -- Book ID (Foreign Key)
    h01f03 AS 'Member ID (Foreign Key)',        -- Member ID (Foreign Key)
    h01f04 AS 'Issue Date',                     -- Issue Date
    h01f05 AS 'Return Date'                     -- Return Date
FROM 
    final_library_db.ymh01;  
    
SELECT 
    *
FROM
    active_book_rentals;

SELECT 
    *
FROM
    books_by_category;

-- I have index on  book table column name b01f03(author name)

-- Dense Index
CREATE INDEX index_b01f03 ON ymb01(b01f03); 

-- Simulating Sparse Index
-- CREATE INDEX index_h01f05 ON ymh01 (h01f05) WHERE h01f05 IS NULL;

ALTER TABLE ymb01
DROP index index_b01f03;

EXPLAIN SELECT 
    *
FROM
    ymb01
WHERE
    b01f03 LIKE 'Harp%';
    
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
    h01f05 IS NULL;
    
SELECT 
    ymb01.b01f05 AS 'Category',               -- Category
    COUNT(ymb01.b01f01) AS 'Total Books'     -- Total number of books in each category
FROM 
    ymb01
GROUP BY 
    ymb01.b01f05                              -- Group by Category
HAVING 
    MAX(ymb01.b01f04) > 2015;                -- Only include categories with books published after 2015
    
SELECT 
    ymb01.b01f05 AS 'Category',
    COUNT(ymb01.b01f01) AS 'Total Books'
FROM 
    ymb01
WHERE 
    ymb01.b01f04 > 2015                -- Filter rows before grouping
GROUP BY 
    ymb01.b01f05;


    

    


