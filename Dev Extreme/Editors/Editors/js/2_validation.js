$(function () {
    // Validation group name
    const validationGroup = "myValidationGroup";

    // Revalidate all inputs when their value changes
    const revalidate = function () {
        DevExpress.validationEngine.validateGroup(validationGroup);
    };

    // Initialize the components without validation rules
    $("#checkBoxContainer").dxCheckBox({
        text: "Accept Terms and Conditions",
        value: false,
        onValueChanged: revalidate
    });

    $("#dateBoxContainer1").dxDateBox({
        type: "date",
        value: new Date(),
        displayFormat: "yyyy-MM-dd",
        onValueChanged: revalidate
    });

    $("#dateBoxContainer2").dxDateBox({
        type: "date",
        value: new Date(),
        displayFormat: "yyyy-MM-dd",
        min: new Date(2020, 0, 1),    // January 1, 2020
        max: new Date(2025, 11, 31),  // December 31, 2025
        onValueChanged: revalidate
    });

    $("#numberBoxContainer").dxNumberBox({
        value: 10,
        showSpinButtons: true,
        min: 0,
        max: 100,
        onValueChanged: revalidate
    });

    $("#selectBoxContainer").dxSelectBox({
        items: ["Option 1", "Option 2", "Option 3"],
        value: "Option 1",
        onValueChanged: revalidate
    });

    $("#textBoxContainer").dxTextBox({
        value: "",
        placeholder: "Enter some text...",
        onValueChanged: revalidate
    });

    $("#textAreaContainer").dxTextArea({
        value: "",
        placeholder: "Write your comments here...",
        height: 90,
        onValueChanged: revalidate
    });

    $("#fileUploaderContainer").dxFileUploader({
        selectButtonText: "Upload Image",
        labelText: "",
        accept: "image/*",
        onValueChanged: revalidate
    });

    $("#radioGroupContainer").dxRadioGroup({
        items: ["Option A", "Option B", "Option C"],
        value: "Option A",
        layout: "horizontal",
        onValueChanged: revalidate
    });

    // Button to submit form
    $("#buttonContainer").dxButton({
        text: "Submit",
        type: "success",
        validationGroup: validationGroup,
        onClick: function () {
            const result = DevExpress.validationEngine.validateGroup(validationGroup);
            if (result.isValid) {
                DevExpress.ui.notify("Form submitted successfully!", "success", 2000);
            } else {
                DevExpress.ui.notify("Please fix validation errors", "error", 3000);
            }
        }
    });

    // Validator components applying rules outside the individual components
    $("#checkBoxContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "You must accept the terms"
        }]
    }).dxValidator("instance");

    $("#dateBoxContainer1").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Date 1 is required"
        }]
    }).dxValidator("instance");

    $("#dateBoxContainer2").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Date 2 is required"
        }]
    }).dxValidator("instance");

    $("#numberBoxContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "range",
            min: 0,
            max: 100,
            message: "Number must be between 0 and 100"
        }]
    }).dxValidator("instance");

    $("#selectBoxContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "You must select an option"
        }]
    }).dxValidator("instance");

    $("#textBoxContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Text is required"
        }]
    }).dxValidator("instance");

    $("#textAreaContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Comments are required"
        }]
    }).dxValidator("instance");

    $("#fileUploaderContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Please upload a file"
        }]
    }).dxValidator("instance");

    $("#radioGroupContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "You must select an option"
        }]
    }).dxValidator("instance");

});
