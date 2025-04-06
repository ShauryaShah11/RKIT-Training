# Dialogues & Notification

# Dialogues & Notification

## Table of Contents

1. Overview
2. LoadIndicator
3. Load Panel
4. Popup
5. Popover
6. Toast
7. Best Practices

---

### **1. Overview**

- DevExtreme provides various UI components for **dialogs and notifications**, including **Load Indicator, Load Panel, Popup, Popover, and Toast**.
- These components help create **intuitive user interactions** for loading states, modals, contextual information, and system notifications.
- Supports **custom styling, animation, positioning, and event handling**.
- All dialog components are **responsive** and work across desktop and mobile devices.

---

### **2. Load Indicator**

- **Description:**
    - Simple animated spinner that indicates a background process is running.
    - Lightweight component that can be embedded directly within UI elements.
- **Properties:**
    - `indicatorSrc` → Custom image for the loading indicator.
    - `visible` → Controls visibility (`true/false`).
    - `size` → Sets the indicator size (number or CSS size value).
    - `height`/`width` → Sets specific dimensions of the indicator.
- **Methods:**
    - `.show()` → Displays the load indicator.
    - `.hide()` → Hides the load indicator.
    - `.option(name, value)` → Updates component options dynamically.
- **Example:**
    
    ```jsx
    $("#loadIndicator").dxLoadIndicator({
      visible: true,
      height: 40,
      width: 40
    });
    
    // Show/hide programmatically
    var indicator = $("#loadIndicator").dxLoadIndicator("instance");
    indicator.show();
    // Later:
    indicator.hide();
    
    ```
    

---

### **3. Load Panel**

- **Description:**
    - Enhanced version of Load Indicator with message text and shading.
    - Blocks user interaction with the page during lengthy operations.
- **Properties:**
    - `message` → Displays a loading message.
    - `showIndicator` → Shows/hides the loading indicator.
    - `showPane` → Shows/hides the background pane.
    - `shading` → Enables background shading.
    - `shadingColor` → Sets the color of the shading.
    - `position` → Sets the panel position relative to a target element.
- **Methods:**
    - `.show()` → Displays the load panel.
    - `.hide()` → Hides the load panel.
    - `.option(name, value)` → Modifies panel properties at runtime.
- **Events:**
    - `onShowing` → Triggers before the panel appears.
    - `onShown` → Triggers after the panel appears.
    - `onHiding` → Triggers before the panel hides.
    - `onHidden` → Triggers after the panel hides.
- **Example:**
    
    ```jsx
    $("#loadPanel").dxLoadPanel({
      message: "Loading data...",
      shading: true,
      shadingColor: "rgba(0, 0, 0, 0.4)",
      showPane: true,
      visible: false,
      position: { of: "#targetElement" }
    });
    
    // Show when needed
    $("#loadPanel").dxLoadPanel("instance").show();
    
    // Hide after operation completes
    setTimeout(function() {
      $("#loadPanel").dxLoadPanel("instance").hide();
    }, 2000);
    
    ```
    

---

### **4. Popup**

- **Description:**
    - Modal or non-modal window that displays content above the page.
    - Supports complex layouts, custom templates, and interaction events.
- **Properties:**
    - `title` → Sets the popup title.
    - `showTitle` → Shows/hides the title bar.
    - `showCloseButton` → Shows/hides the close button.
    - `visible` → Controls visibility (`true/false`).
    - `width`/`height` → Sets dimensions.
    - `maxWidth`/`maxHeight` → Sets maximum dimensions.
    - `fullScreen` → Expands popup to full screen.
    - `contentTemplate` → Template for popup content.
    - `dragEnabled` → Allows dragging the popup.
    - `resizeEnabled` → Allows resizing the popup.
    - `closeOnOutsideClick` → Closes when clicking outside.
    - `shading` → Enables background shading.
- **Methods:**
    - `.show()` → Opens the popup.
    - `.hide()` → Closes the popup.
    - `.toggle()` → Toggles visibility.
    - `.resize()` → Recalculates popup size.
- **Events:**
    - `onShowing` → Fires before the popup appears.
    - `onShown` → Fires after the popup is displayed.
    - `onHiding` → Fires before the popup closes.
    - `onHidden` → Fires after the popup is closed.
    - `onResizeStart` → Fires when resizing begins.
    - `onResize` → Fires during resizing.
    - `onResizeEnd` → Fires when resizing ends.
