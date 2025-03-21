$(function () {
    // Initialize the dxCheckBox with various configurations
    let checkBox = $("#checkBoxContainer").dxCheckBox({
        // ✅ Accessibility and Keyboard Navigation  
        accessKey: 'a',   // Specifies a keyboard shortcut for accessing the checkbox (Default: undefined)
        tabIndex: 0,      // Sets the tab order for keyboard navigation (Default: 0)

        // ✅ Appearance and Behavior  
        activeStateEnabled: false, // Enables/disables active state styling (Default: true)
        // Possible values: true, false
        disabled: false,           // Disables the checkbox, making it non-interactive (Default: false)
        // Possible values: true, false
        hoverStateEnabled: true,   // Enables hover effect when the checkbox is hovered over (Default: true)
        // Possible values: true, false
        focusStateEnabled: true,   // Enables focus effect when using keyboard to navigate (Default: true)
        // Possible values: true, false

        // ✅ Read-Only and RTL Support  
        readOnly: false,  // Prevents user interaction, making the checkbox read-only (Default: false)
        // Possible values: true, false
        rtlEnabled: true, // Enables right-to-left (RTL) text support (Default: false)
        // Possible values: true, false

        // ✅ Text and Display Settings  
        text: "Accept Terms and Agreements", // Checkbox label text displayed next to the checkbox (Default: "")
        hint: "Accept",                      // Tooltip text displayed when hovering over the checkbox (Default: undefined)
        iconSize: 25,                         // Sets the size of the checkbox icon (Default: undefined)
        value: null,                          // Checkbox value (Default: false)
        // Possible values: true, false, null (null means indeterminate state)

        // ✅ Validation Rules  
        isValid: true,  // Determines whether the checkbox passes validation (Default: true)
        // Possible values: true, false
        validationRules: [
            {
                type: "required", // Specifies that this checkbox is required  
                message: "You must accept the terms before proceeding."
            }
        ],  // Validation rules (Default: [])
        validationError: { message: "This field is required" }, // Validation error message (Default: null)
        validationErrors: [{ message: "You must agree to the terms." }], // Multiple validation errors (Default: null)
        validationMessageMode: 'always', // Defines when validation messages are shown (Default: "auto")
        // Possible values: "auto", "always"
        validationStatus: 'valid', // Indicates validation status (Default: "valid")
        // Possible values: "valid", "invalid", "pending"

        // ✅ Custom Attributes for Styling  
        elementAttr: {
            id: "checkbox-container", // Sets a custom ID for the checkbox container element  
            class: "checkbox-class",  // Adds a custom CSS class for styling the checkbox container  
            // title: "Click to toggle terms" // Adds a tooltip text when hovering over the checkbox
        }, // Adds custom HTML attributes to the checkbox container (Default: {})

        // ✅ Event Handlers  
        onValueChanged: function (e) {
            // Event triggered when the checkbox value changes
            // e -> e.value, e.previousValue, e.component, e.element, e.event
            if (e.value) {
                DevExpress.ui.notify("The CheckBox is checked : " + e.value, "success", 500); // Notify when checked
            }
        },
        onContentReady: function (e) {
            console.log("CheckBox rendered:", e.component); // Logs when the CheckBox is fully rendered  
        },
        onDisposing: function (e) {
            console.log("CheckBox is being disposed:", e.component); // Logs when the CheckBox is removed  
        },
        onInitialized: function (e) {
            console.log("CheckBox initialized:", e.component); // Logs when the CheckBox is initialized  
        },
        onOptionChanged: function (e) {
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
    checkBox.registerKeyHandler("enter", function () {
        console.log("Enter key pressed!"); // Logs when the enter key is pressed
    });

    // ✅ Subscribe to the valueChanged event (logs the new value when changed)
    checkBox.on("valueChanged", function (e) {
        console.log("New Value:", e.value); // Logs the new value of the checkbox
    });

    // ✅ Manually refresh UI to ensure it reflects the latest changes
    checkBox.repaint();

    $("#focusButton").click(function () {
        // ✅ Focus the checkbox programmatically
        checkBox.focus(); // Focuses the checkbox, highlighting it for user interaction
    });

    $("#blurButton").click(function () {
        // ✅ Blur the checkbox programmatically (removes focus)
        checkBox.blur(); // Removes focus from the checkbox, if it has focus
    });

    $("#disposeButton").click(function () {
        // ✅ Dispose of the checkbox (clean up resources)
        checkBox.dispose(); // Destroys the checkbox widget and cleans up associated resources
    });
});
