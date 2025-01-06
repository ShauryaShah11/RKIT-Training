-- Specify the database to use
USE practiceDB;

-- Insert a single value into the Users table
INSERT INTO Users (FirstName, LastName, Email)
VALUES ('John', 'Doe', 'john.doe@example.com');

-- Insert multiple values into the Users table
INSERT INTO Users (FirstName, LastName, Email)
VALUES 
    ('Alice', 'Smith', 'alice.smith@example.com'),
    ('Bob', 'Johnson', 'bob.johnson@example.com'),
    ('Charlie', 'Brown', 'charlie.brown@example.com');

-- Insert a single value into the Orders table
INSERT INTO Orders (OrderDate, Amount, UserId)
VALUES ('2024-12-25', 150.00, 1);

-- Insert multiple values into the Orders table
INSERT INTO Orders (OrderDate, Amount, UserId)
VALUES 
    ('2024-12-20', 200.00, 2),
    ('2024-12-21', 300.50, 3),
    ('2024-12-22', 120.75, 1);
