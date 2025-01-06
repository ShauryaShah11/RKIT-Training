-- Specify the database to use
USE practiceDB;

-- Step 1: Create an index on the Email column of the Staff table to speed up searches
CREATE INDEX idx_email
ON Staff (Email);

-- index on multiple columns
-- CREATE INDEX idx_name
-- ON Staff (FirstName, LastName);

-- Step 2: Query that benefits from the index
SELECT 
    *
FROM
    Staff
WHERE
    Email = 'david.clark@example.com';
    
-- drop index

ALTER TABLE Staff
DROP INDEX idx_email;
