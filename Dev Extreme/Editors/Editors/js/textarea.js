$(function () {
    // Initialize DevExtreme TextArea widget with various configuration options
    let textAreaInstance = $("#textAreaContainer").dxTextArea({
        accessKey: 'x',                        // Shortcut key for focusing the TextArea (Alt + x)
        activeStateEnabled: true,              // Enables active visual state when the TextArea is pressed
        disabled: false,                       // Enables the TextArea (false means it’s not disabled)
        elementAttr: {                         // Custom HTML attributes for the container element
            id: "elementId",
            class: "class-name"
        },
        focusStateEnabled: true,               // Adds visual feedback when the TextArea is focused
        height: function () {                   // Dynamically set the TextArea height as 1/1.5 of the window height
            return window.innerHeight / 1.5;
        },
        hint: "Enter a text",                  // Tooltip displayed when the user hovers over the TextArea
        hoverStateEnabled: true,               // Adds hover effect when the user moves the mouse over the TextArea
        inputAttr: {                           // Custom attributes for the internal input element
            id: "inputId"
        },
        isValid: true,                         // Sets the initial validation state of the TextArea to valid
        name: "address",                       // Name attribute for the form submission
        rtlEnabled: false,                     // Disables right-to-left text direction
        stylingMode: 'outlined',               // Specifies the TextArea styling mode: 'outlined', 'underlined', or 'filled'
        tabIndex: 0,                           // Tab order for focusing the TextArea using the Tab key
        value: "Ahmedabad",                    // Initial text value of the TextArea
        placeholder: "Enter an address",       // Placeholder text displayed when the TextArea is empty
        readOnly: false,                       // Makes the TextArea editable
        spellCheck: true,                      // Enables the browser’s spell-check feature
        autoResizeEnabled: true,               // Automatically resizes the TextArea based on content
        minHeight: 100,                        // Minimum height of the TextArea
        maxHeight: 200,                        // Maximum height of the TextArea
        maxLength: 200,                        // Maximum number of characters allowed in the TextArea

        // Event handlers for user interactions
        onValueChanged: function (e) {         // Triggered when the TextArea value changes
            const previousValue = e.previousValue;
            const newValue = e.value;
            console.log("Previous Value: " + previousValue + "\nNew Value: " + newValue);
        },
        onKeyUp: function (e) {                // Triggered when a key is released in the TextArea
            const keyCode = e.event.key;
            console.log("Key Released: " + keyCode);
        },
        onKeyDown: function (e) {
            console.log("Key down event");
        },
        onEnterKey: function (e) {             // Triggered when the Enter key is pressed in the TextArea
            console.log("Enter key pressed.");
        },
        onChange: function (e) {                // Triggered when the TextArea loses focus and its value changes
            console.log("TextArea value changed:", e);
        },
        onContentReady: function (e) {          // Triggered when the TextArea's content is fully loaded
            console.log("TextArea content is ready.");
        },
        onDisposing: function (e) {             // Triggered when the TextArea instance is disposed
            alert("Disposing TextArea Component.");
        }
    }).dxTextArea("instance");                 // Get the TextArea instance for further actions

    // Custom event handlers for keyDown events
    const keyDownHandler1 = function (e) {     // First handler for the keyDown event
        const keyCode = e.event.key;
        console.log("KeyDown Handler 1: " + keyCode);
    };

    const keyDownHandler2 = function (e) {     // Second handler for the keyDown event
        const keyCode = e.event.key;
        console.log("KeyDown Handler 2: " + keyCode);
    };

    // Attach both keyDown handlers to the TextArea instance
    textAreaInstance
        .on("keyDown", keyDownHandler1)
        .on("keyDown", keyDownHandler2);

    // Button click events to perform specific actions on the TextArea
    $("#blur").click(function () {              // Removes focus from the TextArea
        textAreaInstance.blur();
        console.log("TextArea blurred.");
    });

    $("#focus").click(function () {             // Focuses the TextArea
        textAreaInstance.focus();
        console.log("TextArea focused.");
    });

    $("#reset").click(function () {             // Resets the TextArea to its default state
        textAreaInstance.reset();
        console.log("TextArea reset.");
    });

    $("#dispose").click(function () {           // Disposes of the TextArea instance
        textAreaInstance.dispose();
        console.log("TextArea disposed.");
    });

    $("#repaint").click(function () {           // Repaints the TextArea, updating its appearance
        textAreaInstance.repaint();
        console.log("TextArea repainted.");
    });

    // Example of using beginUpdate and endUpdate for batching updates
    textAreaInstance.beginUpdate();            // Starts a batch update (useful for multiple changes)
    // Add multiple changes here if needed
    textAreaInstance.endUpdate();              // Ends the batch update

    // Example of how to use registerKeyHandler to handle custom keys
    textAreaInstance.registerKeyHandler("space", function (e) {
        console.log("Space key pressed.");      // Handles the Space key press event
    });

    // Detach all event handlers for the TextArea
    textAreaInstance.off("keyDown");

    // Example of attaching a jQuery keyup event to the TextArea container
    $("#textAreaContainer").keyup(function (e) {
        alert("Hello from jQuery keyup event.");
    });

    // // Update the value
    // textAreaInstance.option("value", "New Value");

    // // Enable or disable the TextArea
    // textAreaInstance.option("disabled", true);  // Disable
    // textAreaInstance.option("disabled", false); // Enable

    // // Update the placeholder
    // textAreaInstance.option("placeholder", "New Placeholder");

    // // Update the height
    // textAreaInstance.option("height", 300);

    // // Update the styling mode
    // textAreaInstance.option("stylingMode", "underlined");
});