# Bootstarp

### What is Bootstarp

Bootstrap is a popular open-source front-end framework used to develop responsive, mobile-first websites. It includes HTML, CSS, and JavaScript-based design templates for typography, forms, buttons, navigation, and other interface components. Bootstrap simplifies the process of creating responsive and visually appealing websites.

### Structure of Bootstrap

**Grid System:** Bootstrap uses a 12-column fluid grid system that scales up to 12 columns depending on the screen size. The grid system makes it easier to create complex layouts using rows and columns.

```jsx
<div class="container">
  <div class="row">
    <div class="col-sm-6">Column 1</div>
    <div class="col-sm-6">Column 2</div>
  </div>
</div>

```

**Components:** Bootstrap provides ready-to-use components such as buttons, cards, modals, dropdowns, forms, and navigation bars.

```jsx
<button class="btn btn-primary">Primary Button</button>
```

### Examples

- navbar
    
    ```html
    	<nav class="navbar navbar-expand-lg navbar-light bg-light">
    	      <div class="container-fluid">
    	        <a class="navbar-brand ms-3" href="./index.html">My Site</a>
    	
    	        <div class="ms-auto">
    	          <a class="btn btn-primary me-3" href="./register.html">Signup</a>
    	          <a class="btn btn-outline-secondary" href="./login.html">Login</a>
    	        </div>
    	      </div>
    	</nav>
    ```
    
    - **`<nav class="navbar navbar-expand-lg navbar-light bg-light">`**
        - **`<nav>`**: This is the HTML5 element used for defining a navigation section.
        - **`navbar`**: A Bootstrap class to style the `<nav>` element as a navigation bar.
        - **`navbar-expand-lg`**: A Bootstrap class that allows the navbar to be collapsible on smaller screen sizes (medium and smaller). It will expand when viewed on large screens (`lg`).
        - **`navbar-light`**: A Bootstrap class that applies a light color scheme for text and links inside the navbar.
        - **`bg-light`**: A Bootstrap class that sets the background color of the navbar to light grey.
    - **`<div class="container-fluid">`**
        - **`container-fluid`**: A Bootstrap class that makes the container stretch to 100% of the viewport width. It ensures the elements inside are responsive.
    - **`<a class="navbar-brand ms-3" href="./index.html">My Site</a>`**
        - **`<a>`**: This is an anchor (`<a>`) element that creates a link to the **index.html** page.
        - **`navbar-brand`**: A Bootstrap class used to define branding elements, like a logo or the name of the site. It will be styled appropriately by Bootstrap.
        - **`ms-3`**: A Bootstrap margin utility class that adds a left margin of 1 rem (`3` units) to space the brand from the left edge of the navbar.
        - **`href="./index.html"`**: The `href` attribute points to the homepage, meaning clicking the "My Site" text will take the user to the **index.html** page.
    - **`<div class="ms-auto">`**
        - **`ms-auto`**: A Bootstrap class that applies "auto" left margin, pushing this div (and its contents) to the right side of the navbar.
    - **`<a class="btn btn-primary me-3" href="./register.html">Signup</a>`**
        - **`btn btn-primary`**: Bootstrap classes to style the `<a>` tag as a primary button (blue background with white text by default).
        - **`me-3`**: A Bootstrap margin utility class that adds right margin (3 units) to the button, creating space between the "Signup" button and the "Login" button.
        - **`href="./register.html"`**: Clicking this button will take the user to the **register.html** page.
    - **`<a class="btn btn-outline-secondary" href="./login.html">Login</a>`**
        - **`btn btn-outline-secondary`**: Bootstrap classes to style the `<a>` tag as a button with an outlined border and secondary color (gray).
        - **`href="./login.html"`**: Clicking this button will take the user to the **login.html** page.
- footer
    
    ```html
    <footer class="bg-light text-center text-lg-start">
          <div class="container p-3">
            <div class="row">
              <div class="col-md-6 text-md-start">
                <p class="mb-0">&copy; 2024 My Site. All rights reserved.</p>
              </div>
              <div class="col-md-6 text-md-end">
                <a href="#" class="text-decoration-none me-3">Privacy Policy</a>
                <a href="#" class="text-decoration-none">Terms of Service</a>
              </div>
            </div>
          </div>
     </footer>
    ```
    
    - **`<footer class="bg-light text-center text-lg-start">`**
        - **`<footer>`**: The HTML5 element used to define the footer section of the web page.
        - **`bg-light`**: A Bootstrap class that applies a light background color (typically light grey).
        - **`text-center`**: A Bootstrap class that centers the text within the footer for small screens.
        - **`text-lg-start`**: A Bootstrap class that aligns the text to the left on larger screens (`lg` and above). This ensures the content adapts responsively.
    - **`<div class="container p-3">`**
        - **`container`**: A Bootstrap class that ensures the content inside is responsive and fits well within the page layout, adding appropriate padding and margin.
        - **`p-3`**: A Bootstrap padding utility class that adds padding of `1rem` to all sides of the container.
    - **`<div class="row">`**
        - **`row`**: A Bootstrap grid class that creates a row in the grid system. This helps in laying out the footer elements in a structured manner across multiple columns.
    - **`<div class="col-md-6 text-md-start">`**
        - **`col-md-6`**: A Bootstrap grid class that divides the row into two equal columns, each taking up 6 columns (out of 12) on medium (`md`) screens and larger.
        - **`text-md-start`**: A Bootstrap class that aligns the text to the left on medium-sized screens and above.
        - **`<p class="mb-0">&copy; 2024 My Site. All rights reserved.</p>`**:
            - **`mb-0`**: A Bootstrap margin utility class that sets the bottom margin to `0`, ensuring no extra space below the text.
            - **`&copy; 2024 My Site`**: Displays the copyright symbol (`©`) followed by the year and the website name.
    - **`<div class="col-md-6 text-md-end">`**
        - **`col-md-6`**: This column takes up the second half of the footer on medium screens and larger.
        - **`text-md-end`**: A Bootstrap class that aligns the text to the right for medium-sized screens and above. This ensures that the "Privacy Policy" and "Terms of Service" links are aligned to the right side on larger screens.
    - **`<a href="#" class="text-decoration-none me-3">Privacy Policy</a>`**
        - **`<a>`**: An anchor (`<a>`) tag that creates a hyperlink. The `href="#"` attribute is a placeholder and can be replaced with the actual link to the privacy policy page.
        - **`text-decoration-none`**: A Bootstrap class that removes the default underline from the hyperlink, giving it a cleaner look.
        - **`me-3`**: A Bootstrap margin utility class that adds a right margin (3 units) to create space between the "Privacy Policy" and "Terms of Service" links.
    - **`<a href="#" class="text-decoration-none">Terms of Service</a>`**
        - Another hyperlink that points to the terms of service page. The `text-decoration-none` class ensures that there is no underline on this link either.