- **Example:**
    
    ```jsx
    $("#popup").dxPopup({
      title: "User Information",
      width: 400,
      height: 300,
      visible: false,
      dragEnabled: true,
      closeOnOutsideClick: true,
      contentTemplate: function() {
        return $("<div>").append(
          $("<p>").text("User details go here")
        );
      },
      onShown: function() {
        console.log("Popup displayed");
      }
    });
    
    // Show the popup
    $("#showButton").on("click", function() {
      $("#popup").dxPopup("instance").show();
    });
    
    ```
    

---

### **5. Popover**

- **Description:**
    - Contextual information container attached to a specific UI element.
    - Appears on hover, click, or programmatically to provide additional information.
- **Properties:**
    - `target` → Specifies the element the popover is attached to.
    - `position` → Sets the display position (`"top"`, `"bottom"`, `"left"`, `"right"`).
    - `visible` → Controls visibility (`true/false`).
    - `width`/`height` → Sets dimensions.
    - `contentTemplate` → Template for popover content.
    - `showEvent` → Event that triggers the popover display.
    - `hideEvent` → Event that triggers the popover hiding.
    - `hideOnOutsideClick` → Hides when clicking outside.
    - `animation` → Customizes show/hide animations.
- **Methods:**
    - `.show()` → Displays the popover.
    - `.hide()` → Hides the popover.
    - `.toggle()` → Toggles visibility.
    - `.option(name, value)` → Updates properties.
- **Events:**
    - `onShowing` → Fires before showing.
    - `onShown` → Fires after showing.
    - `onHiding` → Fires before hiding.
    - `onHidden` → Fires after hiding.
    - `onPositioning` → Fires when positioning.
- **Example:**
    
    ```jsx
    $("#popover").dxPopover({
      target: "#infoButton",
      showEvent: "mouseenter",
      hideEvent: "mouseleave",
      position: "top",
      width: 250,
      contentTemplate: function() {
        return $("<div>").append(
          $("<h4>").text("Help Information"),
          $("<p>").text("This explains how to use this feature.")
        );
      }
    });
    
    // Show programmatically if needed
    $("#showInfoButton").on("click", function() {
      $("#popover").dxPopover("instance").show();
    });
    
    ```
    

---

### **6. Toast**

- **Description:**
    - Brief, auto-hiding notification that provides feedback about operations.
    - Appears temporarily without requiring user interaction to dismiss.
- **Properties:**
    - `message` → Sets the text message.
    - `type` → Defines the notification type (`"info"`, `"success"`, `"error"`, `"warning"`, `"custom"`).
    - `displayTime` → Duration before auto-hide (in milliseconds).
    - `position` → Toast position on screen.
    - `animation` → Custom showing/hiding animations.
    - `width`/`height` → Sets dimensions.
    - `visible` → Controls visibility (`true/false`).
- **Methods:**
    - `.show()` → Displays the toast.
    - `.hide()` → Hides the toast.
- **Events:**
    - `onShowing` → Fires before showing.
    - `onShown` → Fires after showing.
    - `onHiding` → Fires before hiding.
    - `onHidden` → Fires after hiding.
- **Example:**
    
    ```jsx
    // Component instance method
    $("#toast").dxToast({
      message: "Operation Successful",
      type: "success",
      displayTime: 3000,
      position: {
        my: "center top",
        at: "center top",
        offset: "0 40"
      }
    }).dxToast("instance").show();
    
    // Quick notification method
    $("#saveButton").on("click", function() {
      DevExpress.ui.notify("Data saved successfully", "success", 3000);
    });
    
    ```
    

---

### **7. Best Practices**

- **General Guidelines:**
    - Use **Load Indicator** for small, inline loading states.
    - Use **Load Panel** for blocking operations that take significant time.
    - Use **Popup** for complex interactions requiring user input.
    - Use **Popover** for contextual help or additional information.
    - Use **Toast** for non-intrusive feedback about completed operations.
- **Performance Tips:**
    - Create components at startup and show/hide as needed rather than recreating.
    - Use templates for complex content to improve rendering performance.
    - Avoid overusing modal popups as they interrupt user workflow.
- **Accessibility Considerations:**
    - Ensure proper contrast for text in all dialog components.
    - Add appropriate ARIA attributes when implementing custom templates.
    - Consider keyboard navigation for modal dialogs.
    - Set meaningful timeout duration for toast messages.