-- Step 1: Create and use the database
CREATE DATABASE test;
USE test;

-- Step 2: Create the departments table
CREATE TABLE departments (
    department_id INT PRIMARY KEY, -- Unique identifier for each department
    department_name VARCHAR(100) NOT NULL -- Name of the department
);

-- Step 3: Create the employees table
CREATE TABLE employees (
    employee_id INT PRIMARY KEY, -- Unique identifier for each employee
    employee_name VARCHAR(100) NOT NULL, -- Name of the employee
    salary DECIMAL(10, 2) NOT NULL, -- Salary of the employee
    department_id INT, -- Foreign key linking to the departments table
    FOREIGN KEY (department_id) REFERENCES departments(department_id) -- Establishes relationship with departments
);

-- Step 4: Insert data into the departments table
INSERT INTO departments (department_id, department_name) VALUES
(1, 'Human Resources'),
(2, 'Engineering'),
(3, 'Marketing'),
(4, 'Sales');

-- Step 5: Insert data into the employees table
INSERT INTO employees (employee_id, employee_name, salary, department_id) VALUES
(1, 'Alice', 55000, 1),
(2, 'Bob', 65000, 2),
(3, 'Charlie', 45000, 2),
(4, 'David', 70000, 3),
(5, 'Eve', 60000, 3),
(6, 'Frank', 80000, 2),
(7, 'Grace', 50000, 4),
(8, 'Hannah', 75000, 1),
(9, 'Ivan', 90000, 2),
(10, 'Jack', 95000, 4);

-- Step 6: Complex query with JOINs, sub-queries, UNION, and LIMIT

-- Select employees with a salary above the department average
SELECT 
    e.employee_id, 
    e.employee_name, 
    e.salary, 
    d.department_name
FROM 
    employees e
JOIN 
    departments d ON e.department_id = d.department_id -- Join employees with their departments
WHERE 
    e.salary > (
        SELECT 
            AVG(salary)
        FROM 
            employees
        WHERE 
            department_id = e.department_id -- Average salary of the same department
    )

-- Combine with employees from the department with the highest average salary
UNION
SELECT 
    e.employee_id, 
    e.employee_name, 
    e.salary, 
    d.department_name
FROM 
    employees e
JOIN 
    departments d ON e.department_id = d.department_id
WHERE 
    d.department_name = (
        SELECT 
            department_name
        FROM 
            departments
        WHERE 
            department_id = (
                SELECT 
                    department_id
                FROM 
                    employees
                GROUP BY 
                    department_id -- Group by department to calculate averages
                ORDER BY 
                    AVG(salary) DESC -- Sort by highest average salary
                LIMIT 1 -- Get the department with the highest average
            )
    )
LIMIT 10; -- Limit the final output to 5 rows
