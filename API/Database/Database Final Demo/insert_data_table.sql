-- Specify database to use
USE final_library_db;

-- Start the transaction
START TRANSACTION;

-- Insert data into the 'ymb01' table (Books)
INSERT INTO ymb01 (ymb01f02, ymb01f03, ymb01f04, ymb01f05)
VALUES
('To Kill a Mockingbird', 'Harper Lee', 1960, 'Fiction'),
('1984', 'George Orwell', 1949, 'Dystopian'),
('The Great Gatsby', 'F. Scott Fitzgerald', 1925, 'Fiction'),
('Neuromancer', 'William Gibson', 1984, 'Science Fiction'),
('Dune', 'Frank Herbert', 1965, 'Science Fiction'),
('The Invisible Man', 'H.G. Wells', 1897, 'Science Fiction'),
('Educated', 'Tara Westover', 2018, 'Memoir'),
('Sapiens', 'Yuval Noah Harari', 2011, 'Non-Fiction');

-- Insert data into the 'ymm01' table (Members)
INSERT INTO ymm01 (ymm01f02, ymm01f03, ymm01f04)
VALUES
('Alice Johnson', 'alice.johnson@example.com', '2023-01-01'),
('Bob Smith', 'bob.smith@example.com', '2023-02-01'),
('Charlie Brown', 'charlie.brown@example.com', '2023-03-01'),
('Daisy Lee', 'daisy.lee@example.com', '2023-04-01'),
('Evan White', 'evan.white@example.com', '2023-05-01');

-- Insert data into the 'ymh01' table (Book History)
-- (Book History ID, Book ID, Member ID, Issue Date, Return Date)
INSERT INTO ymh01 (ymh01f02, ymh01f03, ymh01f04, ymh01f05)
VALUES
(1, 1, '2023-11-01', NULL),        -- Alice Johnson borrowed 'To Kill a Mockingbird' and hasn't returned it yet
(2, 2, '2023-12-01', '2023-12-15'), -- Bob Smith borrowed '1984' and returned it
(3, 3, '2023-10-01', '2023-11-01'), -- Charlie Brown borrowed 'The Great Gatsby' and returned it
(4, 4, '2023-09-01', '2023-09-15'), -- Daisy Lee borrowed 'Neuromancer' and returned it
(5, 5, '2023-12-01', NULL);         -- Evan White borrowed 'Dune' and hasn't returned it 

rollback;

-- Commit the transaction to save all changes
COMMIT;
