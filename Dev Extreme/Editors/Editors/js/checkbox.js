$(function () {
    // Initialize the dxCheckBox with various configurations
    let checkBox = $("#checkBoxContainer").dxCheckBox({
        // ✅ Accessibility and Keyboard Navigation  
        accessKey: 'a',          // Specifies a keyboard shortcut for accessing the checkbox  
        tabIndex: 0,             // Sets the tab order for keyboard navigation  

        // ✅ Appearance and Behavior  
        activeStateEnabled: false, // Disables the active state styling (no highlight when pressed)  
        disabled: false,           // Allows user interaction (checkbox is not disabled)  
        hoverStateEnabled: true,   // Enables hover effect when the checkbox is hovered over  
        focusStateEnabled: true,   // Enables focus effect when using keyboard to navigate  

        // ✅ Read-Only and RTL Support  
        readOnly: false,  // Allows user interaction (checkbox is not read-only)  
        rtlEnabled: true, // Enables right-to-left (RTL) text support  

        // ✅ Text and Display Settings  
        text: "Accept Terms and Agreements", // Checkbox label text displayed next to the checkbox  
        hint: "Accept",                      // Tooltip text displayed when hovering over the checkbox  
        iconSize: 25,                         // Sets the size of the checkbox icon  
        value: null,                          // Initially set to null (indeterminate state)

        // ✅ Validation Rules  
        isValid: true,  // Initially considered valid  
        validationRules: [
            {
                type: "required", // Specifies that this checkbox is required  
                message: "You must accept the terms before proceeding."
            }
        ],
        validationError: { message: "This field is required" }, // Message for validation error if not checked  
        validationErrors: [{ message: "You must agree to the terms." }], // Multiple validation errors  
        validationMessageMode: 'always', // Always display validation messages, even if the checkbox is valid  
        validationStatus: 'valid', // Initially marks the checkbox as valid  

        // ✅ Custom Attributes for Styling  
        elementAttr: {
            id: "checkbox-container", // Sets a custom ID for the checkbox container element  
            class: "checkbox-class",  // Adds a custom CSS class for styling the checkbox container  
            // title: "Click to toggle terms" // Adds a tooltip text when hovering over the checkbox
        },

        // ✅ Event Handlers  
        onValueChanged: function(e) {
            // e -> e.value, e.previousValue, e.component, e.element, e.event,
            // Event triggered when the checkbox value changes
            if (e.value) {
                DevExpress.ui.notify("The CheckBox is checked : "+e.value, "success", 500); // Notify when checked
            }
        },
        onContentReady: function(e) {
            console.log("CheckBox rendered:", e.component); // Logs when the CheckBox is fully rendered  
        },
        onDisposing: function(e) {
            console.log("CheckBox is being disposed:", e.component); // Logs when the CheckBox is removed  
        },
        onInitialized: function(e) {
            console.log("CheckBox initialized:", e.component); // Logs when the CheckBox is initialized  
        },
        onOptionChanged: function(e) {
            console.log(`Property "${e.name}" changed to`, e.value); // Logs when an option property changes  
        }
    }).dxCheckBox("instance");  // Retrieve the instance of the checkbox

    // ✅ Begin update process (pause UI updates temporarily)
    checkBox.beginUpdate();

    // Change the value and text of the checkbox dynamically
    checkBox.option("value", true);  // Set the checkbox as checked (true)
    checkBox.option("text", "Checked!");  // Change the label text

    // ✅ End update process (resume UI updates)
    checkBox.endUpdate();

    // ✅ Add keyboard event handler for the "enter" key
    checkBox.registerKeyHandler("enter", function() {
        console.log("Enter key pressed!"); // Logs when the enter key is pressed
    });

    // ✅ Subscribe to the valueChanged event (logs the new value when changed)
    checkBox.on("valueChanged", function(e) {
        console.log("New Value:", e.value); // Logs the new value of the checkbox
    });

    // ✅ Manually refresh UI to ensure it reflects the latest changes
    checkBox.repaint();

    $("#focusButton").click(function() {
        // ✅ Focus the checkbox programmatically
        checkBox.focus(); // Focuses the checkbox, highlighting it for user interaction
    });

    $("#blurButton").click(function() {
        // ✅ Blur the checkbox programmatically (removes focus)
        checkBox.blur(); // Removes focus from the checkbox, if it has focus
    });

    $("#disposeButton").click(function() {
        // ✅ Dispose of the checkbox (clean up resources)
        checkBox.dispose(); // Destroys the checkbox widget and cleans up associated resources
    });
    
});
