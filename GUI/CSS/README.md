# CSS

### Basics Of CSS

- CSS (Cascading Style Sheets) is a style sheet language used to describe the presentation of a document written in HTML or XML. It controls the layout, colors, fonts, and overall appearance of web pages.
- Enhances the visual presentation of web pages, separating content from design.

### There Are Three Ways To Include CSS:

- Inline CSS
    
    CSS rules applied directly to an HTML element using the `style` attribute.
    
    ```css
    <h1 style="color: red;">Hello, World!</h1>
    
    ```
    
    **Usage**: Quick changes; generally not recommended for maintaining large projects due to poor separation of concerns.
    
- Internal CSS
    
    CSS rules defined within the `<style>` tag in the `<head>` section of an HTML document.
    
    ```css
    <style>
      body {
        background-color: lightblue;
      }
      h1 {
        color: navy;
      }
    </style>
    ```
    
     **Usage**: Suitable for single-page websites or when styles are specific to one document.
    
- External CSS
    
    A separate CSS file linked to HTML documents.
    
    ```html
    <link rel="stylesheet" type="text/css" href="styles.css">
    ```
    
    **Advantages:**
    
    - Reusable across multiple HTML pages.
    - Easier maintenance; changes made in one file reflect on all linked pages.

### CSS Syntax

A CSS rule consists of a selector and a declaration block.

```css
selector {
  property: value;
}

```

- **Components**:
    - **Selector**: Targets HTML elements (e.g., `h1`, `.class-name`, `#id-name`).
    - **Declaration Block**: Contains one or more declarations enclosed in curly braces `{}`.

### CSS Selectors

**Types**

- **Universal Selector**:  * (select all elements)
- **Type Selector**: `element` (selects all instances of a specific element, e.g., `p`)
- **Class Selector**: `.classname` (selects elements with a specific class)
- **ID Selector**: `#idname` (selects a single element with a specific ID)
- **Attribute Selector**: `[attribute=value]` (selects elements with a specific attribute)
- **Pseudo-classes**: `:hover`, `:active` (selects elements based on their state)

### CSS Basic Properties

**Common Properties**

- **Color**: Sets the text color.
    
    ```css
    color: blue;
    ```
    
- **Background**: Sets the background color or image.
    
    ```css
    background-color: yellow;
    ```
    
- **Font Size**: Sets the size of the text.
    
    ```css
    font-size: 16px;
    ```
    
- **Margin**: Sets the outer space of an element.
    
    ```css
    margin: 10px;
    ```
    
- **Padding**: Sets the inner space of an element.
    
    ```css
    padding: 10px;
    ```