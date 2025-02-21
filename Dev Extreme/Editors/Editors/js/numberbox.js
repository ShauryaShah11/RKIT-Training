$(function () {
    // Initialize the main NumberBox with various configurations
    var numberBox = $("#numberBoxContainer").dxNumberBox({
        value: 20,                  // Initial Value
        min: 16,                    // Minimum Value
        max: 100,                   // Maximum Value
        showSpinButtons: true,      // Show + and - Buttons
        buttons: ['clear', 'spins'],// Show Clear & Spin Buttons
        showClearButton: true,      // Show Clear Button
        rtlEnabled: false,          // Left-to-Right Layout
        stylingMode: 'filled',      // Styling (filled, outlined, or underlined)
        useLargeSpinButtons: true,  // Large Spin Buttons
        readOnly: false,            // Allow Editing
        disabled: false,            // Enable/Disable Input
        inputAttr: { 'aria-label': 'Number Input' }, // Accessibility Label
        placeholder: "Enter a number", // Placeholder Text
        format: "#,##0.##",         // Number Format (Thousands Separator)
        valueChangeEvent: "input",  // Fire Value Change Event on Input
        invalidValueMessage: "Invalid number!", // Validation Message
        validationMessageMode: "auto", // Show Validation Messages on Error
        validationError: { message: "Number is out of range" }, // Custom Validation

        // 🔹 Event Listeners
        onFocusIn: function () {
            console.log("NumberBox Focused In");
        },
        onFocusOut: function () {
            console.log("NumberBox Focused Out");
        },
        onValueChanged: function (e) {
            console.log(`Value Changed: ${e.value}`);
        },
        onEnterKey: function () {
            console.log("Enter Key Pressed");
        },
        onPaste: function () {
            console.log("Text Pasted");
        },
        onCopy: function () {
            console.log("Text Copied");
        },
        onCut: function () {
            console.log("Text Cut");
        },
        onKeyDown: function () {
            console.log("Key Pressed Down");
        },
        onKeyUp: function () {
            console.log("Key Released");
        },
        onInitialized: function () {
            console.log("NumberBox Initialized");
        },
        onOptionChanged: function (e) {
            console.log(`Option Changed: ${e.name} -> ${e.value}`);
        }
    }).dxNumberBox("instance");

    // 🔹 Button Handlers for Dynamic Updates
    $("#clearNumber").click(function () {
        numberBox.reset();
        console.log("NumberBox Cleared");
    });
    $("#disableNumber").click(function () {
        numberBox.option("disabled", true);
        console.log("NumberBox Disabled");
    });
    $("#enableNumber").click(function () {
        numberBox.option("disabled", false);
        console.log("NumberBox Enabled");
    });
    $("#focusNumber").click(function () {
        numberBox.focus();
    });
    $("#repaintNumber").click(function () {
        numberBox.repaint();
    });
    $("#resetNumber").click(function () {
        numberBox.option("value", 20);
        console.log("NumberBox Reset to 20");
    });

    // Basic NumberBox with minimal configuration
    $("#basicNumberBoxContainer").dxNumberBox({
        showSpinButtons: true,
        min: 0,
        showClearButton: true,
        stylingMode: 'outlined'
    });

    // // Update the value
    // numberBox.option("value", newValue);

    // // Enable or disable the NumberBox
    // numberBox.option("disabled", true);  // Disable
    // numberBox.option("disabled", false); // Enable

    // // Update the placeholder
    // numberBox.option("inputAttr", { placeholder: "New Placeholder" });

    // // Update the minimum and maximum values
    // numberBox.option("min", newMinValue);
    // numberBox.option("max", newMaxValue);

    // // Update the styling mode
    // numberBox.option("stylingMode", "outlined");

    // // Update the format
    // numberBox.option("format", "#,##0.00");
});