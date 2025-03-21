$(function () {
    // Initialize the main NumberBox with various configurations
    var numberBox = $("#numberBoxContainer").dxNumberBox({
        value: 20,                      // Initial Value (default: null)
        min: 16,                        // Minimum Value (default: -Infinity)
        max: 100,                       // Maximum Value (default: Infinity)
        showSpinButtons: true,          // Show + and - Buttons (true/false) (default: false)
        buttons: ['clear', 'spins'],    // Buttons to show: ['clear', 'spins'] (default: [])
        showClearButton: true,          // Show Clear Button (true/false) (default: false)
        rtlEnabled: false,              // Right-to-Left Layout (true/false) (default: false)
        stylingMode: 'filled',          // Styling options: 'filled', 'outlined', 'underlined' (default: 'outlined')
        useLargeSpinButtons: true,      // Use large spin buttons (true/false) (default: false)
        readOnly: false,                // Make input read-only (true/false) (default: false)
        disabled: false,                // Enable/disable input (true/false) (default: false)
        inputAttr: { 'aria-label': 'Number Input' }, // Accessibility attributes (object) (default: {})
        placeholder: "Enter a number",  // Placeholder text (string) (default: "")
        format: "#,##0.##",             // Number format (default: null, example: "#,##0.00")
        valueChangeEvent: "input",      // Value change trigger: 'change', 'input', 'keyup' (default: "change")
        invalidValueMessage: "Invalid number!", // Validation message (string) (default: "Value is invalid")
        validationMessageMode: "auto",  // Validation message display mode: 'auto', 'always' (default: "auto")
        validationError: { message: "Number is out of range" }, // Custom validation error message (default: null)

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
        showSpinButtons: true,         // Show spin buttons (true/false) (default: false)
        min: 0,                        // Minimum value (default: -Infinity)
        showClearButton: true,         // Show clear button (true/false) (default: false)
        stylingMode: 'outlined'        // Styling mode: 'filled', 'outlined', 'underlined' (default: 'outlined')
    });

    // Additional functionalities (Examples)

    // // Update the value dynamically
    // numberBox.option("value", newValue);

    // // Enable or disable the NumberBox dynamically
    // numberBox.option("disabled", true);  // Disable
    // numberBox.option("disabled", false); // Enable

    // // Update the placeholder dynamically
    // numberBox.option("inputAttr", { placeholder: "New Placeholder" });

    // // Update the minimum and maximum values dynamically
    // numberBox.option("min", newMinValue);
    // numberBox.option("max", newMaxValue);

    // // Update the styling mode dynamically
    // numberBox.option("stylingMode", "outlined");

    // // Update the format dynamically
    // numberBox.option("format", "#,##0.00");
});
