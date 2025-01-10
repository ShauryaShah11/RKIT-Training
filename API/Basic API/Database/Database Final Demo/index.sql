-- Specify database to use
USE final_library_db;

CREATE INDEX index_b01f03 ON ymb01(b01f03);

-- Example query: Search for books with titles containing the word 'The'
SELECT 
    *
FROM
    ymb01
WHERE
    b01f03 LIKE '%The%';