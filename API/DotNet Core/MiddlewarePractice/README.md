# Middleware in ASP.NET Core

## **Table of Contents**

1. [Introduction to Middleware](#1-introduction-to-middleware)
2. [How Middleware Works in ASP.NET Core](#2-how-middleware-works-in-aspnet-core)
3. [Built-in Middleware Overview](#3-built-in-middleware-overview)
4. [Middleware Methods](#4-middleware-methods)
    - [4.1 `app.Use`](#41-appuse)
    - [4.2 `app.Run`](#42-apprun)
    - [4.3 `app.UseWhen`](#43-appusewhen)
5. [Middleware Execution Order and Short-Circuiting](#5-middleware-execution-order-and-short-circuiting)
6. [Creating Custom Middleware](#6-creating-custom-middleware)
    - [6.1 Using Inline Middleware (Delegate)](#61-using-inline-middleware-delegate)
    - [6.2 Creating Middleware Class](#62-creating-middleware-class)
    - [6.3 Middleware with Dependency Injection](#63-middleware-with-dependency-injection)
    - [6.4 Custom Middleware for Error Handling](#64-custom-middleware-for-error-handling)
7. [Advanced Middleware Scenarios](#7-advanced-middleware-scenarios)
8. [Best Practices for Middleware](#8-best-practices-for-middleware)
9. [Conclusion](#9-conclusion)

---

## **1. Introduction to Middleware**

Middleware in ASP.NET Core is a fundamental part of the request pipeline. Each middleware component can process the request and decide whether to pass it to the next component or handle it directly.

### **Key Features of Middleware:**

- Centralized handling of cross-cutting concerns (e.g., authentication, logging).
- Supports asynchronous operations.
- Can modify requests and responses.
- Executed in the order they are registered.

---

## **2. How Middleware Works in ASP.NET Core**

The middleware pipeline is built using a sequence of middleware components that are executed in the order they are added. Each middleware:

1. Receives an `HttpContext` object.
2. Executes its logic.
3. Optionally calls the next middleware using `await next()`.

### **Pipeline Example:**

```csharp
public void Configure(IApplicationBuilder app)
{
    app.Use(async (context, next) =>
    {
        Console.WriteLine("Middleware 1: Before next");
        await next();
        Console.WriteLine("Middleware 1: After next");
    });

    app.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from terminal middleware!");
    });
}

```

**Output:**

```
Middleware 1: Before next
Middleware 1: After next

```

---

## **3. Built-in Middleware Overview**

ASP.NET Core comes with several built-in middleware components for common tasks:

- **Authentication** (`app.UseAuthentication()`)
- **Authorization** (`app.UseAuthorization()`)
- **Exception Handling** (`app.UseExceptionHandler()`)
- **Routing** (`app.UseRouting()`)
- **Static Files** (`app.UseStaticFiles()`)

---

## **4. Middleware Methods**

### **4.1 `app.Use`**

`app.Use` adds middleware to the pipeline that can either modify the request/response or pass control to the next component.

### **Example: Logging Middleware**

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Logging request path: " + context.Request.Path);
    await next();
    Console.WriteLine("Response sent.");
});

```

---

### **4.2 `app.Run`**

`app.Run` is used to add terminal middleware that doesn't pass control to the next component.

### **Example: Terminal Middleware**

```csharp
app.Run(async context =>
{
    await context.Response.WriteAsync("This is the end of the pipeline.");
});

```

---

### **4.3 `app.UseWhen`**

`app.UseWhen` conditionally adds middleware to the pipeline based on a specified condition.

### **Example: Conditional Middleware**

```csharp
app.UseWhen(context => context.Request.Path.StartsWithSegments("/admin"), adminApp =>
{
    adminApp.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("Admin-specific middleware executed.");
    });
});

```

---

## **5. Middleware Execution Order and Short-Circuiting**

### **Middleware Execution Order**

The order in which middleware is added in the `Configure` method of `Startup.cs` determines how requests and responses flow through the pipeline.

1. **Incoming Request Flow:** Middleware is executed in the order it is registered.
2. **Outgoing Response Flow:** Middleware is executed in reverse order.

### **Example of Correct Middleware Order:**

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

```

### **Example of Incorrect Middleware Order:**

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseAuthorization();  // Incorrect: Authorization requires Authentication middleware first.
    app.UseAuthentication();
    app.UseRouting();
}

```

**Why is this incorrect?**

- `UseAuthorization` must come after `UseAuthentication`, as it depends on an authenticated user.
- `UseRouting` should come before `UseAuthorization` to ensure the route data is available.

### **Short-Circuiting Middleware:**

Short-circuiting stops the pipeline and returns a response immediately without calling the next middleware.

### **Example: Short-Circuiting for Missing Headers**

```csharp
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.ContainsKey("X-Custom-Header"))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Bad Request: Missing header.");
        return;
    }
    await next();
});

```

---

## **6. Creating Custom Middleware**

### **6.1 Using Inline Middleware (Delegate)**

This is the simplest way to create middleware using a delegate.

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Custom middleware executed.");
    await next();
});

```

---

### **6.2 Creating Middleware Class**

A middleware class provides better structure and reusability.

```csharp
public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Custom middleware before next.");
        await _next(context);
        Console.WriteLine("Custom middleware after next.");
    }
}

```

**Register the Middleware:**

```csharp
app.UseMiddleware<CustomMiddleware>();

```

---

### **6.3 Middleware with Dependency Injection**

```csharp
public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomMiddleware> _logger;

    public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Custom middleware executed.");
        await _next(context);
    }
}

```

**Register the Middleware:**

```csharp
app.UseMiddleware<CustomMiddleware>();

```

---

### **6.4 Custom Middleware for Error Handling**

```csharp
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync($"An error occurred: {ex.Message}");
    }
});

```

---

## **7. Advanced Middleware Scenarios**

- **Conditional Middleware (`app.UseWhen`)**
- **Global Exception Handling**
- **Middleware in HttpClient (`DelegatingHandler`)**
- **Middleware for Custom Headers**

---

## **8. Best Practices for Middleware**

1. Place middleware in the correct order.
2. Keep middleware small and focused on one task.
3. Use asynchronous methods to avoid blocking.
4. Reuse middleware classes across multiple projects.
5. Log requests and responses for easier debugging.

---

## **9. Conclusion**

Middleware is a powerful mechanism in ASP.NET Core that allows you to control how HTTP requests are processed. By understanding and mastering middleware, you can build highly customizable and maintainable web applications.