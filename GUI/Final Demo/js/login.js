document.getElementById("login-form").addEventListener("submit", onSubmit);
document.getElementById("menu-toggle").addEventListener("click", toggleMenu);

function onSubmit(event) {
    event.preventDefault(); // Prevent the default form submission

    let emailInput = document.getElementById("email").value;
    let passwordInput = document.getElementById("password").value;

    let user = localStorage.getItem("user");
    user = JSON.parse(user);
   
    if (user.email === emailInput && user.password === passwordInput) {
        alert("Successfully logged in");
        window.location.href = "index.html"; // Redirect on success
    } else {
        alert("Invalid email or password"); // Show error alert
    }
}

function toggleMenu() {
    document.querySelector(".custom-nav-menu").classList.toggle("active");
}
