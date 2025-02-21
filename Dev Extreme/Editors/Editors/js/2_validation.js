$(function () {
    // Validation group name
    const validationGroup = "myValidationGroup";

    // Revalidate all inputs when their value changes
    const revalidate = function () {
        DevExpress.validationEngine.validateGroup(validationGroup);
    };

    // Initialize the CheckBox component with revalidation on value change
    $("#checkBoxContainer").dxCheckBox({
        text: "Accept Terms and Conditions",
        value: false,
        onValueChanged: revalidate
    });

    // Initialize the first DateBox component with revalidation on value change
    $("#dateBoxContainer1").dxDateBox({
        type: "date",
        value: new Date(),
        displayFormat: "yyyy-MM-dd",
        onValueChanged: revalidate
    });

    // Initialize the second DateBox component with min and max date constraints
    $("#dateBoxContainer2").dxDateBox({
        type: "date",
        value: new Date(),
        displayFormat: "yyyy-MM-dd",
        min: new Date(2020, 0, 1),    // January 1, 2020
        max: new Date(2025, 11, 31),  // December 31, 2025
        onValueChanged: revalidate
    });

    // Initialize the NumberBox component with min and max constraints
    $("#numberBoxContainer").dxNumberBox({
        value: 10,
        showSpinButtons: true,
        min: 0,
        max: 100,
        onValueChanged: revalidate
    });

    // Initialize the SelectBox component with predefined options
    $("#selectBoxContainer").dxSelectBox({
        items: ["Option 1", "Option 2", "Option 3"],
        value: "Option 1",
        onValueChanged: revalidate
    });

    // Initialize the TextBox component with a placeholder
    $("#textBoxContainer").dxTextBox({
        value: "",
        placeholder: "Enter some text...",
        onValueChanged: revalidate
    });

    // Initialize the TextArea component with a placeholder and height
    $("#textAreaContainer").dxTextArea({
        value: "",
        placeholder: "Write your comments here...",
        height: 90,
        onValueChanged: revalidate
    });

    // Initialize the FileUploader component with image file acceptance
    $("#fileUploaderContainer").dxFileUploader({
        selectButtonText: "Upload Image",
        labelText: "",
        accept: "image/*",
        onValueChanged: revalidate
    });

    // Initialize the RadioGroup component with predefined options
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

    // Validator for CheckBox
    $("#checkBoxContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "You must accept the terms"
        }]
    }).dxValidator("instance");

    // Validator for first DateBox
    $("#dateBoxContainer1").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Date 1 is required"
        }]
    }).dxValidator("instance");

    // Validator for second DateBox
    $("#dateBoxContainer2").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Date 2 is required"
        }]
    }).dxValidator("instance");

    // Validator for NumberBox
    $("#numberBoxContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "range",
            min: 0,
            max: 100,
            message: "Number must be between 0 and 100"
        }]
    }).dxValidator("instance");

    // Validator for SelectBox
    $("#selectBoxContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "You must select an option"
        }]
    }).dxValidator("instance");

    // Validator for TextBox
    $("#textBoxContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Text is required"
        }]
    }).dxValidator("instance");

    // Validator for TextArea
    $("#textAreaContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Comments are required"
        }]
    }).dxValidator("instance");

    // Validator for FileUploader
    $("#fileUploaderContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "Please upload a file"
        }]
    }).dxValidator("instance");

    // Validator for RadioGroup
    $("#radioGroupContainer").dxValidator({
        validationGroup: validationGroup,
        validationRules: [{
            type: "required",
            message: "You must select an option"
        }]
    }).dxValidator("instance");
});