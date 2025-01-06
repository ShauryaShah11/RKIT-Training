# Debugging In Visual Studio

Debugging is an essential part of the development process. This guide uses the `UserController` from the provided code to illustrate debugging techniques in Visual Studio.

## Code Overview

The `UserController` is a Web API controller with methods to perform CRUD operations on user data. It uses a `UserRepository` for database interactions. The methods include:

1. **GetAllUser**: Retrieves all users.
2. **GetUser**: Retrieves a specific user by ID.
3. **AddUser**: Adds a new user to the database.

### Debugging Techniques Used in `UserController`

### 1. Setting Breakpoints

Breakpoints are markers to pause the code execution at a specific line, allowing you to inspect variables and program flow.

- **Example**: In the `GetAllUser` method:

Set a breakpoint here to inspect the `users` list after it is populated.

![image.png](https://i.postimg.cc/nLMPxbrV/image.png)

**Steps to Set a Breakpoint:**

1. Click the left margin next to the line number or press `F9`.
2. Run the application in Debug mode (`F5`).
3. Execution will pause when this line is reached.

Before Breakpoint Output:

![image.png](https://i.postimg.cc/3x0GQt75/image-1.png)

After Breakpoint Output:

![image.png](https://i.postimg.cc/cL1sQH9D/image-2.png)

### 2. Using Conditional Breakpoints

Conditional breakpoints allow you to pause execution only when a specified condition is met.

- **Example**: In the `GetAllUser` method, set a conditional breakpoint to pause only if the list contains users:
    
    ```
    users.Count > 0
    ```
    

**Steps to Set a Conditional Breakpoint:**

1. Right-click the breakpoint and select **Conditions**.
2. Enter the condition (e.g., `users.Count > 0`).
3. Run the application. The breakpoint will trigger only when the condition is true.

![image.png](https://i.postimg.cc/J0gbjvmx/image-3.png)

### 3. Inspecting Values with the Watch Window

The Watch window allows you to monitor variable values during debugging.

- **Example**: Monitor the `users` variable in the `GetAllUser` method.
**Steps:**
    1. Pause execution at a breakpoint.
    2. Open the Watch window (`Debug > Windows > Watch > Watch 1`).
    3. Add `users` to the Watch window to see its value.

### 4. Using Debug.WriteLine

`Debug.WriteLine` is a lightweight debugging tool to log information to the Output window.

- **Example**: In the `GetUser` method, log a message when a user is not found:
    
    ```
    #if DEBUG
        Debug.WriteLine($"User with id {id} not found");
    #endif
    ```
    

**Steps:**

1. Add the `Debug.WriteLine` code in a debug-only block using `#if DEBUG`.
2. Run the application in Debug mode.
3. View the message in the Output window (`Ctrl + Alt + O`).

### 5. Exception Handling and Debugging

Catch exceptions and inspect them during debugging to identify issues.

- **Example**: In the `GetUser` method, handle scenarios where a user is not found:
    
    ```
    if (user == null)
    {
        throw new UnauthorizedAccessException("Invalid or expired token");
    }
    ```
    
    Use a breakpoint to pause execution and inspect the exception object.
    

### 6. Debugging HTTP Requests

Use tools like Postman or your browser to send HTTP requests to test the API endpoints.

- **Example Request to Test `GetUser` Method**:
    
    ```
    GET /api/user/1
    ```
    
    - Validate the API response in Postman.
    - Debug the method in Visual Studio when the breakpoint is hit.

### Summary

Debugging in Visual Studio involves:

- **Breakpoints**: Pause execution and inspect variables.
- **Conditional Breakpoints**: Trigger breakpoints under specific conditions.
- **Watch Window**: Monitor variable values.
- **Debug.WriteLine**: Log messages for quick insights.
- **Exception Handling**: Identify and handle errors gracefully.