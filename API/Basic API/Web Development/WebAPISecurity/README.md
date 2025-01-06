# Security in Web API

## Overview

In modern web applications, ensuring the security of APIs is critical to protect sensitive data and functionality. This document explains the key security methods used in Web APIs, including:

- [CORS (Cross-Origin Resource Sharing)](#cors-cross-origin-resource-sharing): Managing cross-origin requests.
- [Authentication](#authentication): Verifying user identity.
- [Authorization](#authorization): Controlling user access.
- [JWT Tokens (JSON Web Tokens)](#jwt-tokens-json-web-tokens): Ensuring secure and stateless communication.
- [Exception Handling](#exception-handling): Managing errors and providing secure responses.

We will start by detailing **CORS** and its role in securing APIs.

---

## [Cross-Origin Resource Sharing (CORS)](#cors-cross-origin-resource-sharing)

### What is CORS?

CORS is a security feature implemented by web browsers to restrict how resources on a web page can be requested from another domain. By default, browsers block such cross-origin requests. CORS allows servers to specify which domains are permitted to access their resources, ensuring better security and control.

### Why Use CORS?

1. **Enable Cross-Origin Requests:** Allow safe communication between different origins (e.g., from a React frontend hosted on one domain to an API hosted on another).
2. **Prevent Unauthorized Access:** Restrict access to trusted domains only.
3. **Avoid Browser Restrictions:** Ensure your application works seamlessly across different clients and platforms.

### Advantages of CORS

- **Granular Control:** Configure specific origins, headers, and methods.
- **Improved Security:** Prevents unauthorized domains from accessing your API.
- **Enhanced Compatibility:** Allows integration with modern Single Page Applications (SPAs).

### Disadvantages of CORS

- **Complex Configuration:** Misconfigured CORS policies can inadvertently allow access to malicious actors.
- **Performance Overhead:** Adds preflight requests, increasing latency.
- **Limited to Browsers:** CORS does not apply to non-browser clients (e.g., Postman).

### CORS Implementation in Web API

### Step 1: Install the CORS Package

You need to install the `Microsoft.AspNet.WebApi.Cors` package. This can be done via the NuGet Package Manager.

### Using NuGet Package Manager Console:

Run the following command in the Package Manager Console:

```mathematica
Install-Package Microsoft.AspNet.WebApi.Cors
```

### Using the .NET CLI:

If you are using .NET CLI, you can install it with the following command:

```csharp
dotnet add package Microsoft.AspNet.WebApi.Cors
```

### Step 2: Enable CORS in the Web API Configuration

Once the package is installed, you can enable CORS globally or for specific controllers and actions.

### Global CORS Configuration:

In your `WebApiConfig.cs`, you need to enable CORS by adding `config.EnableCors()`.

```csharp
public static void Register(HttpConfiguration config)
{
    // Enable CORS for cross-origin requests globally
    config.EnableCors();

    // Other configurations...
    config.MapHttpAttributeRoutes();
    config.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional }
    );
}
```

### Step 3: Configure CORS for Specific Controllers

You can also enable CORS on a per-controller or per-action basis by using the `[EnableCors]` attribute.

```csharp
[EnableCors(origins: "https://myfrontend.com", headers: "*", methods: "GET,POST")]
public class UserController : ApiController
{
    [HttpGet]
    public IHttpActionResult GetAllUsers()
    {
        // Logic to fetch all users
        return Ok(users);
    }
}

```

### Explanation of Parameters in `[EnableCors]`

- **`origins`:** Specifies the allowed domains (e.g., `"https://example.com"`). Use `"*"` to allow all origins.
- **`headers`:** Specifies which headers are allowed in the request.
- **`methods`:** Specifies which HTTP methods are allowed (e.g., GET, POST).

### Real-World Example

Consider a React application hosted on `https://myfrontend.com` that needs to interact with the API at `https://myapi.com`. To allow this interaction, you would configure CORS as follows:

```
[EnableCors(origins: "https://myfrontend.com", headers: "*", methods: "GET,POST")]
public class UserController : ApiController
{
    // Controller methods
}
```

This ensures that only requests from `https://myfrontend.com` are allowed, enhancing security.

---

## [Authentication](#authentication)

Authentication is the process of verifying the identity of a user or system. For Web APIs, it ensures that the user or system accessing the API is who they claim to be.

### Steps for Implementing Authentication in Web API

1. **Receive Credentials:** When a user tries to access a secured resource, the application first needs to receive their credentials, typically through a login form (e.g., username and password).
2. **Validate Credentials:** The API checks whether the provided credentials match those stored in the database or another source (e.g., LDAP).
3. **Generate Token (JWT):** Upon successful authentication, the system generates a JWT token containing user claims (like user ID, role, etc.) and sends it back to the client.
4. **Use Token for Further Requests:** The client includes the JWT token in subsequent requests (usually in the `Authorization` header), allowing the server to verify the token and ensure the user is authenticated.

---

## Packages to Install

To implement JWT Authentication in your Web API, you'll need to install the following packages:

1. **Microsoft.IdentityModel.Tokens**: For creating and validating JWT tokens.
    
    ```bash
    Install-Package Microsoft.IdentityModel.Tokens
    ```
    
2. **System.IdentityModel.Tokens.Jwt**: For handling JWT token operations.
    
    ```bash
    Install-Package System.IdentityModel.Tokens.Jwt
    ```
    

---

## Token Service

The `TokenService` is responsible for generating and validating JWT tokens. Below is the implementation of the `TokenService` class:

```csharp
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPISecurity.Models;

namespace WebAPISecurity.Services
{
    public static class TokenService
    {
        private const string SecretKey = "1234567890abcdef1234567890abcdef"; // Use a strong secret key
        private static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        private static readonly SigningCredentials Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

        public static string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("sub", user.Username),
                new Claim("email", user.Email),
                new Claim("userId", user.UserId.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "AuthService",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "AuthService",
                ValidAudience = "your_audience",
                IssuerSigningKey = Key
            };

            try
            {
                return tokenHandler.ValidateToken(token, parameters, out _);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Invalid or expired token", ex);
            }
        }
    }
}
```

---

## AuthController: User Login and Token Generation

The `AuthController` handles the login functionality by validating the user's credentials and generating a JWT token if authentication is successful. Here's how you can implement the `AuthController`:

```csharp
using System.Linq;
using System.Web.Http;
using WebAPISecurity.Models;
using WebAPISecurity.Repositories;
using WebAPISecurity.Services;

namespace WebAPISecurity.Controllers
{
    public class AuthController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] User loginUser)
        {
            User user = _userRepository.GetAllUsers()
                .FirstOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            string token = TokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }
    }
}
```

### Explanation

1. **Login API Endpoint (`POST: api/auth/login`)**:
    - The API accepts a `User` object containing the `Username` and `Password`.
    - The controller retrieves the user from the repository by matching the credentials.
    - If the user exists, the controller generates a JWT token using the `TokenService`.
    - The token is returned in the response body.
2. **User Authentication**:
    - The `User` object must be validated against a stored database (e.g., a `UserRepository` class).
    - The JWT token, once generated, will be returned to the client, and the client must include it in subsequent requests for accessing protected endpoints.

## [Authorization](#authorization)

Authorization is the process of granting or denying access to resources or actions based on the user's permissions. It ensures that authenticated users can only access the specific resources they are allowed to interact with, based on their roles or privileges.

This section describes the custom authorization mechanism for securing API endpoints using **Bearer tokens**. The `BearerAuth` attribute is a custom implementation to validate JWT (JSON Web Tokens) tokens passed through the Authorization header in HTTP requests.

### Key Components

1. **BearerAuth Custom Attribute** - The custom attribute that performs token validation and sets the authenticated user.
2. **TokenService** - A service responsible for token validation.
3. **UserController** - The controller that uses the `BearerAuth` attribute to secure endpoints.

## 1. BearerAuth Custom Attribute

The `BearerAuth` attribute inherits from `AuthorizeAttribute` and is used to validate the **Bearer token** sent in the request's `Authorization` header.

### Code: `BearerAuth.cs`

```csharp
public class BearerAuth : AuthorizeAttribute
{
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
            actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
        {
            return;
        }

        var authHeader = actionContext.Request.Headers.Authorization;
        if (authHeader == null || authHeader.Scheme != "Bearer" || string.IsNullOrWhiteSpace(authHeader.Parameter))
        {
            HandleUnauthorizedRequest(actionContext);
            return;
        }

        try
        {
            var token = authHeader.Parameter;
            var principal = TokenService.ValidateToken(token);

            Thread.CurrentPrincipal = principal;
            actionContext.RequestContext.Principal = principal;
        }
        catch (UnauthorizedAccessException)
        {
            HandleUnauthorizedRequest(actionContext);
        }
    }

    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        actionContext.Response = actionContext.Request.CreateResponse(
            HttpStatusCode.Unauthorized,
            new { message = "Unauthorized access" }
        );
    }
}
```

### Explanation:

- **OnAuthorization**: This method is executed before the controller action is executed. It checks if the request has a valid Bearer token in the `Authorization` header.
- **HandleUnauthorizedRequest**: If the token is invalid or missing, it sends an `Unauthorized` HTTP response.

### Token Validation

In the code above, the `TokenService.ValidateToken(token)` is responsible for decoding and validating the JWT token. If the token is invalid, it throws an `UnauthorizedAccessException`.

## 2. TokenService

This service is used to validate the Bearer token passed in the request header.

```csharp
public static class TokenService
{
    public static ClaimsPrincipal ValidateToken(string token)
    {
        // Example token validation logic using JWT libraries
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken == null || !jsonToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new UnauthorizedAccessException();
        }

        // Perform more validation (e.g., check issuer, audience, etc.)

        return new ClaimsPrincipal(new ClaimsIdentity(jsonToken?.Claims));
    }
}
```

### Explanation:

- The token is parsed and decoded to extract claims.
- Additional validation can be done on the token’s claims (e.g., issuer, audience).
- A `ClaimsPrincipal` is created from the valid token, and the user context is set for the request.

## 3. Using the `BearerAuth` Attribute in Controller

To use the `BearerAuth` attribute and secure endpoints, you can apply it to controller actions. This ensures that only authenticated users with a valid JWT token can access these actions.

### Code: `UserController.cs`

```csharp
[EnableCors(origins: "*", headers: "*", methods: "*")]
public class UserController : ApiController
{
    private readonly UserRepository _userRepository = new UserRepository();

    [HttpGet]
    public IHttpActionResult GetAllUsers()
    {
        List<User> users = _userRepository.GetAllUsers();
        return Ok(users);
    }

    [HttpGet]
    public IHttpActionResult GetUserById(int id)
    {
        User user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet]
    [Route("api/user/token")]
    [BearerAuth] // Apply custom authorization
    public IHttpActionResult GetUserbyToken()
    {
        var principal = User as ClaimsPrincipal;

        if (principal != null)
        {
            string username = principal.FindFirst("sub")?.Value; // "sub" claim
            string email = principal.FindFirst("email")?.Value; // "email" claim
            int userId = int.Parse(principal.FindFirst("userId")?.Value); // "userId" claim

            User user = _userRepository.GetAllUsers().FirstOrDefault(u => u.UserId == userId);
            return Ok(user);
        }

        return Unauthorized();
    }

    // Additional actions for AddUser, UpdateUser, DeleteUser
}
```

### Explanation:

- The `BearerAuth` attribute is used on the `GetUserbyToken` action to secure it. Only requests with a valid Bearer token can access this endpoint.
- The action retrieves user claims (username, email, userId) from the authenticated token and fetches the user from the repository.

## 4. API Flow for Authorization

1. **Token Generation**: The client first sends credentials (e.g., username and password) to an authentication endpoint to receive a Bearer token.
2. **Authorization Header**: In subsequent requests to secured API endpoints, the client includes the token in the `Authorization` header like so:`Authorization: Bearer <token>`
3. **Token Validation**: The `BearerAuth` attribute validates the token using `TokenService.ValidateToken(token)`.
4. **Authenticated Principal**: Upon successful validation, the `ClaimsPrincipal` is set to the current HTTP context, allowing the user to access protected resources.
5. **Unauthorized Requests**: If the token is invalid, expired, or missing, the server responds with a 401 Unauthorized error.

## [Exception Handling](#exception-handling)

Exception handling is a crucial aspect of application development. In a Web API, when an exception occurs during the execution of an API action, it’s important to handle these exceptions gracefully to provide meaningful feedback to the user, while also ensuring that debugging information is available for developers.

In Web API, we can handle exceptions globally through a **Global Exception Filter**. This approach allows us to catch unhandled exceptions in a centralized way, rather than having to add exception-handling logic in each individual action method.

---

## Global Exception Filter (`GlobalExceptionFilter`)

A **GlobalExceptionFilter** is a custom filter that catches all unhandled exceptions thrown by Web API actions. Instead of throwing an error directly to the client, the filter provides a custom error response with a general error message, exception details, and the stack trace.

### Code Implementation

```csharp
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebAPISecurity.Filters
{
    /// <summary>/// Global Exception Filter to handle unhandled exceptions globally in the Web API.
    /// This filter catches exceptions thrown by actions and returns a custom error response.
    /// </summary>public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>/// This method is triggered when an exception occurs in any API action.
        /// </summary>/// <param name="context">The context that contains the exception details.</param>public override void OnException(HttpActionExecutedContext context)
        {
            // Log the exception details (for debugging and monitoring purposes)
            // You can implement a logging mechanism to log the exception information to a file, database, or external service

            Exception exception = context.Exception;

            // Prepare a custom response message with the error details
            // The message will include the general error message, the exception message, and stack trace
            object responseMessage = new
            {
                Message = "An unexpected error occurred.", // General user-friendly error message
                ExceptionMessage = exception.Message,    // The specific exception message
                exception.StackTrace                      // Stack trace for debugging
            };

            // Set the response to be sent back to the client, with HTTP 500 status code (Internal Server Error)
            // The response includes the error message and details
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
        }
    }
}
```

### Explanation

- **GlobalExceptionFilter**: This class inherits from `ExceptionFilterAttribute` and overrides the `OnException` method. It is used to handle exceptions globally in Web API.
- **OnException Method**: This method is triggered whenever an exception occurs in any API action. It prepares a custom error response containing:
    - A general message for the user.
    - Specific details like the exception message and stack trace for debugging.
- **Response Handling**: The filter sets the response to a `500 Internal Server Error` with the error details.

---

## Registering the Global Exception Filter

To use the **GlobalExceptionFilter** in your Web API, you need to register it in the `WebApiConfig` class. This ensures that the filter will be applied to all actions globally across the application.

### Steps for Registration

1. Open the `WebApiConfig.cs` file located in the `App_Start` folder of your Web API project.
2. Add the following line to register the filter inside the `Register` method:

```csharp
public static void Register(HttpConfiguration config)
{
    // Register the Global Exception Filter
    config.Filters.Add(new GlobalExceptionFilter());

    // Other Web API configuration code...
}
```

This line adds the **GlobalExceptionFilter** to the Web API configuration, ensuring that it will handle any unhandled exceptions globally.

---

## Other Methods for Exception Handling

While the **GlobalExceptionFilter** provides a centralized approach to exception handling, there are other ways to handle exceptions in Web API:

### 1. **Try-Catch Blocks within Controller Actions**

You can also catch exceptions directly within your controller methods by using try-catch blocks.

```csharp
public IHttpActionResult Get(int id)
{
    try
    {
        var data = GetDataById(id);
        return Ok(data);
    }
    catch (Exception ex)
    {
        return InternalServerError(ex); // Returns a 500 response with the exception details
    }
}
```

### 2. **Using `HttpResponseException`**

You can throw an `HttpResponseException` to return a custom HTTP response with status codes.

```csharp
if (someCondition)
{
    throw new HttpResponseException(HttpStatusCode.BadRequest); // Returns 400 Bad Request
}
```

### 3. **Custom Error Responses**

You can create custom error response models for consistency in your error responses:

```csharp
public class ErrorResponse
{
    public string Message { get; set; }
    public string Details { get; set; }
}
```

In your action methods, you can then return a custom error response like this:

```csharp
return Content(HttpStatusCode.InternalServerError, new ErrorResponse
{
    Message = "An unexpected error occurred.",
    Details = exception.Message
});
```

---

## Best Practices for Exception Handling

1. **Log Exceptions**: Always log exceptions for debugging and monitoring purposes. Use a logging library like NLog, Log4Net, or Serilog.
2. **Provide User-Friendly Error Messages**: Avoid sending sensitive exception details (e.g., stack trace) to the client in production. Return a user-friendly message instead.
3. **Use Correct HTTP Status Codes**: Ensure that you return the appropriate status codes based on the type of error (e.g., `400` for bad requests, `500` for internal server errors).
4. **Centralize Error Handling**: Use global exception filters or middleware to handle errors uniformly across the application.