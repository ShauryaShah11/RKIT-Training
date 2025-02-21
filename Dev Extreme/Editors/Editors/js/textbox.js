$(function () {
    // Initialize a simple text box with a default value
    $("#textBoxContainer1").dxTextBox({
        value: "The value that should be edited", // Initial value
        readonly: false                           // Editable state
    });

    // Initialize a password editor with a toggle button to show/hide the password
    let passwordEditor = $("#textBoxContainer2").dxTextBox({
        value: null,
        placeholder: "Enter a password",          // Placeholder text
        mode: 'password',                         // Input type as password
        buttons: [{
            name: 'password',
            location: 'after',                    // Position the button after the input
            options: {
                icon: 'fa fa-eye',                // Icon for the button
                stylingMode: 'text',
                onClick: function () {
                    // Toggle between 'password' and 'text' modes
                    let currentMode = passwordEditor.option('mode');
                    passwordEditor.option('mode', currentMode === 'text' ? 'password' : 'text');
                }
            }
        }]
    }).dxTextBox("instance");

    // Initialize a masked text box with custom rules
    $("#textBoxContainer3").dxTextBox({
        placeholder: 'enter a text',              // Placeholder text
        mask: "SFFFFHN",                          // Mask pattern
        maskRules: {
            // Custom mask rules for different characters
            S: '$',                               // Single character
            H: /[0-9A-F]/,                        // Regular expression
            N: ['$', '%', '&', '@'],              // Array of characters
            F: function (char) {                  // Function to allow uppercase characters
                return char == char.toUpperCase();
            }
        },
    });

    // Initialize a phone number text box with a mask and custom error message
    $("#textBoxContainer4").dxTextBox({
        mask: "+1 (200) 000-0000",                // Phone number format
        maskChar: "‒",                            // Character for masking
        maskInvalidMessage: "The input value does not match the mask" // Error message
    });

    // Get masked and unmasked values of the third text box
    const maskedValue = $("#textBoxContainer3").dxTextBox("option", "text");
    const unmaskedValue = $("#textBoxContainer3").dxTextBox("option", "value");

    // Initialize a text box with additional configuration options
    let textBoxInstance = $("#textBoxContainer5").dxTextBox({
        accessKey: 'x',                           // Shortcut key
        activeStateEnabled: true,                 // Enable active state
        buttons: ['clear'],                       // Clear button
        showClearButton: true,                    // Show clear button inside the text box
        elementAttr: {
            id: "elementId",
            class: "class-name"
        },
        focusStateEnabled: true,                  // Enable focus styles
        hint: 'enter a text',                     // Tooltip hint
        hoverStateEnabled: true,                  // Enable hover styles
        inputAttr: {
            id: "inputId"
        },
        isValid: true,                            // Initial validity state
        maxLength: 50,                            // Maximum character length
        mode: 'email',                            // Input type
        name: 'email',                            // Name attribute
        onChange: function (e) {                  // Event when the value changes
            console.log(e.component);
        },
        onContentReady: function (e) {            // Event when content is ready
            console.log(e.component);
        },
        onInput: function (e) {                   // Event during input
            console.log(e.component);
        },
        showMaskMode: 'onFocus',                  // Show mask only when focused
        spellCheck: true,                         // Enable spellcheck
        useMaskedValue: true,                     // Use the masked value
        valueChangeEvent: 'change'                // Event triggered on value change
    }).dxTextBox("instance");

    // Add event listener for 'change' event to show an alert
    textBoxInstance.on("change", function (e) {
        alert("value changed");
    });

    // Add button click events for various text box actions
    $("#blur").click(function () {                // Removes focus from the TextBox
        textBoxInstance.blur();
        console.log("TextBox blurred.");
    });

    $("#focus").click(function () {               // Focuses the TextBox
        textBoxInstance.focus();
        console.log("TextBox focused.");
    });

    $("#reset").click(function () {               // Resets the TextBox to its default state
        textBoxInstance.reset();
        console.log("TextBox reset.");
    });

    $("#dispose").click(function () {             // Disposes of the TextBox instance
        textBoxInstance.dispose();
        console.log("TextBox disposed.");
    });

    $("#repaint").click(function () {             // Repaints the TextBox, updating its appearance
        textBoxInstance.repaint();
        console.log("TextBox repainted.");
    });

    // Access a custom button from the text box
    const myCustomButton = $("#textBoxContainer5").dxTextBox("getButton", "myCustomButton");
    myCustomButton.focus(); // Focus the custom button

    // Log masked and unmasked values of the third text box
    console.log(maskedValue, unmaskedValue);

    // // Update the value
    // textBoxInstance.option("value", "New Value");

    // // Enable or disable the TextBox
    // textBoxInstance.option("disabled", true);  // Disable
    // textBoxInstance.option("disabled", false); // Enable

    // // Update the placeholder
    // textBoxInstance.option("placeholder", "New Placeholder");

    // // Update the input type
    // textBoxInstance.option("mode", "text");

    // // Update the styling mode
    // textBoxInstance.option("stylingMode", "underlined");
});