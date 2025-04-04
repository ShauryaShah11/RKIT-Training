$(async function () {
    // Local data source
    let localData = [
        { id: 1, firstName: "John", username: "john_doe", email: "john@example.com" },
        { id: 2, firstName: "Jane", username: "jane_doe", email: "jane@example.com" },
        { id: 3, firstName: "Alice", username: "alice_w", email: "alice@example.com" },
        { id: 4, firstName: "Bob", username: "bob_smith", email: "bob@example.com" },
        { id: 5, firstName: "Charlie", username: "charlie_b", email: "charlie@example.com" },
        { id: 6, firstName: "Eve", username: "eve_h", email: "eve@example.com" },
        { id: 7, firstName: "Frank", username: "frank_t", email: "frank@example.com" },
        { id: 8, firstName: "Grace", username: "grace_k", email: "grace@example.com" },
        { id: 9, firstName: "Hank", username: "hank_m", email: "hank@example.com" },
        { id: 10, firstName: "Ivy", username: "ivy_l", email: "ivy@example.com" }
    ];

    // Initialize the second data grid
    let gridContainer2 = $('#gridContainer2').dxDataGrid({
        dataSource: localData, // Use local data source
        keyExpr: 'id', // Define the key field for identifying records
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 }, // Display 'ID'
            { dataField: 'firstName', caption: 'First Name', allowSorting: false }, // Display 'First Name' with no sorting
            { dataField: 'username', caption: 'Username' }, // Display 'Username'
            { dataField: 'email', caption: 'Email' }, // Display 'Email'
            { type: 'buttons', width: 110, buttons: ['edit', 'delete'] } // Add Edit and Delete buttons
        ],
        showBorders: true, // Enable borders for the grid
        searchPanel: {
            visible: true // Enable the search panel
        },
        selection: {
            mode: "multiple", // Allow multiple selection mode
            selectAllMode: "page", // Select all rows on the current page
            allowSelectAll: false, // Disable the select all checkbox
            showCheckBoxesMode: "always" // Always show checkboxes for selection
        },
        paging: {
            pageSize: 10 // Set pagination to 10 rows per page
        },
        editing: {
            mode: 'row', // Set row editing mode
            allowUpdating: true, // Allow updates
            allowDeleting: true, // Allow deletions
            allowAdding: true // Allow adding new rows
        },
        // Event handler for selection change
        onSelectionChanged: function (e) {
            getDatafromDataSource();
        }
    }).dxDataGrid("instance");
    
    // Function to get selected rows directly from the dataSource
    function getDatafromDataSource() {
        const dataSource = gridContainer2.getDataSource().items();

        const selectedKeys = gridContainer2.option("selectedRowKeys");

        const selectedRows = dataSource.filter(row => selectedKeys.includes(row.id));

        console.log("Selected Rows from DataSource:", selectedRows);
    }

});