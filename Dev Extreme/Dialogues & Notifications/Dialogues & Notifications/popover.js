$(function () {
    $('#popoverContainer').dxPopover({
        title: 'Details',
        showTitle: true,
        target: "#detailsLink", // Attach to button
        showEvent: "dxclick", // Show popover on click
        // hideEvent: "mouseleave", // Hide when mouse leaves
        shading: true,
        shadingColor: 'rgba(0, 0, 0, 0.5)', 
        position: "bottom", // Position above the button
        width: 200,
        closeOnOutsideClick: false, // Prevent closing on outside click
        contentTemplate: function () {
            return $("<div>").html(`
                <p class="popover-text">This popover provides more information about the topic.</p>
                <a href="https://js.devexpress.com/" target="_blank" class="popover-link">Learn More</a>
                <br>
                <button id="closePopoverBtn" class="popover-close-btn">Close</button>
            `);
        },
        animation: {
            show: {
                type: 'pop',
                from: { scale: 0 },
                to: { scale: 1 },
            },
            hide: {
                type: 'fade',
                from: 1,
                to: 0,
            },
        },
        onContentReady: function (e) {
            // Attach event to close button after content is rendered
            setTimeout(function () {
                $("#closePopoverBtn").on("click", function () {
                    e.component.hide();
                });
            }, 0);
        }
    });

})