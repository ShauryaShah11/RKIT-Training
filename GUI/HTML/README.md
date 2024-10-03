# HTML

## What is HTML

**HTML (Hypertext Markup Language)** is the basic building block of the web. It’s a language used to create web pages and web applications. HTML is like the skeleton of a webpage.it defines the structure and tells your browser how to display the content, but it doesn't focus on making things look pretty (that’s the job of CSS).

Example of a simple HTML document:

```html
<!DOCTYPE html>
<html>
  <head>
    <title>My First Webpage</title>
  </head>
  <body>
    <h1>Welcome to My Webpage</h1>
    <p>This is my first webpage made with HTML!</p>
  </body>
</html>

```

### 2. **HTML Structure**

An HTML document is structured with specific elements. Each element has a **start tag**, **content**, and an **end tag**. Let’s look at the parts of a typical HTML document:

1. `<!DOCTYPE html>` – This tells the browser what version of HTML you're using. The latest version is HTML5.
2. `<html>` – The root element that wraps everything on your page.
3. `<head>` – This is where you put meta information, like the title of your page or links to stylesheets and scripts.
4. `<title>` – The title of your webpage that appears on the browser tab.
5. `<body>` – The content of your webpage, like text, images, and links, goes here.

### Basic Controls

- Form Elements
    - **Form Method**: Specifies how to send form data (GET or POST).
    - **Action**: The URL where the form data is sent.
    
- Input Types
    - **Input**: Used for various types of user input (text, password, email, etc.).
    
    Text Input :- A text input field allows users to enter a single line of text.
    
    ```html
    <input type="text" id="username" name="username" placeholder="Enter your username" required>
    ```
    
    Email input :- An email input field allows users to enter their email address. It provides basic validation to ensure that the entered text follows the email format.
    
    ```html
    <input type="email" name="email">
    ```
    
    Password Input :- A password input field allows users to enter a password, obscuring the text for security.
    
    ```html
    <input type="password" id="password" name="password" placeholder="Enter your password" required>
    ```
    
    URL Input :- This input type is for entering web addresses (URLs) and provides validation to ensure the correct format.
    
    ```html
    <input type="url" id="website" name="website" placeholder="https://example.com" required>
    ```
    
    Number Input :- A number input field allows users to enter numeric values. It can also include attributes for setting minimum and maximum values.
    
    ```html
    <input type="number" id="age" name="age" min="1" max="120" placeholder="Enter your age" required>
    ```
    
    **Attributes**:
    
    - `type`: Specifies the type of input (in this case, "text").
    - `id`: A unique identifier for the input element, used in association with the `<label>`.
    - `name`: The name of the input, used when submitting the form data.
    - `placeholder`: Provides a hint to the user about what to enter.
    - `required`: Indicates that the field must be filled out before submitting.
- Text Area
    
    A `<textarea>` is used for multi-line text input. It allows users to enter large amounts of text, such as comments, descriptions, or any other form of free text input. The size of the textarea can be adjusted using the `rows` and `cols` attributes, or through CSS.
    
    ```html
    <textarea id="comments" name="comments" rows="4" cols="50" placeholder="Enter your comments here..." required></textarea>
    ```
    
    Attributes
    
    - **id**: A unique identifier for the `<textarea>`, which can be used to associate it with a `<label>`.
    - **name**: The name of the textarea, used when submitting the form data.
    - **rows**: Specifies the visible number of lines in the textarea.
    - **cols**: Specifies the visible width of the textarea in characters.
    - **placeholder**: Provides a hint to the user about what to enter in the textarea.
    - **required**: Indicates that the textarea must be filled out before submitting the form.
    - **disabled**: If present, the textarea cannot be edited or submitted.
    - **readonly**: Makes the textarea read-only, meaning users can see the text but cannot modify it.
    - **maxlength**: Sets the maximum number of characters allowed in the textarea.
- SelectBox
    
    A `<select>` element is used to create a dropdown list from which users can select one or multiple options
    
    ```html
    <label for="car-select">Choose a car:</label>
    <select id="car-select" name="car">
      <option value="bmw">BMW</option>
      <option value="mercedes">Mercedes</option>
      <option value="audi">Audi</option>
      <option value="toyota">Toyota</option>
    </select>
    ```
    
    ### Attributes
    
    - **id**: A unique identifier for the `<select>` element, which can be used to associate it with a `<label>`.
    - **name**: The name of the select box, used when submitting the form data.
    - **multiple**: If present, allows the selection of multiple options at once.
    - **size**: Specifies the number of visible options in the dropdown. If this attribute is set, the dropdown behaves like a list box.
    - **required**: Indicates that the selection must be made before submitting the form.
