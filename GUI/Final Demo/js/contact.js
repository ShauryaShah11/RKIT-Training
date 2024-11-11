$(document).ready(function () {
    // Bind form submission to AJAX handler
    $("#contact-form").on("submit", function (e) {
        e.preventDefault(); // Prevent the default form submission

        // Gather form data
        let formData = {
            email: $("#email").val(),
            message: $("#message").val(),
        };

        // Regular expression for email validation
        const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        // Regular expression for message validation (at least 10 characters)
        const messageRegex = /^.{10,}$/;

        // Validate email
        if (!emailRegex.test(formData.email)) {
            alert("Please enter a valid email address.");
            return;
        }

        // Validate message
        if (!messageRegex.test(formData.message)) {
            alert("Message must be at least 10 characters long.");
            return;
        }

        // Send AJAX POST request
        $.ajax({
            url: "https://example.com/api/contact",
            type: "POST",
            data: JSON.stringify(formData), // Send data as JSON
            contentType: "application/json", // Set content type to JSON
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
    });
});
