# ASP.NET Core Filters

# Table of Contents

1. [Introduction to Filters](#introduction-to-filters)
2. [Types of Filters in ASP.NET Core](#types-of-filters-in-aspnet-core)
    - [Authorization Filters](#authorization-filters)
    - [Resource Filters](#resource-filters)
    - [Action Filters](#action-filters)
    - [Exception Filters](#exception-filters)
    - [Result Filters](#result-filters)
3. [Filter Execution Order](#filter-execution-order)
4. [Implementing Built-in Filters](#implementing-built-in-filters)
5. [Creating Custom Filters](#creating-custom-filters)
    - [Custom Action Filter Example](#custom-action-filter-example)
    - [Custom Exception Filter Example](#custom-exception-filter-example)
6. [Filter Dependency Injection](#filter-dependency-injection)
7. [Global vs Controller-Level vs Action-Level Filters](#global-vs-controller-level-vs-action-level-filters)
8. [Common Issues and Debugging](#common-issues-and-debugging)

---

## Introduction to Filters

Filters in ASP.NET Core are used to execute code at specific stages of the request processing pipeline. They allow you to add cross-cutting concerns such as logging, authentication, error handling, caching, and more.

### Why Use Filters?

- Centralized logic for cross-cutting concerns
- Cleaner controller actions
- Reusable and testable components

---

## Types of Filters in ASP.NET Core

ASP.NET Core provides five main types of filters. Each type is executed at a different stage in the request processing pipeline.

### 1. Authorization Filters

- Execute first in the pipeline.
- Used to verify whether the user is authorized to access a specific resource.
- Example: `[Authorize]` attribute.

**Example:**

```csharp
[Authorize(Roles = "Admin")]
public IActionResult AdminPanel()
{
    return View();
}

```

### 2. Resource Filters

- Execute after authorization but before model binding.
- Useful for caching or resource initialization.

**Example:**

```csharp
public class CustomResourceFilter : IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        // Logic before the action executes
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        // Logic after the action executes
    }
}

```

### 3. Action Filters

- Execute before and after the action method execution.
- Used for tasks such as logging, validation, and modification of action arguments.

**Example:**

```csharp
public class LogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Logic before the action executes
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Logic after the action executes
    }
}

```

### 4. Exception Filters

- Handle exceptions thrown during the action execution.
- Ideal for global exception handling.

**Example:**

```csharp
public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new { error = context.Exception.Message })
        {
            StatusCode = 500
        };
    }
}

```

### 5. Result Filters

- Execute after the action method but before the response is sent to the client.
- Useful for modifying or formatting the response.

**Example:**

```csharp
public class ResultModificationFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // Modify response before it's sent to the client
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // Logic after the response is sent
    }
}

```

---

## Filter Execution Order

1. Authorization Filters
2. Resource Filters
3. Action Filters (OnActionExecuting)
4. Action Method Execution
5. Action Filters (OnActionExecuted)
6. Result Filters (OnResultExecuting)
7. Result Execution
8. Result Filters (OnResultExecuted)
9. Exception Filters (on exception only)

---

## Implementing Built-in Filters

ASP.NET Core provides several built-in filters:

- `[Authorize]`: Authorization filter
- `[ServiceFilter]`: Inject services as filters
- `[ExceptionFilter]`: Handle exceptions globally

**Using a Built-in Exception Filter:**

```csharp
[ServiceFilter(typeof(CustomExceptionFilter))]
public IActionResult Index()
{
    throw new Exception("An error occurred!");
}

```

---

## Creating Custom Filters

### Custom Action Filter Example

```csharp
public class CustomActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Before action execution");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("After action execution");
    }
}

```

### Custom Exception Filter Example

```csharp
public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new { message = "An error occurred" })
        {
            StatusCode = 500
        };
    }
}

```

---

## Filter Dependency Injection

Filters can have dependencies injected into their constructors using ASP.NET Core's built-in dependency injection.

**Example:**

```csharp
public class CustomActionFilter : IActionFilter
{
    private readonly ILogger<CustomActionFilter> _logger;

    public CustomActionFilter(ILogger<CustomActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Action is executing");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Action executed");
    }
}

```

---

## Global vs Controller-Level vs Action-Level Filters

- **Global Filters**: Apply to all controllers and actions.
    - Configure in `Startup.cs`.
- **Controller-Level Filters**: Apply to all actions within a controller.
    - Add filter attributes to the controller.
- **Action-Level Filters**: Apply to a specific action method.
    - Add filter attributes to the method.

---

## Common Issues and Debugging

1. **Filter Not Executing**
    - Ensure the filter is registered correctly.
    - Check the order of execution.
2. **Dependency Injection Issues**
    - Make sure services are registered in `ConfigureServices`.
3. **Global Exception Filter Not Catching Errors**
    - Check if middleware is handling exceptions before the filter.

---

## Conclusion

Filters in ASP.NET Core provide a powerful way to manage cross-cutting concerns in your application. Understanding the different types and their execution order allows you to create clean, maintainable, and testable code.