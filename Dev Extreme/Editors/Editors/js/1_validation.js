$(function () {
    
    // Normal Validation
    // Initialize email field with validation
    var emailValidator = $("#normalEmail").dxTextBox({
        placeholder: 'Email',
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Enter an email'
        }, {
            type: 'email',
            message: 'Enter a valid email address'
        }]
    }).dxValidator("instance");

    // Initialize password field with validation
    var passwordValidator = $("#normalPassword").dxTextBox({
        placeholder: 'Password',
        mode: 'password',
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Enter a password'
        }]
    }).dxValidator("instance");

    // Initialize submit button
    $("#normalSubmit").dxButton({
        text: "Submit",
        onClick: function () {
            // Trigger validation for email and password fields individually
            var emailValidationResult = $("#normalEmail").dxValidator("instance").validate();
            var passwordValidationResult = $("#normalPassword").dxValidator("instance").validate();

            if (emailValidationResult.isValid && passwordValidationResult.isValid) {
                alert("Submitting...");
            } else {
                alert("Please fix validation errors");
            }
        }
    });
    
    // Group Validation
    let loginGroup = 'loginGroup';

    $('#login').dxTextBox({
        placeholder: 'Login'
    }).dxValidator({
        validationRules: [
            { type: 'required', message: 'Login is required.' },
            { type: 'pattern', pattern: '^[a-zA-Z]+$', message: 'Login should only contain letters.' }
        ],
        validationGroup: loginGroup
    });

    let passwordInstance = $('#loginPassword').dxTextBox({
        placeholder: 'Password',
        mode: 'password',
        buttons: [{
            name: 'password',
            location: 'after',
            options: {
                icon: 'fa fa-eye',
                stylingMode: 'text',
                onClick: function () {
                    let currentMode = passwordInstance.option('mode');
                    passwordInstance.option('mode', currentMode === 'text' ? 'password' : 'text');
                }
            }
        }]
    }).dxValidator({
        validationRules: [{ type: 'required', message: 'Password is required.' }],
        validationGroup: loginGroup
    }).dxTextBox('instance');

    $('#loginButton').dxButton({
        text: 'Sign In',
        validationGroup: loginGroup,
        onClick: function (e) {
            let result = DevExpress.validationEngine.validateGroup(loginGroup);

            if (!result.isValid) {
                console.log('Validation failed:', result.brokenRules);
                // Errors will now appear in the dxValidationSummary
            } else {
                alert('Form submitted successfully!');
            }
        }
    });

    $('#loginSummary').dxValidationSummary({
        validationGroup: loginGroup
    });

    // Group Validation With Custom Validation on Last Name with help of checkbox

    let validateGroup2 = 'validateGroup2';
    $('#firstName').dxTextBox({
        placeholder: 'first name'
    }).dxValidator({
        validationRules: [{
            type: 'required'
        }],
        validationGroup: validateGroup2
    });

    $('#lastName').dxTextBox({
        placeholder: 'last name'
    }).dxValidator({
        validationRules: [{
            type: 'custom',
            message: 'Required',
            reevaluate: true,
            validationCallback: function (e) {
                if ($('#checkBox').dxCheckBox('option', 'value')) {
                    return e.value;
                }
                return true;
            }
        }],
        validationGroup: validateGroup2
    });

    $('#validationButton').dxButton({
        text: 'Validate',
        onClick: function (e) {
            let result = DevExpress.validationEngine.validateGroup('validateGroup2');

            if (!result.isValid) {
                console.log('Validation failed:', result.brokenRules);
            } else {
                alert('Validation successful!');
            }
        }
    });

    $("#checkBox").dxCheckBox({
        text: "Validate last name",
        value: false
    });

    // Contact Validation

    const contactGroup = "contactGroup";

    const callbacks = [];

    const revalidate = function () {
        callbacks.forEach(func => func());
    };

    const phone = $("#contactPhone").dxTextBox({
        placeholder: "Phone",
        onValueChanged: revalidate
    }).dxTextBox("instance");

    const email = $("#contactEmail").dxTextBox({
        placeholder: "Email",
        onValueChanged: revalidate
    }).dxTextBox("instance");

    const validator = $("#contacts").dxValidator({
        validationGroup: contactGroup,
        validationRules: [{
            type: "custom",
            validationCallback: function (e) {
                const phoneValue = phone.option("value");
                const emailValue = email.option("value");
                return Boolean(phoneValue || emailValue);
            },
            message: "Specify your phone or email"
        }],
        adapter: {
            getValue: function () {
                return phone.option("value") || email.option("value");
            },
            applyValidationResults: function (e) {
                $("#contacts").css({
                    "border": e.isValid ? "none" : "1px solid red",
                    "padding": "10px",
                    "margin": "10px 0"
                });
            },
            validationRequestsCallbacks: callbacks
        }
    }).dxValidator("instance");

    $("#contactButton").dxButton({
        text: "Contact me",
        validationGroup: contactGroup,
        onClick: function (e) {
            const result = DevExpress.validationEngine.validateGroup(contactGroup);

            if (result.isValid) {
                const phoneValue = phone.option("value");
                const emailValue = email.option("value");

                if (phoneValue || emailValue) {
                    DevExpress.ui.notify("Form is valid! Submitting...", "success", 3000);
                } else {
                    DevExpress.ui.notify("Please enter either phone or email", "error", 3000);
                }
            } else {
                DevExpress.ui.notify("Please fix validation errors", "error", 3000);
            }
        }
    });

    $("#contactSummary").dxValidationSummary({
        validationGroup: contactGroup
    });
});