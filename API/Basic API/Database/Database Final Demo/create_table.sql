-- Specify database to use
USE final_library_db;

-- Start the transaction
START TRANSACTION;

-- Create Books table
CREATE TABLE ymb01 (
    ymb01f01 INT AUTO_INCREMENT PRIMARY KEY, -- Book ID (Primary Key)
    ymb01f02 VARCHAR(255) NOT NULL UNIQUE,         -- Book Title
    ymb01f03 VARCHAR(255),                  -- Author Name
    ymb01f04 INT, 							-- Published Year 
    ymb01f05 VARCHAR(255)                  -- Category
);

-- Create Members table
CREATE TABLE ymm01 (
    ymm01f01 INT AUTO_INCREMENT PRIMARY KEY,  -- Member ID (Primary Key)
    ymm01f02 VARCHAR(255) NOT NULL,            -- Member Name
    ymm01f03 VARCHAR(255) UNIQUE,              -- Email Address (Unique)
    ymm01f04 DATE NOT NULL  -- Membership Start Date (only date)
);

-- Create Book History table
CREATE TABLE ymh01 (
    ymh01f01 INT AUTO_INCREMENT PRIMARY KEY, -- Book History ID (Primary Key)
    ymh01f02 INT NOT NULL,                   -- Book ID (Foreign Key)
    ymh01f03 INT NOT NULL,                   -- Member ID (Foreign Key)
    ymh01f04 DATE NOT NULL,                  -- Issue Date
    ymh01f05 DATE,                           -- Return Date
    CONSTRAINT fk_book FOREIGN KEY (ymh01f02) REFERENCES ymb01(ymb01f01) ON DELETE CASCADE, -- FK to Books
    CONSTRAINT fk_member FOREIGN KEY (ymh01f03) REFERENCES ymm01(ymm01f01) ON DELETE CASCADE, -- FK to Members
    CHECK (ymh01f05 IS NULL OR ymh01f05 > ymh01f04) -- Return Date must be later than Issue Date
);

-- Commit the transaction to save all changes
COMMIT;
