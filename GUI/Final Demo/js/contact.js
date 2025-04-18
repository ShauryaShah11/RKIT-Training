$(document).ready(function () {
    // Function to validate form data
    function validateData(formData) {
        // Regular expression for email validation
        const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        // Regular expression for message validation (at least 10 characters)
        const messageRegex = /^.{10,}$/;

        // Validate email
        if (!emailRegex.test(formData.email)) {
            alert("Please enter a valid email address.");
            return false;
        }

        // Validate message
        if (!messageRegex.test(formData.message)) {
            alert("Message must be at least 10 characters long.");
            return false;
        }

        return true;
    }

    // Bind form submission to AJAX handler
    $("#contact-form").on("submit", function (e) {
        e.preventDefault(); // Prevent the default form submission

        // Gather form data
        let formData = {
            email: $("#email").val(), // Get email value from input field
            message: $("#message").val(), // Get message value from textarea
        };

        // Validate form data
        let isValidFormData = validateData(formData);

        if (isValidFormData) {
            // Send AJAX POST request
            $.ajax({
                url: "https://run.mocky.io/v3/2bedc58a-bc53-4326-83a3-f27758609b90", // API endpoint for contact form submission
                type: "POST",
                data: JSON.stringify(formData), // Send data as JSON
                contentType: "application/json", // Set content type to JSON
                dataType: "json", // Expect a plain text response
                success: function (data) {
                    // Handle successful response
                    alert("Message sent successfully!");
                    $("#contact-form")[0].reset(); // Reset form after successful submission
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle errors
                    alert("An error occurred. Please try again.");
                    console.error("Error:", textStatus, errorThrown);
                },
            });
        }
    });
});