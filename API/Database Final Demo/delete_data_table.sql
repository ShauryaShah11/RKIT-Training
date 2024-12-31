-- Specify database to use
USE final_library_db;

-- Delete the book with ID 3 from the Books table
DELETE FROM ymb01 
WHERE
    ymb01f01 = 3; -- Book ID = 3

-- Delete the member with ID 3 from the Members table
DELETE FROM ymm01 
WHERE
    ymm01f01 = 3; -- Member ID = 3

-- Delete the book history with ID 3 from the Book History table
DELETE FROM ymh01 
WHERE
    ymh01f01 = 3; -- Book History ID = 3
