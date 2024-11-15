$(document).ready(function () {
    // When the submit button is clicked, prevent the default form submission behavior
    $("#btn-submit").click(function (event) {
        event.preventDefault(); // Prevent default form submission
        if ($("#multi-step-form").valid()) {
            // Check if the form is valid
            onSubmit(event); // Call the onSubmit function only if the form is valid
        }
    });

    // Assign click event handlers to next and previous step buttons
    $(".nextStep").click(nextStep);
    $(".prevStep").click(prevStep);

    // Variables to store form data
    let personalInfo, businessInfo, financialDetails, contactDetails;

    // Function to store personal information from form fields
    function storePersonalInfo() {
        let fullname, username, email, password, phoneNumber;
        fullname = $("#name").val(); // Get full name from input field
        username = $("#username").val(); // Get username from input field
        email = $("#email").val(); // Get email from input field
        password = $("#password").val(); // Get password from input field
        phoneNumber = $("#phonenumber").val(); // Get phone number from input field

        // Create a PersonalInfo object with the gathered data
        personalInfo = PersonalInfo.create(
            fullname,
            username,
            email,
            phoneNumber,
            password
        );
    }

    // Function to store business information from form fields
    function storeBusinessInfo() {
        let businessName, businessType;
        businessName = $("#business_name").val(); // Get business name from input field
        businessType = $("input[name='business_type']:checked").val(); // Get selected business type

        // Create a BusinessInfo object with the gathered data
        businessInfo = BusinessInfo.create(businessName, businessType);
    }

    // Function to store financial details from form fields
    function storeFinancialDetails() {
        let currency, accountingMethod, startDate;
        currency = $("#currency").val(); // Get currency from input field
        accountingMethod = $("#accounting_method").val(); // Get accounting method from input field
        startDate = $("#start_date").val(); // Get start date from input field

        // Create a FinancialDetails object with the gathered data
        financialDetails = FinancialDetails.create(
            currency,
            accountingMethod,
            startDate
        );
    }

    // Function to handle form submission
    function onSubmit(event) {
        // Gather contact details from form fields
        let address, city, businessCertificate;
        address = $("#address").val(); // Get address from input field
        city = $("#city").val(); // Get city from input field
        businessCertificate = $("#fileupload").val(); // Get business certificate file path

        // Create a ContactDetails object with the gathered data
        contactDetails = ContactDetails.create(
            address,
            city,
            businessCertificate
        );

        // Save data to localStorage and check if registration is successful
        localStorage.setItem("user", JSON.stringify(personalInfo));
        if (localStorage.getItem("user")) {
            alert("Registered Successfully");
            window.location.href = "./login.html"; // Redirect to login page on success
        } else {
            alert("Registration Failed");
        }
    }

    let currentStep = 0; // Initialize the current step to 0 (first step)
    const formSteps = $(".form-step"); // Get all form steps

    // Function to show the current step in the multi-step form
    function showStep(step) {
        formSteps.each(function (index) {
            // Toggle the 'active' class based on the current step
            $(this).toggleClass("active", index === step);
        });
    }

    // Move to the next step in the multi-step form
    function nextStep() {
        const form = $("#multi-step-form");
        if (form.valid()) {
            if (currentStep < formSteps.length - 1) {
                // Store data based on the current step
                if (currentStep === 0) {
                    storePersonalInfo();
                    storeBusinessInfo();
                } else if (currentStep === 1) {
                    storeFinancialDetails();
                }
                currentStep++;
                showStep(currentStep);
            }
        } else {
            form.validate().focusInvalid();
        }
    }

    // Move to the previous step in the multi-step form
    function prevStep() {
        if (currentStep > 0) {
            // Ensure we're not at the first step
            currentStep--; // Decrement the step counter
            showStep(currentStep); // Show the previous step
        }
    }

    // Initialize the form by showing the first step
    showStep(currentStep);

    // Custom validator method to check file type and size
    $.validator.addMethod(
        "fileType",
        function (value, element, param) {
            var file = element.files[0];
            if (file) {
                var fileType = file.type;
                var fileSize = file.size;
                var validTypes = param.types;
                var maxSize = param.maxSize;
                return validTypes.includes(fileType) && fileSize <= maxSize;
            }
            return false;
        },
        "Please upload a valid file (PDF, max 2MB)." // Error message for invalid files
    );

    // Initialize form validation on the multi-step form
    $("#multi-step-form").validate({
        rules: {
            name: { required: true, minlength: 3 },
            username: { required: true, minlength: 3 },
            email: { required: true, email: true },
            phonenumber: { required: true, minlength: 10 },
            password: { required: true, minlength: 8 },
            cpassword: { equalTo: "#password" },
            business_name: { required: true },
            business_type: { required: true },
            currency: { required: true },
            accounting_method: { required: true },
            start_date: { required: true },
            address: { required: true },
            city: { required: true },
            business_certificate: {
                required: true,
                fileType: {
                    types: ["application/pdf"], // Allowed file types
                    maxSize: 2 * 1024 * 1024, // 2MB in bytes
                },
            },
        },
        messages: {
            name: {
                required: "Please enter your name.",
                minlength: "Your name must be at least 3 characters long.",
            },
            username: {
                required: "Please enter your username.",
                minlength: "Your username must be at least 3 characters long.",
            },
            email: {
                required: "Please enter your email address.",
                email: "Please enter a valid email address.",
            },
            phonenumber: {
                required: "Please enter your phone number.",
                minlength: "Your phone number must be at least 10 digits long.",
            },
            password: {
                required: "Please provide a password.",
                minlength: "Your password must be at least 8 characters long.",
            },
            cpassword: { equalTo: "Please enter the same password as above" },
            business_name: { required: "Please enter your business name." },
            business_type: { required: "Please select your business type." },
            currency: { required: "Please select your currency." },
            accounting_method: {
                required: "Please select your accounting method.",
            },
            start_date: { required: "Please enter the business start date." },
            address: { required: "Please enter your address." },
            city: { required: "Please enter your city." },
            business_certificate: {
                required:
                    "Please upload your business registration certificate.",
                fileType: "Please upload a valid file (PDF, max 2MB).",
            },
        },
        errorClass: "is-invalid",
        validClass: "is-valid",
        errorElement: "div",
        highlight: function (element, errorClass, validClass) {
            // Highlight invalid fields
            $(element).removeClass(validClass).addClass(errorClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            // Remove highlight from valid fields
            $(element).removeClass(errorClass).addClass(validClass);
        },
        submitHandler: function (form) {
            // Submit the form if all fields are valid
            form.submit();
        },
        invalidHandler: function (event, validator) {
            // Show alert if there are validation errors
            alert("Please fix the errors in the form.");
        },
    });
});
