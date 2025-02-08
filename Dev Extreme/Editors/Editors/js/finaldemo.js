$(function () {
    let registerGroup = 'registerGroup';

    const sendRequest = function (value) {
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
    $("#nameContainer").dxTextBox({
        placeholder: 'Enter your name'
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Name is required'
        }, {
            type: 'pattern',
            pattern: "[a-zA-Z]+$",
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
            type: 'email',
            message: 'Please enter a valid email address'
        }, {
            type: 'async',
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
        type: 'date',
        pickerType: 'rollers',
        max: new Date()
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Date of Birth is required'
        }, {
            type: 'range',
            max: new Date(),
            message: 'birthdate can not be in future'
        },
        {
            type: 'custom',
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
    $("#countryContainer").dxSelectBox({
        items: ['USA', 'Canada', 'UK', 'Australia', 'India'],
        value: null,
        placeholder: 'Select your country'
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Country is required'
        }, {
            type: 'custom',
            validationCallback: function (e) {
                return e.value != 'USA';
            },
            message: 'Choose other country then USA'
        }
        ],
        validationGroup: registerGroup
    });

    // Age Input
    $("#ageContainer").dxNumberBox({
        value: 0,
        min: 18,
        max: 100,
        placeholder: 'Enter your age',
        readOnly: true
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Age is required'
        }, {
            type: 'range',
            min: 18,
            max: 100,
            message: 'Age must be between 18 and 100'
        }],
        validationGroup: registerGroup
    });

    // Comments Text Area
    $("#commentsContainer").dxTextArea({
        placeholder: 'Enter comments'
    }).dxValidator({
        validationRules: [
            {
                type: 'stringLength',
                min: 5,
                max: 100,
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
    $("#termsContainer").dxCheckBox({
        text: 'I agree to the Terms and Conditions',
        value: false
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'You must agree to the terms'
        }],
        validationGroup: registerGroup
    });

    // File Uploader (Profile Picture)
    $("#fileUploaderContainer").dxFileUploader({
        accept: 'image/*',
        selectButtonText: 'Upload Profile Picture',
        uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload",
        allowCanceling: true,
        maxFileSize: 100000,
        multiple: true
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
    $("#genderContainer").dxRadioGroup({
        items: ['Male', 'Female', 'Other'],
        value: 'Male'
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Please select your gender'
        }],
        validationGroup: registerGroup
    });

    // Submit Button
    $("#submitContainer").dxButton({
        text: 'Submit',
        onClick: function () {
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
                // Convert object to array and store it in session storage
                var storedData = JSON.parse(sessionStorage.getItem('userFormData')) || []; // Retrieve existing data (if any)
                storedData.push(formData); // Push new data to the array

                sessionStorage.setItem('userFormData', JSON.stringify(storedData)); // Store updated array in session storage
                alert('Form Submitted Successfully!');
            } else {
                alert('Please fix validation errors');
            }
        }
    });

    $("#searchContainer").dxTextBox({
        placeholder: 'Search saved data',
        onValueChanged: function (e) {
            var searchQuery = e.value.toLowerCase();
            var savedData = JSON.parse(sessionStorage.getItem('userFormData'));

            if (savedData && Array.isArray(savedData)) {
                var filteredData = savedData.filter(item =>
                    item.name.toLowerCase() === searchQuery
                );

                if (filteredData.length > 0) {
                    $("#dataGridContainer").dxDataGrid({
                        dataSource: filteredData,
                        columns: Object.keys(filteredData[0]).map(key => ({ dataField: key, caption: key.charAt(0).toUpperCase() + key.slice(1) })),
                        showBorders: true,
                        visible: true,
                        paging: { pageSize: 5 },
                        searchPanel: { visible: true }
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

    $("#dataGridContainer").dxDataGrid({
        visible: false // Initially hidden
    });

});