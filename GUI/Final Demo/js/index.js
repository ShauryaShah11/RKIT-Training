// Add event listener to the menu toggle button
document.getElementById("menu-toggle").addEventListener("click", toggleMenu);

// Function to toggle the menu visibility
function toggleMenu() {
    // Toggle the 'active' class on the custom navigation menu
    document.querySelector(".custom-nav-menu").classList.toggle("active");
}

