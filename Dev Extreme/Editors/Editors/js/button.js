$(function () {
    // Normal Buttons
    $("#normal-contained").dxButton({
        text: 'Normal Contained',          // Button text
        type: 'normal',                    // Button type
        stylingMode: 'contained',          // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Normal Contained Button Was Clicked");
        }
    });

    $("#normal-outlined").dxButton({
        text: 'Normal Outlined',           // Button text
        type: 'normal',                    // Button type
        stylingMode: 'outlined',           // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Normal Outlined Button Was Clicked");
        }
    });

    $("#normal-text").dxButton({
        text: 'Normal Text',               // Button text
        type: 'normal',                    // Button type
        stylingMode: 'text',               // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Normal Text Button Was Clicked");
        }
    });

    // Success Buttons
    $("#success-contained").dxButton({
        text: 'Success Contained',         // Button text
        type: 'success',                   // Button type
        stylingMode: 'contained',          // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Success Contained Button Was Clicked");
        }
    });

    $("#success-outlined").dxButton({
        text: 'Success Outlined',          // Button text
        type: 'success',                   // Button type
        stylingMode: 'outlined',           // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Success Outlined Button Was Clicked");
        }
    });

    $("#success-text").dxButton({
        text: 'Success Text',              // Button text
        type: 'success',                   // Button type
        stylingMode: 'text',               // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Success Text Button Was Clicked");
        }
    });

    // Default Buttons
    $("#default-contained").dxButton({
        text: 'Default Contained',         // Button text
        type: 'default',                   // Button type
        stylingMode: 'contained',          // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Default Contained Button Was Clicked");
        }
    });

    $("#default-outlined").dxButton({
        text: 'Default Outlined',          // Button text
        type: 'default',                   // Button type
        stylingMode: 'outlined',           // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Default Outlined Button Was Clicked");
        }
    });

    $("#default-text").dxButton({
        text: 'Default Text',              // Button text
        type: 'default',                   // Button type
        stylingMode: 'text',               // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Default Text Button Was Clicked");
        }
    });

    // Danger Buttons
    $("#danger-contained").dxButton({
        text: 'Danger Contained',          // Button text
        type: 'danger',                    // Button type
        stylingMode: 'contained',          // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Danger Contained Button Was Clicked");
        }
    });

    $("#danger-outlined").dxButton({
        text: 'Danger Outlined',           // Button text
        type: 'danger',                    // Button type
        stylingMode: 'outlined',           // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Danger Outlined Button Was Clicked");
        }
    });

    $("#danger-text").dxButton({
        text: 'Danger Text',               // Button text
        type: 'danger',                    // Button type
        stylingMode: 'text',               // Styling mode
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("Danger Text Button Was Clicked");
        }
    });

    // Button with icon
    $('#buttonContainer2').dxButton({
        text: 'Click Me!',                 // Button text
        stylingMode: 'outlined',           // Styling mode
        icon: 'comment',                   // Icon for the button
        onClick: function () {              // Click event handler
            DevExpress.ui.notify("The Button Was Clicked");
        }
    });

    // Dynamic Updates
    $("#disableButton").click(function () {
        $("#normal-contained").dxButton("instance").option("disabled", true);
        console.log("Button Disabled");
    });

    $("#enableButton").click(function () {
        $("#normal-contained").dxButton("instance").option("disabled", false);
        console.log("Button Enabled");
    });

    $("#changeTextButton").click(function () {
        $("#normal-contained").dxButton("instance").option("text", "New Text");
        console.log("Button Text Changed");
    });

    $("#changeTypeButton").click(function () {
        $("#normal-contained").dxButton("instance").option("type", "success");
        console.log("Button Type Changed");
    });

    $("#changeStylingModeButton").click(function () {
        $("#normal-contained").dxButton("instance").option("stylingMode", "text");
        console.log("Button Styling Mode Changed");
    });

    // // Update the text
    // $("#normal-contained").dxButton("instance").option("text", "New Text");

    // // Enable or disable the button
    // $("#normal-contained").dxButton("instance").option("disabled", true);  // Disable
    // $("#normal-contained").dxButton("instance").option("disabled", false); // Enable

    // // Update the button type
    // $("#normal-contained").dxButton("instance").option("type", "success");

    // // Update the styling mode
    // $("#normal-contained").dxButton("instance").option("stylingMode", "text");
});