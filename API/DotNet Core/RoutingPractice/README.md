
# Routing in ASP.NET Core

## Table of Contents

1. [Introduction to Routing](#1-introduction-to-routing)
2. [Routing Concepts](#2-routing-concepts)
    - [Endpoint Routing](#endpoint-routing)
    - [Conventional Routing](#conventional-routing)
    - [Attribute Routing](#attribute-routing)
3. [Middleware and Routing](#3-middleware-and-routing)
4. [Types of Routing](#4-types-of-routing)
    - [Conventional Routing](#conventional-routing-1)
    - [Attribute Routing](#attribute-routing-1)
    - [Custom Routing](#custom-routing)
5. [Route Templates and Constraints](#5-route-templates-and-constraints)
6. [Routing in Controllers and Razor Pages](#6-routing-in-controllers-and-razor-pages)
7. [Dynamic Routing](#7-dynamic-routing)
8. [Custom Route Handlers](#8-custom-route-handlers)
9. [Route Data and Route Values](#9-route-data-and-route-values)
10. [Common Issues and Solutions](#10-common-issues-and-solutions)
11. [Best Practices for Routing](#11-best-practices-for-routing)

---

## [1. Introduction to Routing](#1-introduction-to-routing)

Routing in ASP.NET Core is the process of mapping incoming requests to the appropriate action methods in controllers or Razor pages. It plays a critical role in building web applications by determining how URLs are matched to application logic.

**Example:**

```
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});
```

---

## 2. Routing Concepts

### Endpoint Routing

Introduced in ASP.NET Core 3.0, endpoint routing centralizes routing configuration, making it more powerful and flexible.

- Defined using `UseRouting` and `UseEndpoints` middleware.
- Supports middleware-based authorization and response generation.

### Conventional Routing

Conventional routing is primarily used in MVC applications.

- Routes are defined in `Startup.Configure`.
- Matches URLs to controller actions based on predefined templates.

**Example:**

```
endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

### Attribute Routing

Attribute routing uses attributes on controller actions to define routes.

- More control over individual actions.

**Example:**

```
[Route("api/products")]
public IActionResult GetProducts() { ... }
```

---

## 3. Middleware and Routing

Routing middleware is responsible for evaluating the request and selecting the appropriate endpoint.

- `app.UseRouting()` sets up the middleware for routing.
- `app.UseEndpoints()` maps endpoints to controllers or Razor pages.

Middleware execution order is crucial:

1. `UseRouting`
2. `UseAuthentication`
3. `UseAuthorization`
4. `UseEndpoints`

---

## 4. Types of Routing

### Conventional Routing

- Used in MVC-based applications.
- Routes are configured centrally in `Startup.cs`.

### Attribute Routing

- Offers fine-grained control over individual routes.
- Defined using attributes like `[Route]`, `[HttpGet]`, `[HttpPost]`.

### Custom Routing

- Create custom logic for unique URL patterns.
- Implement `IRouter` for advanced scenarios.

---

## 5. Route Templates and Constraints

Route templates define how URLs are matched.
**Example:**

```
pattern: "{controller=Home}/{action=Index}/{id:int?}"
```

**Constraints:**

- `{id:int}`: Matches integers.
- `{slug:regex(^[a-z0-9-]+$)}`: Matches a specific pattern.

---

## 6. Routing in Controllers and Razor Pages

### Controllers

Use `[Route]` and HTTP method attributes (`[HttpGet]`, `[HttpPost]`) to define routes.
**Example:**

```
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult GetProduct(int id) { ... }
}
```

### Razor Pages

Razor Pages use page-based routing.
**Example:**

```
/Pages/Products/Index.cshtml -> /products
```

---

## 7. Dynamic Routing

Dynamic routing allows changing routes at runtime.

- Useful for generating routes based on database values or user input.

---

## 8. Custom Route Handlers

Custom route handlers give you control over how routes are processed.

- Implement `IRouter` or use endpoint routing for custom behavior.

---

## 9. Route Data and Route Values

Route data contains information about the matched route.
**Accessing Route Data:**

```
var routeValue = HttpContext.GetRouteValue("id");
```

---

## 10. Common Issues and Solutions

### 404 Errors

- Ensure the route pattern matches the incoming request.
- Verify middleware order (`UseRouting` and `UseEndpoints`).

### Conflicting Routes

- Attribute routing can conflict with conventional routes.
- Use route constraints to avoid conflicts.

### Middleware Order

- Routing must be configured before authentication and authorization middleware.

---

## 11. Best Practices for Routing

1. Use attribute routing for better control and readability.
2. Avoid deep nesting in route templates.
3. Use meaningful names for route parameters.
4. Ensure middleware order is correct.
5. Use route constraints to validate inputs.

---

## Conclusion

Routing is an essential feature of ASP.NET Core for building scalable web applications. Understanding the different types of routing and how middleware interacts with routes can help you build more efficient applications.

---

## Bugs and Troubleshooting

1. **404 Errors**: Ensure `UseRouting` and `UseEndpoints` are properly configured.
2. **Middleware Order Issues**: Always place `UseRouting` before authentication and authorization middleware.
3. **Conflicting Routes**: Use constraints and specific route patterns.
