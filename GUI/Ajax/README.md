# AJAX

## **Index**

- [**What is Ajax?**](#what-is-ajax)
- [**Use of Ajax**](#use-of-ajax)
- [**How to send data with Ajax Request?**](#how-to-send-data-with-ajax-request)
- [**Difference between GET, POST, PUT, DELETE Method**](#difference-between-get-post-put-delete-method)
- [**JSON Data**](#json-data)
- [**Serialization & De-serialization**](#serialization--de-serialization)

---

## **What is Ajax?**

AJAX (Asynchronous JavaScript and XML) is a technique for creating dynamic and interactive web pages. It allows web pages to load content asynchronously (in the background) without refreshing the whole page. This technique uses a combination of technologies: JavaScript, XMLHttpRequest (XHR), and often JSON, though it originally used XML.

### **Key Features:**

- **Asynchronous**: The browser doesn't need to reload the page to receive new data.
- **Dynamic Updates**: Content can be updated without reloading the entire page, providing a seamless user experience.
- **Improved Performance**: By only requesting the data that’s needed, AJAX reduces server load and enhances performance.

### **Example:**

AJAX can be used to fetch new data from the server without refreshing the webpage. For example, when a user scrolls down a page, additional content can be loaded via an AJAX request.

---

## **Use of Ajax**

AJAX is used to update parts of a web page without requiring the whole page to be reloaded. Some common uses of AJAX include:

- **Loading New Content**: Dynamically loading content when users scroll, like news feeds or social media posts.
- **Form Submission**: Submitting forms without refreshing the page, improving the user experience.
- **Auto-suggestions**: Sending search queries to the server and displaying results instantly.
- **Background Data Fetching**: Fetching data (e.g., user stats, notifications) from the server without disrupting the user’s current view.

### **Example:**

```jsx

// AJAX request to fetch user data
$.ajax({
  url: '/api/user', // Endpoint for user data
  method: 'GET',     // Method for fetching data
  success: function(response) {
    console.log(response);
  },
  error: function() {
    console.log('Error fetching user data');
  }
});
```

---

## **How to send data with Ajax Request?**

To send data with an AJAX request, you use the `data` option in the `$.ajax()` method. This allows you to send data to the server, such as form input or JSON objects.

### **Example (Sending Data with POST):**

```jsx

$.ajax({
  url: '/submit-form',
  type: 'POST',
  data: { name: 'John', age: 30 }, // Data being sent to the server
  success: function(response) {
    console.log('Data submitted successfully');
  },
  error: function() {
    console.log('Error submitting data');
  }
});
```

### **Sending JSON Data:**

```jsx

$.ajax({
  url: '/submit-json',
  type: 'POST',
  contentType: 'application/json',
  data: JSON.stringify({ name: 'John', age: 30 }), // Sending JSON data
  success: function(response) {
    console.log('JSON data submitted successfully');
  },
  error: function() {
    console.log('Error submitting JSON data');
  }
});
```

In this example, we use `JSON.stringify()` to convert the data to JSON format before sending it to the server.

---

## **Difference between GET, POST, PUT, DELETE Method**

- **GET**:
    - **Purpose**: Retrieve data from the server.
    - **Use Case**: Fetching data for display (e.g., loading user profiles, articles).
    - **Characteristics**: Data is appended in the URL, visible in the address bar, and can be cached.
    
    ```jsx
    
    $.ajax({
      url: '/fetch-data',
      type: 'GET',
      success: function(data) {
        console.log(data);
      }
    });
    ```
    
- **POST**:
    - **Purpose**: Send data to the server to create a new resource.
    - **Use Case**: Submitting forms, sending user data to be stored in the database.
    - **Characteristics**: Data is sent in the request body, making it more secure for sensitive data.
    
    ```jsx
    
    $.ajax({
      url: '/submit-data',
      type: 'POST',
      data: { username: 'John' },
      success: function(response) {
        console.log('Data submitted');
      }
    });
    ```
    
- **PUT**:
    - **Purpose**: Update an existing resource on the server.
    - **Use Case**: Editing user profiles, updating content.
    - **Characteristics**: Data is sent in the request body. Used when updating a resource, and the full representation of the resource is sent.
    
    ```jsx
    $.ajax({
      url: '/update-data',
      type: 'PUT',
      data: { id: 1, name: 'John Doe' },
      success: function(response) {
        console.log('Data updated');
      }
    });
    ```
    
- **DELETE**:
    - **Purpose**: Remove a resource from the server.
    - **Use Case**: Deleting user accounts, removing items from a shopping cart.
    - **Characteristics**: Data is typically sent in the request body. Used when deleting a resource.
    
    ```jsx
    $.ajax({
      url: '/delete-data',
      type: 'DELETE',
      data: { id: 1 },
      success: function(response) {
        console.log('Data deleted');
      }
    });
    ```
    

### **Summary:**

- **GET** is for retrieving data.
- **POST** is for sending data to create resources.
- **PUT** is for updating existing resources.
- **DELETE** is for removing resources.

---

## **JSON Data**

JSON (JavaScript Object Notation) is a lightweight data-interchange format that is easy for humans to read and write, and easy for machines to parse and generate. It is often used for exchanging data between a client and a server.

### **Structure of JSON**:

```json
{
  "name": "John",
  "age": 30,
  "city": "New York"
}

```

### **Using JSON in Ajax:**

When sending or receiving data via AJAX, JSON is the preferred format.

**Sending JSON in AJAX:**

```jsx

$.ajax({
  url: '/api/data',
  type: 'POST',
  contentType: 'application/json',
  data: JSON.stringify({ name: 'John', age: 30 }),
  success: function(response) {
    console.log('JSON data sent successfully');
  },
  error: function() {
    console.log('Error sending JSON data');
  }
});
```

**Receiving JSON in AJAX:**

```jsx
$.ajax({
  url: '/api/get-data',
  type: 'GET',
  dataType: 'json',
  success: function(data) {
    console.log(data);  // Will log the parsed JSON object
  }
});
```

In this example, the `dataType: 'json'` option tells jQuery to automatically parse the JSON response from the server.

---

## **Serialization & De-serialization**

### **Serialization**:

Serialization is the process of converting a data object into a format that can be easily transferred over a network (e.g., converting a JavaScript object into a JSON string).

### **Example: Serialization in AJAX:**

```jsx
var userData = {
  name: 'John',
  age: 30
};

// Serialize the object to JSON string
var serializedData = JSON.stringify(userData);

// Sending the serialized data in AJAX
$.ajax({
  url: '/submit-data',
  type: 'POST',
  contentType: 'application/json',
  data: serializedData,
  success: function(response) {
    console.log('Data sent');
  }
});
```

### **De-serialization**:

De-serialization is the process of converting a serialized format (like a JSON string) back into a data object that can be manipulated by the program.

### **Example: De-serialization in AJAX:**

```jsx
$.ajax({
  url: '/get-user-data',
  type: 'GET',
  dataType: 'json',
  success: function(response) {
    // De-serialize the JSON response into a JavaScript object
    var userData = response;
    console.log(userData.name);  // Accessing the deserialized data
  }
});
```