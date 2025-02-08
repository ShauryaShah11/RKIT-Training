$(function () {
    //Normal Buttons
    $("#normal-contained").dxButton({
        text: 'Normal Contained',
        type: 'normal',
        stylingMode: 'contained',
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

    // Default Buttons
    $("#default-contained").dxButton({
        text: 'Default Contained',
        type: 'default',
        stylingMode: 'contained',
        onClick: function () {
            DevExpress.ui.notify("Default Contained Button Was Clicked");
        }
    });

    $("#default-outlined").dxButton({
        text: 'Default Outlined',
        type: 'default',
        stylingMode: 'outlined',
        onClick: function () {
            DevExpress.ui.notify("Default Outlined Button Was Clicked");
        }
    });

    $("#default-text").dxButton({
        text: 'Default Text',
        type: 'default',
        stylingMode: 'text',
        onClick: function () {
            DevExpress.ui.notify("Default Text Button Was Clicked");
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

    $('#buttonContainer2').dxButton({
        text: 'Click Me!',
        stylingMode: 'outlined',
        icon: 'comment',
        onClick: function () {
            DevExpress.ui.notify("The Button Was Clicked");
        }
    })
})