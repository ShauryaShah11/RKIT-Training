-- Specify database to use
USE final_library_db;

CREATE INDEX index_ymb01f03 ON ymb01(ymb01f03);

-- Example query: Search for books with titles containing the word 'The'
SELECT 
    *
FROM
    ymb01
WHERE
    ymb01f02 LIKE '%The%';