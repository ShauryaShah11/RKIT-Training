$(function () {
    // Initialize Toast
    var toast = $("#toastContainer").dxToast({
        message: "Hello! This is a simple toast notification.",
        type: "info", // Types: 'info', 'success', 'warning', 'error'
        displayTime: 3000, // Toast will disappear after 3 seconds
        position: "bottom center", // Position on screen
        animation: {
            show: { type: "fade", duration: 400 },
            hide: { type: "fade", duration: 400 },
        }
    }).dxToast("instance");

    // Button to trigger Toast
    $("#showToastBtn1").dxButton({
        text: "Show Toast",
        type: "success",
        onClick: function () {
            toast.show();
        }
    });

    var activeToasts = []; // Array to store active toasts
    var maxToasts = 5; // Maximum stacked toasts
    var toastSpacing = 20; // Space between stacked toasts

    // Function to Show Multiple Toasts (Stacking with Auto-Clear)
    function showToast(message, type) {
        if (activeToasts.length >= maxToasts) {
            // Remove the oldest toast if limit is reached
            var oldToast = activeToasts.shift();
            oldToast.dispose(); // Remove from DOM
        }

        // Adjust offsets based on active toast count
        let newOffset = activeToasts.length * (50 + toastSpacing);

        let toast = $("<div>").appendTo("body").dxToast({
            message: message,
            type: type, // 'info', 'success', 'warning', 'error'
            displayTime: 3000, // Toast disappears after 3 seconds
            position: {
                my: "bottom center",
                at: "bottom center",
                offset: `0 -${newOffset}px`, // Dynamic spacing between toasts
            },
            closeOnClick: true, // Allow users to close manually
            animation: {
                show: { type: "fade", duration: 400 },
                hide: { type: "fade", duration: 400 },
            },
            onHiding: function () {
                activeToasts = activeToasts.filter(t => t !== toast);
                // Adjust offsets of remaining toasts
                activeToasts.forEach((t, index) => {
                    t.option("position.offset", `0 -${index * (50 + toastSpacing)}px`);
                });
            }
        }).dxToast("instance");

        // Add custom styling for better appearance
        $(".dx-toast-content").css({
            "border-radius": "10px",
            "box-shadow": "0px 4px 10px rgba(0, 0, 0, 0.2)",
            "font-size": "16px",
            "padding": "12px",
        });

        toast.show();
        activeToasts.push(toast); // Store active toast instance
    }

    // Buttons to trigger stacked toasts
    $("#showToastBtn2").dxButton({
        text: "Show Toast",
        type: "success",
        onClick: function () {
            showToast("This is a new toast message!", "info");
        }
    });

    $("#showErrorToastBtn").dxButton({
        text: "Show Error Toast",
        type: "danger",
        onClick: function () {
            showToast("An error occurred!", "error");
        }
    });
}); 