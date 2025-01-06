# Creating a Web API Project in ASP.NET Framework

This document provides step-by-step instructions to create a Web API project using ASP.NET Framework. Follow these steps to set up your project, configure it, and get started with building your Web API.

## Prerequisites

Before you begin, ensure that you have the following installed on your system:

- **.NET Framework**: Ensure the required version of the .NET Framework is installed on your system.
- **Visual Studio** (2022 or earlier): Install Visual Studio with the "ASP.NET and web development" workload.

## Step 1: Create a New Project

1. **Open Visual Studio**:
    - Launch Visual Studio on your system.
2. **Start a New Project**:
    - Click on "Create a new project".
3. **Select the Template**:
    - From the list of templates, search for **ASP.NET Web Application (.NET Framework)** and select it.
    
    ![image.png](https://i.postimg.cc/J4dRH2LX/image.png)
    
    - Click **Next**.
4. **Configure the Project**:
    - Enter a project name (e.g., `WebApiDemoFramework`).
    - Choose a location for the project.
    - Ensure the "Place solution and project in the same directory" option is checked if desired.
    - Click **Next**.
5. **Choose the Template**:
    - In the "New ASP.NET Web Application" dialog, select **Web API**.
    
    ![image.png](https://i.postimg.cc/5tWdwckX/image-1.png)
    
    - Click **Create**.

## Step 2: Understand the Project Structure

After the project is created, you will see the following structure:

- **Controllers**: Contains the API controllers that define endpoints (e.g., `ValuesController.cs`).
- **App_Start**: Includes configuration files like `WebApiConfig.cs` for setting up routes.
- **Global.asax**: Entry point of the application for handling application-level events.
- **Web.config**: Configuration file for the application settings.

## Step 3: Run the Default Application

1. Press **F5** or **Ctrl+F5** to build and run the application.
2. The application will open in your default browser, displaying the default Web API routes and endpoints.

## Step 4: Add a New Controller

1. **Create a Controller**:
    - Right-click the `Controllers` folder.
    - Select **Add > Controller**.
    - Choose "Web API 2 Controller - Empty".
    - Name the controller (e.g., `ProductController`).
2. **Define Actions**:
    - Add action methods like `GET`, `POST`, `PUT`, and `DELETE` to handle client requests.

Example:

```
using System.Web.Http;

public class ProductController : ApiController
{
    [HttpGet]
    public IHttpActionResult GetAll()
    {
        return Ok(new[] { "Product1", "Product2" });
    }

    [HttpPost]
    public IHttpActionResult AddProduct([FromBody] string product)
    {
        return Created("", product);
    }
}
```

## Step 5: Configure Web API Routes

1. Open the `WebApiConfig.cs` file located in the `App_Start` folder.
2. Ensure the default route template is configured:

```jsx
config.Routes.MapHttpRoute(
    name: "DefaultApi",
    routeTemplate: "api/{controller}/{id}",
    defaults: new { id = RouteParameter.Optional }
);
```