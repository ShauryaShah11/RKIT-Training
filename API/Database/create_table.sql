-- Specify the database to use
USE practiceDB;

-- Create Users table
CREATE TABLE Users (
    UserID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50),
    Email VARCHAR(100) UNIQUE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Orders table
-- CREATE TABLE Orders(
-- 	OrderId INT PRIMARY KEY AUTO_INCREMENT,
--     OrderDate DATE,
--     Amount DECIMAL,
--     UserId INT    
-- );

-- added foreign key in orders table 
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY AUTO_INCREMENT,
    OrderDate DATE,
    Amount DECIMAL(10 , 2 ) NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId)
        REFERENCES Users (UserID)
        ON DELETE CASCADE ON UPDATE CASCADE
);

