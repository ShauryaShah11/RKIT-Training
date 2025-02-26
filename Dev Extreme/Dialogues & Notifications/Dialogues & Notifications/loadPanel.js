$(function () {
    // Initialize Load Panel - a UI element to show loading indication
    var loadPanel = $("#loadPanelContainer").dxLoadPanel({
        position: { of: "#gridContainer" }, // Position relative to grid container
        // position: { of: "body" } // Centers the load panel on the whole page
        // position: {
        //     my: "center",  // Load panel’s center
        //     at: "center",  // Align with center of target element
        //     of: "#gridContainer" // Target element
        // }
        // my / at Values	Effect
        // "left top"	Top-left corner
        // "center top"	Centered at top
        // "right top"	Top-right corner
        // "center center"	Fully centered
        // "left bottom"	Bottom-left corner
        // "right bottom"	Bottom-right corner
        
        visible: false, // Initially hidden
        shading: true, // Enables background shading when visible
        showPane: true, // Displays the panel content
        hideOnOutsideClick: true, // Allows hiding by clicking outside
        message: "Fetching data from API...", // Message displayed on the load panel
        shadingColor: "rgba(0,0,0,0.4)", // Background shading color
        animation: {
            show: { type: "fade", from: 0, to: 1, duration: 400 }, // Fade-in effect
            hide: { type: "fade", from: 1, to: 0, duration: 400 }  // Fade-out effect
        },
        container: "#gridContainer", // Attach to grid container
        deferRendering: true, // Renders the panel immediately instead of on-demand
        onHiding: function () {
            console.log("Load panel hidden");
        },
        onContentReady: function () {
            console.log("Load panel content is ready");
        },
        onShowing: function () {
            console.log("Load panel is about to be shown");
        }
    }).dxLoadPanel("instance");

    // Initialize DataGrid (Initially empty)
    $("#gridContainer").dxDataGrid({
        dataSource: [], // Empty data source initially
        columns: [
            { dataField: "id", caption: "ID", width: 50 }, // ID column
            { dataField: "firstName", caption: "Name" }, // Name column
            { dataField: "email", caption: "Email" }, // Email column
            { dataField: "gender", caption: "Gender" }, // Gender column
            { dataField: "age", caption: "Age (Years)" } // Age column
        ],
        showBorders: true // Enables grid border visibility
    });

    // Button to fetch data from API
    $("#fetchDataBtn").dxButton({
        text: "Fetch Data", // Button label
        type: "success", // Green success button
        onClick: function () {
            loadPanel.show(); // Show loading panel before making API call
            $.ajax({
                url: "https://67ac7b0a5853dfff53dae5a1.mockapi.io/api/v1/users", // API endpoint
                method: "GET", // HTTP GET request
                dataType: "json", // Expect JSON response
                success: function (data) {
                    // Simulate loading delay before updating DataGrid
                    setTimeout(function () {
                        $("#gridContainer").dxDataGrid("instance").option("dataSource", data);
                        loadPanel.hide(); // Hide load panel after data is loaded
                    }, 2000);
                },
                error: function () {
                    alert("Error fetching data!"); // Show error message if request fails
                    loadPanel.hide(); // Hide load panel on error
                },
                complete: function () {
                    // No additional actions on complete
                }
            });
        }
    });

    // Button to manually hide Load Panel
    $("#hideLoadPanelButton").dxButton({
        text: "Hide Load Panel", // Button label
        type: "danger", // Red danger button
        onClick: function () {
            loadPanel.hide(); // Hide load panel when clicked
        }
    });
});