- CheckBox
    
    A checkbox allows users to select one or more options from a set. It is commonly used in forms to enable multiple selections, giving users the flexibility to choose several items simultaneously.
    
    ```html
    <label>
      <input type="checkbox" name="hobby" value="reading"> Reading
    </label>
    <label>
      <input type="checkbox" name="hobby" value="writing"> Writing
    </label>
    <label>
      <input type="checkbox" name="hobby" value="coding" checked> Coding
    </label>
    
    ```
    
    ### Attributes
    
    - **type**: Must be set to "checkbox" to create a checkbox.
    - **name**: The name of the checkbox group. When the form is submitted, this name is used as the key in the submitted data.
    - **value**: The value associated with the checkbox that will be sent to the server if the checkbox is checked.
    - **checked**: If present, this attribute pre-selects the checkbox when the page loads.
    - **disabled**: If present, the checkbox is disabled and cannot be checked or unchecked.
    - **id**: A unique identifier for the checkbox, which can be used to associate it with a `<label>`.
- Radio Button
    
    A radio button allows users to select one option from a predefined set of options. Unlike checkboxes, radio buttons are mutually exclusive, meaning only one radio button in a group can be selected at a time.
    
    ```html
    <label>
      <input type="radio" name="gender" value="male"> Male
    </label>
    <label>
      <input type="radio" name="gender" value="female"> Female
    </label>
    <label>
      <input type="radio" name="gender" value="other" checked> Other
    </label>
    
    ```
    
    ### Attributes
    
    - **type**: Must be set to "radio" to create a radio button.
    - **name**: The name of the radio button group. All radio buttons with the same `name` attribute belong to the same group and allow only one selection.
    - **value**: The value associated with the selected radio button that will be sent to the server when the form is submitted.
    - **checked**: If present, this attribute pre-selects the radio button when the page loads.
    - **disabled**: If present, the radio button is disabled and cannot be selected.
    - **id**: A unique identifier for the radio button, which can be used to associate it with a `<label>`.
- Button
    
    A button in HTML can be used to trigger actions on the web page, such as submitting forms.
    
    ```html
    <button type="button">Click Me</button>
    <input type="button" value="Click Me">
    
    ```
    
    ### Attributes
    
    - **type**: Specifies the button type. Possible values are:
        - `button`: Triggers an action (not submitting a form).
        - `submit`: Submits the form.
        - `reset`: Resets the form fields.
    - **name**: The name of the button, which is sent to the server with the form data.
    - **value**: The text that appears on the button (for `<input>` buttons) or the content inside the button (for `<button>`).
    - **id**: A unique identifier for the button.
    - **class**: Classes for applying CSS styles.
    - **onclick**: JavaScript function to be executed when the button is clicked.
- File Control
    
    A file control allows users to upload files from their device to a web server. This is commonly used in forms where users need to submit documents, images, or other files.
    
    ```html
    <form action="/upload" method="post" enctype="multipart/form-data">
      <label for="fileUpload">Upload a file:</label>
      <input type="file" id="fileUpload" name="fileUpload">
      <button type="submit">Submit</button>
    </form>
    
    ```
    
    Uploading Multiple Files
    
    ```html
    <form action="/upload" method="post" enctype="multipart/form-data">
      <label for="fileUpload">Upload files:</label>
      <input type="file" id="fileUpload" name="files[]" multiple>
      <button type="submit">Submit</button>
    </form>
    
    ```
    
    Accepting Specific File Types
    
    ```html
    <form action="/upload" method="post" enctype="multipart/form-data">
      <label for="imageUpload">Upload an image (JPEG, PNG):</label>
      <input type="file" id="imageUpload" name="imageUpload" accept=".jpg, .jpeg, .png">
      <button type="submit">Submit</button>
    </form>
    
    ```
    
    ### Attributes
    
    - **type**: Must be set to "file" to create a file input control.
    - **name**: The name of the file input, used to identify the field in form submissions.
    - **id**: A unique identifier for the file input, which can be used for labeling and styling.
    - **accept**: Specifies the types of files that the server accepts (e.g., image formats, PDF). This can restrict user file selection.
    - **multiple**: Allows users to select multiple files to upload at once.
    - **required**: Makes the file input mandatory for form submission.
    - **disabled**: Disables the file input, preventing user interaction.

