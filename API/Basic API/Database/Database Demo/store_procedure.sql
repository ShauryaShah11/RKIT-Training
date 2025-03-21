CREATE DATABASE EmployeeTestDB;

USE EmployeeTestDB;

CREATE TABLE employees (
    emp_id INT AUTO_INCREMENT PRIMARY KEY,
    emp_name VARCHAR(100),
    department_id INT,
    salary DECIMAL(10,2)
);

INSERT INTO employees (emp_name, department_id, salary) VALUES 
('Alice Johnson', 1, 50000.00),
('Bob Smith', 2, 60000.00),
('Charlie Brown', 1, 55000.00),
('David Williams', 3, 70000.00),
('Emily Davis', 2, 62000.00);

SELECT 
    *
FROM
    employees;
    
DELIMITER $$
CREATE PROCEDURE get_employees()
BEGIN
	SELECT 
		*
	FROM
		employees;
END $$
DELIMITER ;

CALL get_employees();

DELIMITER $$
CREATE PROCEDURE get_employee_by_department(IN dept_id INT)
BEGIN
	SELECT
		*
	FROM
		employees
	WHERE department_id = dept_id;
END $$
DELIMITER ;
    
CALL get_employee_by_department(1);

DELIMITER $$
CREATE PROCEDURE get_salary_by_id(IN emp_id_param INT, OUT emp_salary DECIMAL(10,2))
BEGIN
    SELECT salary INTO emp_salary 
    FROM employees 
    WHERE emp_id = emp_id_param;
END $$
DELIMITER ;

CALL get_salary_by_id(1, @salary);

SELECT @salary;

DELIMITER //
CREATE PROCEDURE CalculateBonus(IN emp_id_params INT)
BEGIN
    DECLARE emp_salary DECIMAL(10,2);
    DECLARE bonus DECIMAL(10,2);
    
    SELECT salary INTO emp_salary FROM employees WHERE emp_id = emp_id_params;
    
    -- Calculate bonus (10% of salary)
    SET bonus = emp_salary * 0.10;
    
    SELECT emp_salary, bonus;
END //
DELIMITER ;

CALL CalculateBonus(1);

DELIMITER //
CREATE PROCEDURE CheckSalary(IN emp_id_params INT, OUT result VARCHAR(50))
BEGIN
    DECLARE emp_salary DECIMAL(10,2);
    
    SELECT salary INTO emp_salary FROM employees WHERE emp_id = emp_id_params;

    IF emp_salary > 50000 THEN
        SET result = 'High Salary';
    ELSEIF emp_salary BETWEEN 30000 AND 50000 THEN
        SET result = 'Medium Salary';
    ELSE
        SET result = 'Low Salary';
    END IF;
END //
DELIMITER ;

CALL CheckSalary(1, @status);
SELECT @status;

DELIMITER //
CREATE PROCEDURE GetDepartmentName(IN dept_id INT, OUT dept_name VARCHAR(50))
BEGIN
	CASE dept_id
        WHEN 1 THEN SET dept_name = 'HR';
        WHEN 2 THEN SET dept_name = 'IT';
        WHEN 3 THEN SET dept_name = 'Finance';
        ELSE SET dept_name = 'Unknown';
	END CASE;
END //
DELIMITER ;

CALL GetDepartmentName(1, @dept_name);
SELECT @dept_name;
		



