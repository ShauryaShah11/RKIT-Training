-- Specify database to use
USE final_library_db;

-- Start the transaction
START TRANSACTION;

-- Create Books table
CREATE TABLE ymb01 (
    b01f01 INT AUTO_INCREMENT PRIMARY KEY, -- Book ID (Primary Key)
    b01f02 VARCHAR(255) NOT NULL UNIQUE,         -- Book Title
    b01f03 VARCHAR(255),                  -- Author Name
    b01f04 INT, 							-- Published Year 
    b01f05 VARCHAR(255)                  -- Category
);

-- Create Members table
CREATE TABLE ymm01 (
    m01f01 INT AUTO_INCREMENT PRIMARY KEY,  -- Member ID (Primary Key)
    m01f02 VARCHAR(255) NOT NULL,            -- Member Name
    m01f03 VARCHAR(255) UNIQUE,              -- Email Address (Unique)
    m01f04 DATE NOT NULL  -- Membership Start Date (only date)
);

-- Create Book History table
CREATE TABLE ymh01 (
    h01f01 INT AUTO_INCREMENT PRIMARY KEY, -- Book History ID (Primary Key)
    h01f02 INT NOT NULL,                   -- Book ID (Foreign Key)
    h01f03 INT NOT NULL,                   -- Member ID (Foreign Key)
    h01f04 DATE NOT NULL,                  -- Issue Date
    h01f05 DATE,                           -- Return Date
    CONSTRAINT fk_book FOREIGN KEY (h01f02) REFERENCES ymb01(b01f01) ON DELETE CASCADE, -- FK to Books
    CONSTRAINT fk_member FOREIGN KEY (h01f03) REFERENCES ymm01(m01f01) ON DELETE CASCADE, -- FK to Members
    CHECK (h01f05 IS NULL OR h01f05 > h01f04) -- Return Date must be later than Issue Date
);

-- Commit the transaction to save all changes
COMMIT;
