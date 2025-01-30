# JQuery

## **Index**

1. [jQuery Introduction](#jquery-introduction)
2. [Use of jQuery](#use-of-jquery)
3. [Difference between jQuery and JavaScript](#difference-between-jquery-and-javascript)
4. [HTML/CSS Methods of jQuery](#htmlcss-methods-of-jquery)
5. [jQuery Selectors](#jquery-selectors)
6. [Events in jQuery](#events-in-jquery)
   - [Basic Events](#basic-events)
   - [How to Fire an Event Programmatically](#how-to-fire-an-event-programmatically)
   - [Custom Logic on Event Fire](#custom-logic-on-event-fire)
7. [jQuery Validation](#jquery-validation)
   - [Basic Validation](#basic-validation)
   - [Validation with jQuery Validator](#validation-with-jquery-validator)
8. [jQuery Functions](#jquery-functions)
   - [map()](#map)
   - [grep()](#grep)
   - [extend()](#extend)
   - [each()](#each)
   - [merge()](#merge)
9. [Regular Expressions in jQuery](#regular-expressions-in-jquery)
10. [Callback Functions](#callback-functions)
11. [Deferred & Promise Objects](#deferred--promise-objects)


---

## **jQuery Introduction**

jQuery is a fast, small, and feature-rich JavaScript library. It simplifies things like HTML document traversal and manipulation, event handling, and animation. jQuery is designed to make it easier to interact with the DOM and handle browser inconsistencies.

### **Key Features:**

- **DOM Manipulation**: jQuery provides easy ways to manipulate HTML, CSS, and DOM elements.
- **Event Handling**: Simplifies handling browser events (clicks, form submissions, etc.).
- **Animation**: jQuery provides built-in methods to create animations (hide, show, fade, slide, etc.).
- **AJAX**: jQuery simplifies the process of making asynchronous HTTP requests.

---

## **Use of jQuery**

jQuery simplifies JavaScript tasks and provides cross-browser compatibility. Here are some common use cases of jQuery:

1. **DOM Manipulation**: Manipulate HTML elements, CSS styles, and attributes.
2. **Event Handling**: Manage user interactions such as mouse clicks, keyboard inputs, and form submissions.
3. **AJAX Requests**: Simplify making asynchronous HTTP requests to the server and handling responses without reloading the page.
4. **Animations**: Animate DOM elements such as fading, sliding, and changing CSS properties.
5. **Cross-browser Compatibility**: jQuery helps solve inconsistencies in how browsers interpret JavaScript.

---

## **Difference between jQuery and JavaScript**

| **Feature** | **JavaScript** | **jQuery** |
| --- | --- | --- |
| **Syntax** | More verbose and requires handling browser inconsistencies. | Simplifies syntax with short functions and chainable methods. |
| **DOM Manipulation** | Requires more code and manual handling for browser inconsistencies. | Simplifies DOM manipulation with a unified API. |
| **Events** | Event handling requires separate code for each browser. | Provides simple, unified syntax for event handling. |
| **Animation** | Needs manual handling with `setTimeout()` or `setInterval()`. | Provides built-in methods for animations. |
| **AJAX** | Requires creating XMLHttpRequest objects manually. | Simplifies AJAX calls with `.ajax()`, `.get()`, `.post()`. |

---

## **HTML/CSS Methods of jQuery**

jQuery offers a range of methods for manipulating HTML and CSS.

- **HTML Methods**:
    - `.html()` - Gets or sets the inner HTML of an element.
    - `.text()` - Gets or sets the text content of an element.
    - `.val()` - Gets or sets the value of form elements.
- **CSS Methods**:
    - `.css()` - Get or set CSS properties.
    - `.addClass()`, `.removeClass()`, `.toggleClass()` - Manipulate classes.
    - `.width()`, `.height()` - Get or set the width/height of an element.

---

## **jQuery Selectors**

jQuery selectors allow you to target HTML elements easily using CSS-style selectors.

- **Basic Selectors**:
    - `$(element)` - Selects elements by tag name.
    - `$(#id)` - Selects elements by ID.
    - `$(.class)` - Selects elements by class.
- **Attribute Selectors**:
    - `$('[attribute="value"]')` - Selects elements with a specific attribute.

---

## **Events in jQuery**

### **Basic Events**

jQuery simplifies event handling. Common events include `click`, `mouseenter`, `keydown`, and more.

Example:

```jsx

// Handle a click event
$('#button').click(function() {
  alert('Button clicked!');
});
```

### **How to Fire an Event Programmatically**

You can fire events manually using the `.trigger()` or `.triggerHandler()` methods.

Example:

```jsx

// Manually triggering a click event
$('#button').trigger('click');
```

### **Custom Logic on Event Fire**

You can bind custom logic to events, such as creating your own event types or chaining multiple actions.

Example:

```jsx

// Custom event with specific logic
$('#button').on('customEvent', function() {
  alert('Custom event triggered!');
});

$('#button').trigger('customEvent');
```

---

## **jQuery Validation**

### **Basic Validation**

jQuery allows for easy form validation using its built-in methods. You can check if fields are empty or if they match a pattern.

Example:

```jsx

if ($('#email').val() == '') {
  alert('Email is required!');
}
```

### **Validation with jQuery Validator**

jQuery provides a plugin called **jQuery Validation** for more advanced form validation.

Example:

```jsx

$('#myForm').validate({
  rules: {
    email: {
      required: true,
      email: true
    }
  },
  messages: {
    email: {
      required: 'Please enter an email address.',
      email: 'Please enter a valid email address.'
    }
  }
});
```

---

## **jQuery Functions**

### **map()**

The `.map()` function is used to translate or modify the items in a jQuery object.

Example:

```jsx

var numbers = [1, 2, 3, 4];
var doubled = $.map(numbers, function(value) {
  return value * 2;
});
console.log(doubled); // [2, 4, 6, 8]
```

### **grep()**

The `.grep()` function is used to filter elements of an array based on a condition.

Example:

```jsx

var numbers = [1, 2, 3, 4];
var evenNumbers = $.grep(numbers, function(value) {
  return value % 2 === 0;
});
console.log(evenNumbers); // [2, 4]
```

### **extend()**

The `.extend()` function is used to merge objects. It can also be used to extend jQuery with custom methods.

Example:

```jsx

var obj1 = { name: 'John', age: 30 };
var obj2 = { city: 'New York' };
var merged = $.extend(obj1, obj2);
console.log(merged); // { name: 'John', age: 30, city: 'New York' }
```

### **each()**

The `.each()` function iterates over elements in a jQuery object, performing a function for each element.

Example:

```jsx

$('p').each(function(index) {
  console.log('Paragraph ' + (index + 1));
});
```

### **merge()**

The `.merge()` function merges two arrays, removing duplicates.

Example:

```jsx

var array1 = [1, 2, 3];
var array2 = [3, 4, 5];
var merged = $.merge(array1, array2);
console.log(merged); // [1, 2, 3, 3, 4, 5]
```

---

## **Regular Expressions in jQuery**

jQuery supports regular expressions for tasks such as matching patterns in text and validating input fields.

Example:

```jsx

var pattern = /^[a-zA-Z]+$/;
var input = 'Hello';
if (pattern.test(input)) {
  console.log('Valid input!');
} else {
  console.log('Invalid input!');
}
```

---

## **Callback Functions**

A callback function is a function that is passed as an argument to another function and is executed after the completion of that function.

Example:

```jsx
function greet(name, callback) {
  console.log('Hello, ' + name);
  callback();
}

greet('John', function() {
  console.log('Callback executed!');
});
```

---

## **Deferred & Promise Objects**

- **Deferred** objects represent an operation that hasn't completed yet but is expected to. It allows you to register callbacks to handle the completion of the operation.
- **Promise** objects are a more modern approach to handling asynchronous operations. They allow you to chain `.then()` for success and `.catch()` for failure.

Example:

```jsx

var deferred = $.Deferred();

deferred.done(function() {
  console.log('Success!');
}).fail(function() {
  console.log('Failure!');
});

// Resolving the deferred object
deferred.resolve();  // Outputs: 'Success!'
```

```jsx

$.ajax({
  url: 'example.com',
  method: 'GET'
}).done(function(response) {
  console.log('Data received:', response);
}).fail(function() {
  console.log('Error!');
});
```