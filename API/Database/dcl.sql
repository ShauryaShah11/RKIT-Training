-- DCL commands are used to manage permissions and access controls in a database. These commands ensure database security by allowing or restricting user access to database objects.

-- Specify the database to use
USE practiceDB;

-- Step 1: Create a new user
CREATE USER 'intern_user'@'localhost' IDENTIFIED BY 'password123';

-- Step 2: Grant SELECT and INSERT privileges on the Staff table
GRANT SELECT, INSERT ON Staff TO 'intern_user'@'localhost';

-- Step 3: Revoke the INSERT privilege
REVOKE INSERT ON Staff FROM 'intern_user'@'localhost';

-- verify privilege of user
SHOW GRANTS FOR 'intern_user'@'localhost';
