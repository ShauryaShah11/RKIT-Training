$(function () {
    /**
     * DevExtreme dxButton Configuration
     * 
     * Options used:
     * - text (string): The text on the button.
     * - type (string): Defines the button style.
     *   Possible values: "normal" (default), "success", "danger", "default".
     * - stylingMode (string): Defines button appearance.
     *   Possible values: "contained" (default), "outlined", "text".
     * - icon (string): Specifies an icon (optional).
     * - onClick (function): Click event handler.
     */

    // Normal Buttons
    $("#normal-contained").dxButton({
        text: 'Normal Contained',
        type: 'normal', // Default type
        stylingMode: 'contained', // Default styling
        onClick: function () {
            DevExpress.ui.notify("Normal Contained Button Was Clicked");
        }
    });

    $("#normal-outlined").dxButton({
        text: 'Normal Outlined',
        type: 'normal',
        stylingMode: 'outlined',
        onClick: function () {
            DevExpress.ui.notify("Normal Outlined Button Was Clicked");
        }
    });

    $("#normal-text").dxButton({
        text: 'Normal Text',
        type: 'normal',
        stylingMode: 'text',
        onClick: function () {
            DevExpress.ui.notify("Normal Text Button Was Clicked");
        }
    });

    // Success Buttons
    $("#success-contained").dxButton({
        text: 'Success Contained',
        type: 'success',
        stylingMode: 'contained',
        onClick: function () {
            DevExpress.ui.notify("Success Contained Button Was Clicked");
        }
    });

    $("#success-outlined").dxButton({
        text: 'Success Outlined',
        type: 'success',
        stylingMode: 'outlined',
        onClick: function () {
            DevExpress.ui.notify("Success Outlined Button Was Clicked");
        }
    });

    $("#success-text").dxButton({
        text: 'Success Text',
        type: 'success',
        stylingMode: 'text',
        onClick: function () {
            DevExpress.ui.notify("Success Text Button Was Clicked");
        }
    });

    // Danger Buttons
    $("#danger-contained").dxButton({
        text: 'Danger Contained',
        type: 'danger',
        stylingMode: 'contained',
        onClick: function () {
            DevExpress.ui.notify("Danger Contained Button Was Clicked");
        }
    });

    $("#danger-outlined").dxButton({
        text: 'Danger Outlined',
        type: 'danger',
        stylingMode: 'outlined',
        onClick: function () {
            DevExpress.ui.notify("Danger Outlined Button Was Clicked");
        }
    });

    $("#danger-text").dxButton({
        text: 'Danger Text',
        type: 'danger',
        stylingMode: 'text',
        onClick: function () {
            DevExpress.ui.notify("Danger Text Button Was Clicked");
        }
    });

    // Button with Icon
    $('#buttonContainer2').dxButton({
        text: 'Click Me!',
        stylingMode: 'outlined',
        icon: 'comment', // Possible icons: 'home', 'search', 'user', 'comment', etc.
        onClick: function () {
            DevExpress.ui.notify("The Button Was Clicked");
        }
    });

    /**
     * Dynamic Updates
     * - Changing properties dynamically using `.option()` method.
     */

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
});
