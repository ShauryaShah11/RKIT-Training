$(function () {
    // Sample static array of employee data
    const employees = [
        { ID: 1, Name: "John Doe", Position: "Manager", Age: 35 },
        { ID: 2, Name: "Jane Smith", Position: "Developer", Age: 28 },
        { ID: 3, Name: "Sam Johnson", Position: "Designer", Age: 42 }
    ];

    // ================== Data Binding with Simple Array ==================
    $('#dataGridContainer1').dxDataGrid({
        dataSource: employees, // Static array as the data source
        columns: [
            { dataField: 'ID', caption: 'ID', width: 50 },  // ID column with custom caption and width
            'Name',  // Simple column without custom settings
            'Position',  // Simple column without custom settings
            { dataField: 'Age', caption: 'Age (Years)' }  // Custom caption for the Age column
        ],
        showBorders: true, // Show grid borders for better visibility
    });

    // ================== Custom Store for Loading Data via AJAX ==================
    let store = new DevExpress.data.CustomStore({
        load: async function () {
            // Fetch data from the API
            let data = await $.get("https://dummyjson.com/users");
            return data.users; // Return only the 'users' array from the response
        }
    });

    // ================== Record Paging with AJAX Request ==================
    $('#dataGridContainer2').dxDataGrid({
        dataSource: store,  // Use the custom store as the data source
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },  // ID column with custom width
            {
                dataField: 'name', caption: 'Name', 
                calculateCellValue: function (data) {
                    // Combine firstName and lastName into a single "Name" column
                    return `${data.firstName} ${data.lastName}`;
                }
            },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,  // Show grid borders
        allowColumnReordering: true,  // Allow users to reorder columns
        paging: {
            pageSize: 10 // Display 10 rows per page
        },
        pager: {
            showPageSizeSelector: true,  // Allow the user to select page size
            allowedPageSizes: [10, 20, 30],  // Page size options
            showInfo: true,  // Show current page information
            displayMode: 'adaptive',  // Adaptive mode for better UI on small screens
            infoText: 'Page {0} of {1} ({2} items)',  // Custom information text
            showNavigationButtons: true,  // Display navigation buttons for paging
            visible: 'auto'  // Automatically show/hide the pager based on data size
        }
    });    
});
