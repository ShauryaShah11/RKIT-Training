-- Specify database to use
USE final_library_db;

-- Update the Book Title and Author Name for the book with ID 1
UPDATE ymb01 
SET 
    ymb01f02 = 'Titanic',
    ymb01f03 = 'F. Scott Fitzgerald'
WHERE
    ymb01f01 = 1; -- Book ID = 1

-- Update the Email Address and Membership Start Date for the member with ID 1
UPDATE ymm01 
SET 
    ymm01f03 = 'john.doe@example.com',
    ymm01f04 = '2023-06-15'
WHERE
    ymm01f01 = 1;  -- Member ID = 1

-- Update the Return Date for the Book History with Book History ID 1
UPDATE ymh01 
SET 
    ymh01f05 = '2024-12-31'
WHERE
    ymh01f01 = 1;            -- Book History ID = 1


    

