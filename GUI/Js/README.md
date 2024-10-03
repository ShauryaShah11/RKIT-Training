# JS

### What is JavaScript

JavaScript is a high-level, interpreted programming language that is primarily used to create interactive and dynamic content on websites. It can manipulate HTML, handle events, and communicate with servers to update content without refreshing the page. JavaScript is essential for adding interactivity, animations, and complex features to web pages.

### Use of JavaScript

- Validate form inputs (e.g., ensuring emails are properly formatted).
- Create interactive web elements like sliders, drop-down menus, and image galleries.
- Handle user events such as clicks, hovers, and form submissions.
- Dynamically update content on a page without needing a full reload (using AJAX).
- Build web applications that interact with server-side data, like single-page applications (SPAs)

### Different Ways to include JavaScript

- Inline JavaScript
    
    Add JavaScript directly to an HTML element using the `onclick`, `onmouseover`, or similar attributes.
    
    ```jsx
    <button onclick="alert('Hello!')">Click me</button>
    ```
    
- Internal JavaScript
    
    Place JavaScript within the `<script>` tag inside the HTML file
    
    ```jsx
    <script>
      alert('This is internal JavaScript');
    </script>
    ```
    
- External JavaScript
    
    Link to an external `.js` file from your HTML using the `<script>` tag
    
    ```jsx
    <script src="script.js"></script>
    
    ```
    

### Syntax Of JavaScript

JavaScript syntax refers to the rules for writing JavaScript code:

- Statements end with a semicolon (`;`).
- Variables are declared using `var`, `let`, or `const`.
- Functions are defined using the `function` keyword.

```jsx
let name = 'John';
function greet() {
  alert('Hello, ' + name);
}
greet();
```

### Basic Events Of JavaScript

JavaScript can respond to user interactions or events. Some common events include:

- `onclick`: Triggered when an element is clicked.
    
    ```html
    <button onclick="myFunction()">Click me</button>
    ```
    
- `onmouseover`: Triggered when the mouse is moved over an element.
    
    ```html
    <div onmouseover="hoverFunction()">Hover over me</div>
    ```
    
- `onchange`: Triggered when an input field value changes.
    
    ```html
    <input type="text" onchange="changeFunction()">
    ```
    

### Basic Validation With JavaScript

JavaScript is commonly used for client-side form validation before submitting data to a server. This improves user experience by catching errors early.

```jsx
<script>
  function validatePassword() {
    const pass = document.getElementById('password').value;

    // Regular expression checks
    const uppercase = /[A-Z]/; // Matches any uppercase letter
    const uniqueChar = /[!@#$%^&*(),.?":{}|<>]/; // Matches any special character

    // Validate conditions
    if (pass.length < 8 || !uppercase.test(pass) || !uniqueChar.test(pass)) {
      alert('Password must be at least 8 characters long, contain one uppercase letter, and one special character.');
      return false;
    }
    return true;
  }
</script>

<form onsubmit="return validatePassword()">
  <input type="password" id="password" name="password" required>
  <button type="submit">Submit</button>
</form>

```