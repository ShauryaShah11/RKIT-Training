// Add event listener to the login form for the submit event
document.getElementById("login-form").addEventListener("submit", onSubmit);

// Function to handle form submission
function onSubmit(event) {
    event.preventDefault(); // Prevent the default form submission

    // Get email and password input values
    let emailInput = document.getElementById("email").value; // Get email from input field
    let passwordInput = document.getElementById("password").value; // Get password from input field

    // Retrieve user data from localStorage
    let user = localStorage.getItem("user");
    user = JSON.parse(user); // Parse the JSON string to an object

    // Check if the entered email and password match the stored user data
    if (user.email === emailInput && user.password === passwordInput) {
        alert("Successfully logged in"); // Show success alert
        window.location.href = "index.html"; // Redirect on success
    } else {
        alert("Invalid email or password"); // Show error alert
    }
}