### Control’s Attribute

1. Name
The `name` attribute specifies the name of an input element. It is used to reference form data after a form is submitted.
    
    ```html
    <input type="text" name="username">
    ```
    
    ### Usage
    
    - **Form Submission**: The value of the `name` attribute becomes the key in the submitted form data.
    - **Grouping Inputs**: Elements with the same `name` can be grouped, especially for radio buttons and checkboxes.
    
2. Id 
The `id` attribute provides a unique identifier for an HTML element. It must be unique within the page.

    
    ```html
    <input type="text" id="username">
    
    ```
    
    ### Usage
    
    - **JavaScript and CSS Targeting**: Used to select elements for styling or manipulation.
    - **Label Association**: Can be associated with `<label>` elements for accessibility.

1. Value
The `value` attribute specifies the initial value of an input element. It represents the value that will be submitted with the form.
    
    ```html
    <input type="text" value="John Doe">
    
    ```
    
    ### Usage
    
    - **Default Values**: Sets a default value for text inputs, checkboxes, and radio buttons.
    - **Dynamic Changes**: Can be updated dynamically using JavaScript.
    
2. Class
The `class` attribute is used to assign one or more class names to an element. These class names can be used for styling with CSS or for targeting elements with JavaScript.
    
    ```html
    <input type="text" class="form-control">
    
    ```
    
    ### Usage
    
    - **Styling**: Allows multiple elements to share the same style rules defined in CSS.
    - **JavaScript Selection**: Can be used to select multiple elements with the same class for manipulation.

### Basic Tags With Attribute

- Image:
    
    The `<img>` tag is used to embed images in an HTML document. It is a self-closing tag and does not require a closing tag.
    
    ```html
    <img src="image.jpg" alt="Description of image" width="500" height="300">
    
    ```
    
    ### Attributes
    
    - **src**: Specifies the path to the image file.
    - **alt**: Provides alternative text for the image if it cannot be displayed. This is important for accessibility.
    - **width** and **height**: Define the dimensions of the image in pixels.
    
- Anchor Tag
    
    The `<a>` tag defines a hyperlink that links to another webpage, email address, or file. It is an inline element.
    
    ```html
    <a href="https://www.example.com" target="_blank" title="Visit Example">Click here to visit Example</a>
    
    ```
    
    ### Attributes
    
    - **href**: Specifies the URL or target of the link.
    - **target**: Specifies where to open the linked document (e.g., `_blank` for a new tab).
    - **title**: Provides additional information about the link when hovered over.
    
- Meta Tag
    
    Meta tags are HTML tags that provide metadata about a webpage. This information is not displayed on the page itself but is used by browsers and search engines.
    
    ```html
    <head>
      <meta charset="UTF-8">
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <meta name="description" content="This is an example webpage.">
      <meta name="keywords" content="HTML, CSS, JavaScript">
      <meta name="author" content="John Doe">
    </head>
    
    ```
    
    ### Usage
    
    - **SEO**: Helps search engines understand the content of the page.
    - **Viewport Settings**: Controls the layout on mobile browsers.
    - **Character Set**: Specifies the character encoding for the HTML document.

### Resposive Websites

A responsive website is designed to adapt its layout and content based on the screen size and orientation of the device being used to view it. This approach ensures that users have a good experience regardless of whether they are on a desktop, tablet, or smartphone.

Ways to achieve Responsive Website

- **Use Flexible Layouts**: Employ CSS Flexbox or Grid to create fluid layouts that adapt to different screen sizes.
- **Media Queries**: Utilize CSS media queries to apply different styles based on device characteristics (width, height, orientation).
    
    ```css
    @media (max-width: 600px) {
      body {
        background-color: lightblue;
      }
    }
    ```
    
- **Fluid Images and Videos**: Ensure that images and videos are responsive by using relative units like percentages.
    
    ```css
    img {
      max-width: 100%;
      height: auto;
    }
    
    ```
    
- **Mobile-First Approach**: Start designing for smaller screens first, then enhance for larger screens using media queries.