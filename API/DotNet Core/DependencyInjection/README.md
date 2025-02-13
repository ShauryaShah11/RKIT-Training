# Dependency Injection (DI) in ASP.NET Core

## **Table of Contents**

1. [Introduction to Dependency Injection](#introduction-to-dependency-injection)
2. [Built-in IoC Container](#built-in-ioc-container)
3. [Registering Application Services](#registering-application-services)
4. [Understanding Service Lifetime](#understanding-service-lifetime)
    - [Transient](#transient)
    - [Scoped](#scoped)
    - [Singleton](#singleton)
5. [Extension Methods for Registration](#extension-methods-for-registration)
6. [Constructor Injection](#constructor-injection)
7. [Conclusion](#conclusion)

---

## **Introduction to Dependency Injection**

**Dependency Injection (DI)** is a technique for achieving **Inversion of Control (IoC)** between classes and their dependencies. ASP.NET Core has a built-in IoC container that makes it easy to manage dependencies throughout the application.

### **Why Use Dependency Injection?**

- Promotes **loose coupling** and better **testability**.
- Centralized management of service instances.
- Reduces boilerplate code for object creation and lifetime management.

---

## **Built-in IoC Container**

ASP.NET Core comes with a **built-in IoC (Inversion of Control) container** that manages service registrations and resolves dependencies automatically.

### **Features of the Built-in IoC Container:**

- Supports **constructor injection** by default.
- Configurable in the `Startup.cs` file via `ConfigureServices` method.
- Built to be **extensible**, allowing integration with third-party IoC containers if required (e.g., Autofac, StructureMap).

**Example:**

```csharp

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddScoped<IMyService, MyService>();
}
```

---

## **Registering Application Services**

To use DI, you must register your services in the **`ConfigureServices`** method in `Startup.cs`.

### **Registration Syntax:**

```csharp

services.Add{Lifetime}(IServiceType, ImplementationType);
```

### **Example:**

```csharp

services.AddTransient<IEmailService, EmailService>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddSingleton<ILogger, Logger>();
```

---

## **Understanding Service Lifetime**

ASP.NET Core provides three service lifetimes that define how and when a service instance is created.

### 1. **Transient**

- A **new instance** is created **every time** the service is requested.
- Suitable for **stateless services**.

**Example:**

```csharp

services.AddTransient<IService, MyService>();
```

### 2. **Scoped**

- A **single instance** is created per **HTTP request**.
- Suitable for **per-request services**, such as database context objects.

**Example:**

```csharp

services.AddScoped<IService, MyService>();
```

### 3. **Singleton**

- A **single instance** is created and **shared across the entire application**.
- Suitable for **stateful services** or expensive object creation.

**Example:**

```csharp

services.AddSingleton<IService, MyService>();
```

| **Lifetime** | **Instance per** | **Use Case** |
| --- | --- | --- |
| **Transient** | Request | Lightweight, stateless services |
| **Scoped** | HTTP Request | Services with a per-request lifecycle |
| **Singleton** | Application (shared instance) | Shared data or configuration services |

---

## **Extension Methods for Registration**

To keep service registration clean and organized, use **extension methods**.

**Example:**

1. Create an extension class:

```csharp

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
```

1. Use the extension method in `Startup.cs`:

```csharp

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddCustomServices();
}
```

---

## **Constructor Injection**

**Constructor injection** is the most common way to inject dependencies into a class. ASP.NET Core automatically provides the required services when it instantiates the class.

### **Example:**

```csharp
public class HomeController : Controller
{
    private readonly IEmailService _emailService;

    public HomeController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        _emailService.SendEmail("example@example.com", "Welcome", "Hello!");
        return View();
    }
}

```

In this example, the `IEmailService` dependency is injected into the `HomeController` through its constructor.

---

## **Conclusion**

- Dependency Injection (DI) in ASP.NET Core simplifies application development by managing object lifetimes and dependencies.
- The **built-in IoC container** provides **transient**, **scoped**, and **singleton** lifetimes for services.
- Use **constructor injection** to inject services into controllers, and organize service registrations using **extension methods** for cleaner code.