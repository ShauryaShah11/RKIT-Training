$(function () {

    var data = [
        { ID: 1, Name: "John Doe", Age: 30, Position: "Software Engineer" },
        { ID: 2, Name: "Jane Smith", Age: 28, Position: "Project Manager" },
        { ID: 3, Name: "Mike Johnson", Age: 35, Position: "Designer" },
        { ID: 4, Name: "Emily Davis", Age: 25, Position: "HR Specialist" },
        { ID: 5, Name: "Chris Lee", Age: 40, Position: "CTO" },
        { ID: 6, Name: "Anna Garcia", Age: 32, Position: "Marketing Manager" },
        { ID: 7, Name: "David Wilson", Age: 38, Position: "Product Owner" },
        { ID: 8, Name: "Sarah Martinez", Age: 27, Position: "Business Analyst" },
        { ID: 9, Name: "James Brown", Age: 45, Position: "CEO" },
        { ID: 10, Name: "Jessica Taylor", Age: 33, Position: "Sales Director" },
        { ID: 11, Name: "Matthew Robinson", Age: 29, Position: "Web Developer" },
        { ID: 12, Name: "Olivia Harris", Age: 31, Position: "Data Scientist" },
        { ID: 13, Name: "Lucas Clark", Age: 34, Position: "Product Designer" },
        { ID: 14, Name: "Sophia Lewis", Age: 26, Position: "QA Engineer" },
        { ID: 15, Name: "Ethan Walker", Age: 42, Position: "Operations Manager" },
        { ID: 16, Name: "Ava Young", Age: 37, Position: "Customer Support Manager" },
        { ID: 17, Name: "Isabella Allen", Age: 30, Position: "Software Developer" },
        { ID: 18, Name: "Benjamin Scott", Age: 28, Position: "UX/UI Designer" },
        { ID: 19, Name: "Henry King", Age: 39, Position: "Sales Manager" },
        { ID: 20, Name: "Charlotte Lopez", Age: 41, Position: "Operations Director" }
    ];

    // Basic Widget (DataGrid) Creation
    $("#dataGridContainer").dxDataGrid({
        dataSource: data,
        columns: [
            { dataField: "ID", caption: "ID" },
            { dataField: "Name", caption: "Name" },
            { dataField: "Age", caption: "Age" },
            { dataField: "Position", caption: "Position" }
        ],
        paging: { pageSize: 5 },
        filterRow: { visible: false },
        searchPanel: { visible: true }
    });

    // Get a Widget (DataGrid) Instance
    var dataGridInstance = $("#dataGridContainer").dxDataGrid("instance");
    console.log("Initial DataGrid Instance:", dataGridInstance);

    // Get Widget (DataGrid) Options
    // Method 1: Using the instance to get an option (pageSize)
    var currentPageSize = dataGridInstance.option("paging.pageSize");
    console.log(`Current Page Size is : ${currentPageSize}`);

    // Method 2: Using jQuery to get an option (pageSize)
    var currentPageSize1 = $("#dataGridContainer").dxDataGrid("option", "paging.pageSize");
    console.log(`Current Page Size (Second Way) is : ${currentPageSize1}`);

    // Set Widget (DataGrid) Options
    $("#changeColumnWidth").click(function () {
        var columns = dataGridInstance.option("columns"); // Get current columns
        columns[1].width = 200; // Set width of the "Name" column to 0 (hidden)
        dataGridInstance.option("columns", columns); // Apply new column widths
        console.log("Column width changed.");
    });

    // Call Methods
    $("#disableGrid").click(function () {
        // Disable the DataGrid (make it unclickable and uneditable)
        dataGridInstance.option("disabled", true);
        console.log("DataGrid is disabled.");
    });

    // Handle Events
    // Change onRowClick Event
    dataGridInstance.option("onRowClick", function (e) {
        alert("Row was clicked! ID: " + e.data.ID);
    });

    // Destroy the Widget (DataGrid)
    $("#destroyGrid").click(function () {
        // Dispose the widget (remove functionality)
        dataGridInstance.dispose();
        console.log("After Dispose() :", dataGridInstance);

        // Check before clearing HTML
        console.log("Before empty() :", $("#dataGridContainer")[0].outerHTML);

        // Clear the HTML (remove the DataGrid from the DOM)
        $("#dataGridContainer").empty();
        console.log("After empty() :", $("#dataGridContainer")[0].outerHTML);
    });
});