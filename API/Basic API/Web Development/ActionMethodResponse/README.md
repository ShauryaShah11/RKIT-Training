# Action Method Responses in ASP.NET Web API

This document explains various action method response patterns used in ASP.NET Web API projects, focusing on the `UserController` example. It covers standard and customized responses for different HTTP methods.

---

## 1. **Types of Action Method Responses**

In ASP.NET Web API, action methods return different types of responses based on the requirements:

### 1.1 **Void**

- **Description**: Used when no data needs to be returned.
- **Example**: `HttpDelete` methods that simply perform an operation.

### 1.2 **Primitive Types**

- **Description**: Used to return simple values like `int`, `string`, or `bool`.
- **Example**: Returning a status or a specific value.

### 1.3 **Custom Objects**

- **Description**: Returns user-defined objects.
- **Example**: Returning a `User` object with detailed information.

### 1.4 **IHttpActionResult**

- **Description**: Provides flexibility with built-in response types like `Ok()`, `BadRequest()`, `NotFound()`, etc.
- **Example**: Used in `POST` or `PUT` methods to indicate the success or failure of the operation.

### 1.5 **HttpResponseMessage**

- **Description**: Offers detailed control over HTTP responses, including headers, status codes, and content.
- **Example**: Used for complex response scenarios requiring custom headers or error handling.

---

## 2. **Action Methods Overview**

### 2.1 Delete User by ID

```
[HttpDelete]
public void DeleteUserById(int userId)
{
    _userRepository.DeleteUser(userId);
}
```

- **Description**: Deletes a user by their unique identifier.
- **Response**: Returns `204 No Content` if successful.

---

### 2.2 Retrieve User by ID

```
[HttpGet]
public User GetUserById(int id)
{
    return _userRepository.GetUserById(id);
}
```

- **Description**: Retrieves a user by ID.
- **Response**: Returns the user object or `null` if not found.

---

### 2.3 Retrieve User with Custom HTTP Response

```
[HttpGet]
[Route("api/user/message/{id}")]
public HttpResponseMessage GetUserWithCustomMessage(int id)
{
    try
    {
        HttpResponseMessage response = new HttpResponseMessage();
        User user = _userRepository.GetUserById(id);

        if (user == null)
        {
            response.StatusCode = HttpStatusCode.NotFound;
            response.ReasonPhrase = "User not found";
            response.Content = new StringContent("The requested user does not exist.", Encoding.UTF8, "text/plain");
            response.Headers.Add("X-Error-Code", "404");
            return response;
        }

        response.StatusCode = HttpStatusCode.OK;
        response.Content = new ObjectContent<User>(user, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
        response.ReasonPhrase = "User retrieved successfully";
        response.Headers.Add("X-Custom-Header", "OperationSuccess");
        response.Version = new Version(1, 1);
        response.RequestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/user/message/{id}");
        return response;
    }
    catch (Exception ex)
    {
        var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent($"An error occurred: {ex.Message}", Encoding.UTF8, "text/plain"),
            ReasonPhrase = "Internal Server Error",
            Version = new Version(1, 1)
        };
        errorResponse.Headers.Add("X-Error-Code", "500");
        errorResponse.Headers.Add("X-Error-Detail", ex.Message);
        return errorResponse;
    }
}
```

- **Description**: Provides detailed HTTP responses, including headers and error codes.
- **Success Response**:
    - **Status**: 200 OK
    - **Headers**: `X-Custom-Header`
- **Error Response**:
    - **Status**: 404 Not Found / 500 Internal Server Error
    - **Headers**: `X-Error-Code`, `X-Error-Detail`

---

### 2.4 Retrieve User with Default Response

```
[HttpGet]
[Route("api/user/message/{id}")]
public HttpResponseMessage GetUserWithDefaultMessage(int id)
{
    User user = _userRepository.GetUserById(id);
    if (user == null)
    {
        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
    }
    return Request.CreateResponse(HttpStatusCode.OK, user);
}
```

- **Description**: Uses built-in `Request` object for simple response creation.
- **Response**:
    - **200 OK**: User object.
    - **404 Not Found**: Error message.

---

### 2.5 Add New User

```
[HttpPost]
public IHttpActionResult AddUser(User user)
{
    if (user == null)
    {
        return BadRequest("User data is missing");
    }
    if (_userRepository.AddUser(user))
    {
        return Created($"api/user/{user.UserId}", user);
    }
    return Conflict();
}
```

- **Description**: Adds a new user.
- **Response**:
    - **201 Created**: User created successfully.
    - **400 Bad Request**: User data is missing.
    - **409 Conflict**: Unable to add user due to conflict.

---

### 2.6 Update Existing User

```
[HttpPut]
public IHttpActionResult UpdateUser(User user)
{
    if (user == null)
    {
        return BadRequest("User data is missing");
    }
    if (_userRepository.UpdateUser(user))
    {
        return Ok(user);
    }
    return NotFound();
}
```

- **Description**: Updates a user.
- **Response**:
    - **200 OK**: User updated.
    - **400 Bad Request**: Missing data.
    - **404 Not Found**: User not found.

---

### 2.7 Retrieve All Users

```
[HttpGet]
[Route("api/users")]
public IEnumerable<User> GetAllUsers()
{
    return _userRepository.GetAllUsers();
}
```

- **Description**: Retrieves all users.
- **Response**: Returns a list of user objects.

---

## Step 5: Configure Web API Routes

1. Open the `WebApiConfig.cs` file located in the `App_Start` folder.
2. Ensure the default route template is configured:

```
config.Routes.MapHttpRoute(
    name: "DefaultApi",
    routeTemplate: "api/{controller}/{id}",
    defaults: new { id = RouteParameter.Optional }
);
```

1. Save the file and close it.

---

## Step 6: Testing Your Web API

1. **Run the Application**:
    - Press `F5` or run the application in your IDE.
2. **Use a Tool for Testing**:
    - Use tools like Postman, Swagger, or curl to test your API endpoints.
3. **Test Example Endpoints**:
    - **GET**: `http://localhost:<port>/api/users`
    - **POST**: `http://localhost:<port>/api/user`
    - **PUT**: `http://localhost:<port>/api/user`
    - **DELETE**: `http://localhost:<port>/api/user/{id}`
4. **Verify Responses**:
    - Ensure the responses align with the expected HTTP status codes and data.

---

## Summary

This document showcases different approaches to handle action method responses in ASP.NET Web API. Examples include:

- Using `HttpResponseMessage` for detailed control over responses.
- Utilizing built-in methods like `Request.CreateResponse` and `Request.CreateErrorResponse`.
- Employing `IHttpActionResult` for simplicity and built-in HTTP response types.

Each method demonstrates a unique way of handling API responses, ensuring flexibility and clarity in API design.