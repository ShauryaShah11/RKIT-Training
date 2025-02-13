# .Net Core Fundamental

## **Index**

1. [**.NET Core Overview**](#1-net-core-overview)
2. [**ASP.NET Core**](#2-aspnet-core)
3. [**Project Structure**](#3-project-structure)
4. [**wwwroot Folder**](#4-wwwroot-folder)
5. [**Program.cs**](#5-programcs)
6. [**Startup.cs**](#6-startupcs)
7. [**launchSettings.json**](#7-launchsettingsjson)
8. [**appSettings.json**](#8-appsettingsjson)
9. [**Bugs and Solutions**](#9-bugs-and-solutions)

---

## **1. .NET Core Overview**

### **What is .NET Core?**

.NET Core is an open-source, cross-platform framework developed by Microsoft for building modern applications. It is designed to be modular, lightweight, and optimized for performance. .NET Core supports multiple platforms, including Windows, macOS, and Linux, enabling developers to create applications that run anywhere.

### **Key Features of .NET Core:**

- **Cross-Platform**: Runs on Windows, Linux, and macOS.
- **High Performance**: Optimized for fast execution, making it ideal for cloud-native and microservice architectures.
- **Modular**: You can install only the necessary components, reducing the application's footprint.
- **Built-In Dependency Injection**: A flexible, built-in mechanism to inject dependencies into classes.
- **Asynchronous Programming**: Built-in support for asynchronous programming using `async` and `await` keywords.

### **Example Use Cases:**

- Web applications (ASP.NET Core)
- Microservices
- Cloud-native apps
- IoT applications

---

## **2. ASP.NET Core**

### **What is ASP.NET Core?**

ASP.NET Core is a framework for building modern web applications, including APIs, using the .NET Core runtime. It is designed to be lightweight and fast, providing features such as MVC, Razor Pages, and Web API for building dynamic, data-driven applications.

### **Key Features of ASP.NET Core:**

- **Cross-Platform**: ASP.NET Core runs on Windows, macOS, and Linux.
- **MVC (Model-View-Controller)**: Supports both traditional MVC-based applications and newer RESTful APIs.
- **Built-In Middleware Support**: ASP.NET Core includes middleware to handle things like authentication, routing, and error handling.
- **Unified Programming Model**: Use the same tools for web, mobile, and cloud-based apps.

### **Example: Simple API in ASP.NET Core**

```csharp

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }
}

```

---

## **3. Project Structure**

### **Default Project Structure**

A typical ASP.NET Core project includes several key folders and files that organize your application logically:

- **Controllers**: Contains classes responsible for handling HTTP requests.
- **Models**: Defines the data structure used in the application.
- **Views**: Contains Razor views for MVC-based applications.
- **wwwroot**: Stores static files such as images, JavaScript, and CSS.
- **appsettings.json**: A configuration file for storing settings like connection strings.
- **Program.cs**: The entry point for the application.
- **Startup.cs**: Configures services and middleware for the application.

### **Example of a Project Structure**:

```
bash
CopyEdit
/MyApp
    /Controllers
        ProductsController.cs
    /Models
        Product.cs
    /Views
        /Home
            Index.cshtml
    /wwwroot
        /css
        /js
    /appsettings.json
    /Program.cs
    /Startup.cs

```

---

## **4. wwwroot Folder**

### **What is the `wwwroot` Folder?**

The `wwwroot` folder is the default location for storing static files in an ASP.NET Core application. Files in this folder can be accessed by clients (browsers) directly, such as CSS, JavaScript, and image files.

### **Example: Static File Serving in `Startup.cs`**

```csharp

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
    }

    app.UseStaticFiles(); // Serves static files from wwwroot
}

```

---

## **5. Program.cs**

### **What is `Program.cs`?**

`Program.cs` is the entry point for a .NET Core application. It contains the `Main` method, which is responsible for setting up the host and starting the application.

### **Example of `Program.cs`**

```csharp

public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

public static void Main(string[] args)
{
    CreateHostBuilder(args).Build().Run();
}

```

---

## **6. Startup.cs**

### **What is `Startup.cs`?**

`Startup.cs` is where you configure services and middleware for the application. It is a central point for adding dependencies and defining the request pipeline.

### **Key Methods in `Startup.cs`:**

- **ConfigureServices**: Used for configuring application services like dependency injection.
- **Configure**: Defines how the application will handle HTTP requests.

### **Example of `Startup.cs`**

```csharp

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddScoped<IProductService, ProductService>();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}

```

---

## **7. launchSettings.json**

### **What is `launchSettings.json`?**

`launchSettings.json` stores configuration information used when launching the application in different environments, such as Development or Production. It defines environment variables, the URLs the app should listen on, and other settings.

### **Example of `launchSettings.json`**

```json
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:5000",
      "sslPort": 0
    }
  },
  "profiles": {
    "IIS Express": {
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Project": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "http://localhost:5001"
    }
  }
}

```

---

## **8. appSettings.json**

### **What is `appSettings.json`?**

`appSettings.json` is a configuration file that holds key-value pairs for application settings. These settings can include connection strings, API keys, or any other configuration values.

### **Example of `appSettings.json`**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyAppDb;Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

---

## **9. Bugs and Solutions**

### **1. Dependency Injection Issues in ASP.NET Core**

- **Problem**: Services are not injected correctly.
- **Solution**: Ensure that the services are properly registered in the `ConfigureServices` method:
    
    ```csharp
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IMyService, MyService>();
    }
    ```
    

### **2. Static Files Not Loading from `wwwroot`**

- **Problem**: Static files (CSS, JS, images) are not being served.
- **Solution**: Ensure the middleware to serve static files is configured in `Startup.cs`:
    
    ```csharp
    
    public void Configure(IApplicationBuilder app)
    {
        app.UseStaticFiles(); // This ensures that static files are served correctly
    }
    ```
    

### **3. Route Not Recognized in ASP.NET Core**

- **Problem**: Routes are not being recognized by the application.
- **Solution**: Ensure routing is properly configured:
    
    ```csharp
    
    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
    
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Ensure controllers are properly mapped
        });
    }
    ```
    

### **4. Missing Middleware in `Startup.cs`**

- **Problem**: Middleware is not executing in the correct order.
- **Solution**: Ensure middleware is added in the right order to handle requests properly:
    
    ```csharp
    public void Configure(IApplicationBuilder app)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }
    
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
    }
    ```
    

### **5. Missing Configuration Values in `appSettings.json`**

- **Problem**: Missing or incorrect configuration values.
- **Solution**: Correctly reference configuration settings in `Startup.cs`:
    
    ```csharp
    var connectionString = Configuration.GetConnectionString("DefaultConnection");
    ```