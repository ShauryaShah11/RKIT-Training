$(function () {
    // Group name for validating multiple inputs together
    let registerGroup = 'registerGroup';

    // Email validation helper
    // Simulates server check for existing emails
    const sendRequest = function (value) {
        // Known emails that will be rejected
        const dummyEmail = [
            "john.doe@example.com",
            "jane.smith@gmail.com",
            "user123@yahoo.com",
            "test.account@outlook.com",
            "dummy.email@company.org",
            "contact@domain.co",
            "sample_user@service.net",
            "hello.world@website.io"
        ];

        const d = $.Deferred();

        setTimeout(() => {
            if (dummyEmail.includes(value)) {
                d.reject();  // Reject with an error message
            } else {
                d.resolve();
            }
        }, 1000);

        return d.promise();  // Return the promise
    };

    // Name Input
    // dxTextBox - simple text input
    $("#nameContainer").dxTextBox({
        placeholder: 'Enter your name' // Default: ""
    }).dxValidator({
        validationRules: [{
            type: 'required', // Fails if empty
            message: 'Name is required'
        }, {
            type: 'pattern', // Regex validation
            pattern: "[a-zA-Z]+$", // Letters only
            message: 'name should only contain letters'
        }
        ],
        validationGroup: registerGroup
    });

    // Email Input
    $("#emailContainer").dxTextBox({
        placeholder: 'Enter your email'
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Email is required'
        }, {
            type: 'email', // Built-in email format check
            message: 'Please enter a valid email address'
        }, {
            type: 'async', // Server-side validation
            message: 'email is already registered',
            validationCallback(params) {
                return sendRequest(params.value)
            }
        }],
        validationGroup: registerGroup
    });

    // Date Box (Date of Birth)
    $("#dobContainer").dxDateBox({
        placeholder: 'Select your Date of Birth',
        type: 'date', // Options: 'date', 'time', 'datetime' (default: 'date')
        pickerType: 'calendar', // Options: 'calendar', 'list', 'native', 'rollers'
        acceptCustomValue: true, // Allow typing date (default: false)
        useMaskBehavior: true, // Enable input mask (default: false)
        max: new Date() // Can't select future dates
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Date of Birth is required'
        }, {
            type: 'range', // Min/max date check
            max: new Date(),
            message: 'birthdate can not be in future'
        },
        {
            type: 'custom', // Custom validation logic
            validationCallback: function (e) {
                const today = new Date();
                const birthDate = new Date(e.value);

                const age = today.getFullYear() - birthDate.getFullYear();
                let ageInstance = $("#ageContainer").dxNumberBox('instance');
                ageInstance.option("value", age);
                return age >= 18;
            },
            message: 'You must be at least 18 years old'
        }
        ],
        validationGroup: registerGroup
    });

    // Country Selection
    // dxSelectBox - dropdown with options
    $("#countryContainer").dxSelectBox({
        items: ['USA', 'Canada', 'UK', 'Australia', 'India'], // Dropdown options
        value: null, // No initial selection
        placeholder: 'Select your country'
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Country is required'
        }, {
            type: 'custom',
            validationCallback: function (e) {
                return e.value != 'USA'; // Reject if USA selected
            },
            message: 'Choose other country then USA'
        }
        ],
        validationGroup: registerGroup
    });

    // Age Input
    // dxNumberBox - numeric input with constraints
    $("#ageContainer").dxNumberBox({
        value: null, // Default: null
        min: 18, // Minimum value
        max: 100, // Maximum value
        placeholder: 'Enter your age',
        //readOnly: true, // Prevent editing (default: false)
        disabled: true // Completely disable input (default: false)
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Age is required'
        }, {
            type: 'range', // Number range check
            min: 18,
            max: 100,
            message: 'Age must be between 18 and 100'
        }],
        validationGroup: registerGroup
    });

    // Comments Text Area
    // dxTextArea - multi-line text input
    $("#commentsContainer").dxTextArea({
        placeholder: 'Enter comments' // Default: ""
    }).dxValidator({
        validationRules: [
            {
                type: 'stringLength', // Text length validation
                min: 5, // Min chars
                max: 100, // Max chars
                message: 'Comment should be between 5 to 100 characters'
            },
            {
                type: 'required',
                message: 'comments is required'
            }
        ],
        validationGroup: registerGroup
    });

    // Terms Checkbox
    // dxCheckBox - boolean on/off input
    $("#termsContainer").dxCheckBox({
        text: 'I agree to the Terms and Conditions', // Label text
        value: false // Initial state: unchecked (default: false)
    }).dxValidator({
        validationRules: [{
            type: 'required', // For checkbox, requires checked=true
            message: 'You must agree to the terms'
        }],
        validationGroup: registerGroup
    });

    // File Uploader (Profile Picture)
    // dxFileUploader - file selection and upload
    $("#fileUploaderContainer").dxFileUploader({
        accept: 'image/*', // File types (default: "")
        selectButtonText: 'Upload Profile Picture', // Button label (default: "Select file")
        uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload", // Upload endpoint
        allowCanceling: true, // Allow cancel during upload (default: true)
        maxFileSize: 100000, // Max bytes (default: 0 = unlimited)
        multiple: true // Allow multiple files (default: false)
    }).dxValidator({
        validationRules: [
            {
                type: 'required',
                message: 'profile picture should be required'
            },
            {
                type: 'custom',
                validationCallback: function (e) {
                    return e.value.length <= 1;  // Allow only one file
                },
                message: 'You can upload only one file'
            }
        ],
        validationGroup: registerGroup
    });

    // Gender Radio Group
    // dxRadioGroup - mutually exclusive options
    $("#genderContainer").dxRadioGroup({
        items: ['Male', 'Female', 'Other'], // Radio options
        value: 'Male' // Initial selection (default: null)
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Please select your gender'
        }],
        validationGroup: registerGroup
    });

    // Submit Button
    // dxButton - clickable button
    $("#submitContainer").dxButton({
        text: 'Submit', // Button label
        onClick: function () {
            // Run validation on all inputs in the group
            let result = DevExpress.validationEngine.validateGroup(registerGroup);

            // Check if all fields are valid
            if (result.isValid) {
                // Save data to session storage
                var formData = {
                    name: $("#nameContainer").dxTextBox("instance").option("value"),
                    email: $("#emailContainer").dxTextBox("instance").option("value"),
                    dob: $("#dobContainer").dxDateBox("instance").option("value").toLocaleDateString("en-US"),
                    country: $("#countryContainer").dxSelectBox("instance").option("value"),
                    age: $("#ageContainer").dxNumberBox("instance").option("value"),
                    comments: $("#commentsContainer").dxTextArea("instance").option("value"),
                    terms: $("#termsContainer").dxCheckBox("instance").option("value"),
                    gender: $("#genderContainer").dxRadioGroup("instance").option("value")
                };
                // Get existing data or create new array
                var storedData = JSON.parse(sessionStorage.getItem('userFormData')) || []; 
                storedData.push(formData); // Add new entry

                sessionStorage.setItem('userFormData', JSON.stringify(storedData)); 
                alert('Form Submitted Successfully!');
            } else {
                alert('Please fix validation errors');
            }
        }
    });

    // Search Box
    // Filters and displays saved form submissions
    $("#searchContainer").dxTextBox({
        placeholder: 'Search saved data',
        onValueChanged: function (e) {
            var searchQuery = e.value.toLowerCase();
            var savedData = JSON.parse(sessionStorage.getItem('userFormData'));

            if (savedData && Array.isArray(savedData)) {
                // Filter by name match
                var filteredData = savedData.filter(item =>
                    item.name.toLowerCase() === searchQuery
                );

                if (filteredData.length > 0) {
                    // Display matching results in grid
                    $("#dataGridContainer").dxDataGrid({
                        dataSource: filteredData,
                        columns: Object.keys(filteredData[0]).map(key => ({ 
                            dataField: key, 
                            caption: key.charAt(0).toUpperCase() + key.slice(1) 
                        })),
                        showBorders: true, // Default: false
                        visible: true,
                        paging: { pageSize: 5 }, // Records per page
                        searchPanel: { visible: true } // Enable searching within grid
                    }).show();
                } else {
                    $("#dataGridContainer").dxDataGrid().hide();
                    alert('No matching data found!');
                }
            } else {
                alert('No saved data found in session storage!');
            }
        }
    });

    // Initialize empty DataGrid (hidden until search results)
    $("#dataGridContainer").dxDataGrid({
        visible: false // Hidden initially
    });

});