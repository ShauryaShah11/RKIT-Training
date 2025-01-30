# Advance JS

### **Index**

1. [**Session Storage & Local Storage**](#session-storage--local-storage)
   - [Difference Between LocalStorage, SessionStorage, and Cookies](#difference-between-localstorage-sessionstorage-and-cookies)
2. [**Basics of Cookies**](#basics-of-cookies)
3. [**Browser Debugging**](#browser-debugging)
4. [**Inspect Element Window**](#inspect-element-window)
5. [**Caching**](#caching)
6. [**OOJS (Object-Oriented JavaScript)**](#oojs-object-oriented-javascript)
   - [Ways to Implement Classes in JavaScript](#ways-to-implement-classes-in-javascript)
   - [Static Classes and Properties](#static-classes-and-properties)
7. [**ECMAScript 6 (ES6)**](#ecmascript-6-es6)
   - [Difference between `let`, `var`, and `const`](#difference-between-let-var-and-const)
   - [JavaScript Classes](#javascript-classes)
   - [Arrow Functions](#arrow-functions)
   - [Import/Export, `async`, and `await` Functions](#importexport-async-and-await-functions)
8. [**Extra Points**](#extra-points)
   - [Difference between `==` & `===`, `!=` & `!==`](#difference-between-and)

---

### **Advanced JavaScript Concepts**

---

### **Session Storage & Local Storage**

**Session Storage** and **Local Storage** are two types of storage that allow web browsers to store data on the client side. They are both part of the **Web Storage API**, but they differ in their lifetime and scope.

- **Session Storage**: Stores data for the duration of a page session. A session is initiated when the page is loaded and ends when the page or browser tab is closed. The data is cleared when the session ends.
    
    **Example:**
    
    ```jsx
    
    // Setting data in session storage
    sessionStorage.setItem('username', 'john_doe');
    
    // Getting data from session storage
    console.log(sessionStorage.getItem('username')); // 'john_doe'
    ```
    
- **Local Storage**: Unlike session storage, local storage persists even after the browser is closed. It allows data to be stored indefinitely until explicitly removed. Local storage is suitable for storing data that should be available on future visits.
    
    **Example:**
    
    ```jsx
    // Setting data in local storage
    localStorage.setItem('theme', 'dark');
    
    // Getting data from local storage
    console.log(localStorage.getItem('theme')); // 'dark'
    ```
    

### **Difference Between LocalStorage, SessionStorage, and Cookies**

| Feature | **LocalStorage** | **SessionStorage** | **Cookies** |
| --- | --- | --- | --- |
| **Storage Duration** | Data persists until explicitly deleted or browser cache is cleared | Data is cleared once the browser tab is closed | Data persists based on expiration date or session |
| **Storage Capacity** | 5–10 MB depending on the browser | 5–10 MB depending on the browser | Around 4 KB |
| **Scope** | Data is available across tabs and windows in the same origin | Data is available only in the current tab | Data is available across tabs/windows for the same domain |
| **Accessibility** | Only accessible via JavaScript | Only accessible via JavaScript | Accessible by both JavaScript and the server |
| **Expiration** | No expiration date | No expiration date | Expiration date can be set by the server or client |
| **Security** | Accessible only by the same origin (domain) | Accessible only by the same origin (domain) | Cookies can be marked as "HttpOnly" for security, limiting access to JavaScript |
| **Data Transmission** | Not sent with HTTP requests | Not sent with HTTP requests | Sent with every HTTP request to the domain |

---

### **Basics of Cookies**

Cookies are small pieces of data stored in the browser that are sent to the server with each HTTP request. They are often used for storing user preferences, session identifiers, and tracking information.

**Setting a Cookie:**

```jsx

document.cookie = "username=JohnDoe; expires=Thu, 18 Dec 2025 12:00:00 UTC; path=/";
```

**Reading a Cookie:**

```jsx

console.log(document.cookie); // username=JohnDoe
```

**Deleting a Cookie:**

```jsx

document.cookie = "username=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";
```

---

### **Browser Debugging**

Debugging is the process of finding and fixing errors or bugs in your code. The browser's built-in developer tools offer several debugging features:

- **Console**: Logs outputs, errors, and debugging information.
- **Sources Tab**: Allows you to step through your code, set breakpoints, and examine variables in real-time.
- **Network Tab**: Shows all network requests made by the page, including AJAX calls.
- **Performance Tab**: Helps in profiling the performance of a page to find areas that are slowing down your app.

---

### **Inspect Element Window**

The "Inspect Element" window is a tool in most browsers (e.g., Chrome, Firefox) that allows developers to view and interact with the HTML, CSS, and JavaScript of a web page in real-time.

### **Different Tabs in Inspect Element Window**

1. **Elements**: Displays the HTML structure of the page. You can edit the HTML and see changes instantly.
2. **Console**: Shows logs, errors, and warnings generated by the JavaScript running on the page.
3. **Sources**: Displays the JavaScript files loaded by the page, and allows you to set breakpoints and debug code.
4. **Network**: Shows all the network requests made by the page, including API calls, resources, and AJAX requests.
5. **Performance**: Allows you to record and analyze the page's runtime performance to identify bottlenecks.
6. **Application**: Shows information about storage, cookies, local storage, session storage, and indexedDB.
7. **Security**: Displays details about the security of the page, including the SSL certificate and other security-related features.
8. **Memory**: Allows you to track and analyze memory usage, helping you detect memory leaks.

---

### **Caching**

Caching refers to storing data locally so that subsequent requests for the same data can be served faster, without needing to fetch it again from the server.

- **Browser Cache**: Stores images, CSS, JavaScript, and other resources locally to speed up page loading.

---

### **OOJS (Object-Oriented JavaScript)**

**Object-Oriented JavaScript (OOJS)** refers to a programming paradigm that uses objects and classes to organize and structure code. The core concepts of OOJS are:

- **Encapsulation**: Bundling of data (properties) and methods that operate on that data within a single unit (object).
- **Inheritance**: Ability to create a new class based on an existing class, inheriting its properties and methods.
- **Polymorphism**: Ability to use a method or function in multiple ways, depending on the object it is acting upon.

### **Ways to Implement Classes in JavaScript**

1. **Constructor Function**:
    
    ```jsx
    
    function Car(make, model) {
      this.make = make;
      this.model = model;
    }
    const myCar = new Car('Toyota', 'Corolla');
    ```
    
2. **Class Syntax (ES6)**:
    
    ```jsx
    
    class Car {
      constructor(make, model) {
        this.make = make;
        this.model = model;
      }
    }
    const myCar = new Car('Toyota', 'Corolla');
    ```
    

### **Static Classes and Properties**

A **static class** in JavaScript refers to a class that is not instantiated. Static properties and methods belong to the class itself rather than instances of the class.

**Example of Static Class:**

```jsx

class MathUtils {
  static add(a, b) {
    return a + b;
  }
}

console.log(MathUtils.add(5, 3)); // 8
```

---

### **ECMAScript 6 (ES6)**

ECMAScript 6 (also known as ES6 or ES2015) introduced significant changes and new features to JavaScript. These features provide cleaner syntax, better performance, and easier management of large codebases.

### **Difference between `let`, `var`, and `const`**

- **`var`**: Declares variables with function scope. It is hoisted to the top of its scope and initialized with `undefined`.
- **`let`**: Declares block-scoped variables. It is not hoisted and does not initialize with a value.
- **`const`**: Declares block-scoped variables that cannot be reassigned after being initialized.

```jsx
var x = 10;
let y = 20;
const z = 30;

x = 15; // Allowed
y = 25; // Allowed
z = 35; // Error: Assignment to constant variable.
```

---

### **JavaScript Classes**

Classes are blueprints for creating objects. They are syntactical sugar over JavaScript's existing prototype-based inheritance.

**Example:**

```jsx

class Person {
  constructor(name, age) {
    this.name = name;
    this.age = age;
  }

  greet() {
    console.log(`Hello, my name is ${this.name}`);
  }
}

const john = new Person('John', 30);
john.greet(); // 'Hello, my name is John'

```

---

### **Arrow Functions**

Arrow functions provide a concise way to write functions, especially for anonymous functions and callbacks.

```jsx
const add = (a, b) => a + b;
console.log(add(2, 3)); // 5
```

**Differences from regular functions:**

- Arrow functions do not have their own `this` context. They inherit `this` from the enclosing scope.
- They cannot be used as constructors (i.e., you cannot call them with `new`).

---

### **Import/Export, `async`/`await` Functions**

- **`import` and `export`**: Allow you to split your code into modules and manage dependencies.
    
    ```jsx
    
    // Exporting
    export const PI = 3.14;
    
    // Importing
    import { PI } from './math.js';
    ```
    
- **`async` and `await`**: Make asynchronous code look and behave more like synchronous code.
    
    **Example:**
    
    ```jsx
    
    async function fetchData() {
      const response = await fetch('https://api.example.com');
      const data = await response.json();
      console.log(data);
    }
    ```
    

### **Extra Points**

### **Difference Between `==` and `===`, `!=` and `!==`**

- **`==` (Loose equality)**: Compares values, performing type coercion if necessary.
- **`===` (Strict equality)**: Compares values without type coercion. The types must be the same for them to be equal.
- **`!=` (Loose inequality)**: Compares values with type coercion.
- **`!==` (Strict inequality)**: Compares values without type coercion.

```jsx
console.log(5 == '5'); // true
console.log(5 === '5'); // false
console.log(5 != '5'); // false
console.log(5 !== '5'); // true
```