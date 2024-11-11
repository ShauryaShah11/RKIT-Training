$(document).ready(function () {
    $("#btn-submit").click(onSubmit);

    $("#nextStep").click(nextStep);
    $("#prevStep").click(prevStep);

    function onSubmit(event) {
        event.preventDefault();

        // Gather personal information
        let fullname = $("#name").val();
        let username = $("#username").val();
        let email = $("#email").val();
        let password = $("#password").val();
        let cpassword = $("#cpassword").val();
        let phoneNumber = $("#phonenumber").val();

        // Gather business information
        let businessName = $("#business_name").val();
        let businessType = $("input[name='business_type']:checked").val();

        // Gather financial details
        let currency = $("#currency").val();
        let accountingMethod = $("#accounting_method").val();
        let startDate = $("#start_date").val();

        // Gather contact details
        let address = $("#address").val();
        let city = $("#city").val();
        let businessCertificate = $("#fileupload").val();

        // Create instances of the classes
        const personalInfo = new PersonalInfo(
            fullname,
            username,
            email,
            phoneNumber,
            password
        );
        const businessInfo = new BusinessInfo(businessName, businessType);
        const financialDetails = new FinancialDetails(
            currency,
            accountingMethod,
            startDate
        );
        const contactDetails = new ContactDetails(
            address,
            city,
            businessCertificate
        );

        // Create an instance of the User class
        const user = new User(
            personalInfo,
            businessInfo,
            financialDetails,
            contactDetails
        );

        localStorage.setItem('user', JSON.stringify(personalInfo));
        alert("Registered Successfully");
        window.location.href = "./login.html";
        console.log(user);
    }

    let currentStep = 0;
    const formSteps = $(".form-step");

    function showStep(step) {
        formSteps.each(function (index) {
            $(this).toggleClass("active", index === step);
        });
    }

    // Expose nextStep and prevStep to the global scope
    function nextStep() {
        const form = $("#multi-step-form");
        if (form.valid()) {
            if (currentStep < formSteps.length - 1) {
                currentStep++;
                showStep(currentStep);
            }
        } else {
            form.validate().focusInvalid();
        }
    };

   function prevStep() {
        if (currentStep > 0) {
            currentStep--;
            showStep(currentStep);
        }
    };

    // Initialize the form by showing the first step
    showStep(currentStep);

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
            return true;
        },
        "Please upload a valid file (PDF, max 2MB)."
    );

    $("#multi-step-form").validate({
        rules: {
            name: {
                required: true,
                minlength: 3,
            },
            username: {
                required: true,
                minlength: 3,
            },
            email: {
                required: true,
                email: true,
            },
            phonenumber: {
                required: true,
                minlength: 10,
            },
            password: {
                required: true,
                minlength: 8,
            },
            cpassword: {
                equalTo: "#password",
            },
            business_name: {
                required: true,
            },
            business_type: {
                required: true,
            },
            currency: {
                required: true,
            },
            accounting_method: {
                required: true,
            },
            start_date: {
                required: true,
            },
            address: {
                required: true,
            },
            city: {
                required: true,
            },
            business_certificate: {
                required: true,
                fileType: {
                    types: ["application/pdf"],
                    maxSize: 2 * 1024 * 1024, // 2MB
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
            cpassword: {
                equalTo: "Please enter the same password as above",
            },
            business_name: {
                required: "Please enter your business name.",
            },
            business_type: {
                required: "Please select your business type.",
            },
            currency: {
                required: "Please select your currency.",
            },
            accounting_method: {
                required: "Please select your accounting method.",
            },
            start_date: {
                required: "Please enter the business start date.",
                validDate: "Please enter a valid date.",
            },
            address: {
                required: "Please enter your address.",
            },
            city: {
                required: "Please enter your city.",
            },
            business_certificate: {
                required:
                    "Please upload your business registration certificate.",
                fileType: "Please upload a valid file (PDF, max 2MB).",
            },
        },
        errorClass: "is-invalid", // Bootstrap class for error state
        validClass: "is-valid", // Bootstrap class for valid state
        errorElement: "div", // Element to wrap the error message
        highlight: function (element, errorClass, validClass) {
            $(element).removeClass(validClass).addClass(errorClass); // Add error class
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass(errorClass).addClass(validClass); // Add valid class
        },
        submitHandler: function (form) {
            form.submit();
        },
        invalidHandler: function (event, validator) {
            alert("Please fix the errors in the form.");
        },
    });
});
