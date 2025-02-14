$(function () {
    // Initialize a custom store for the grid
    let virtualStore = new DevExpress.data.CustomStore({
        key: 'id',  // Define the unique identifier (primary key) for records in the store

        // Load data from an external API
        load: async function () {
            try {
                // Fetch data from the API (Dummy JSON API used for illustration)
                let data = await $.get("https://dummyjson.com/users?limit=50&skip=0");
                return data.users;  // Return the 'users' array from the API response
            } catch (error) {
                // Handle error if the data loading fails
                DevExpress.ui.notify('Error loading data', 'error', 2000); // Notify the user
                console.error('Load Error:', error);  // Log the error to the console
            }
        },

        // Insert new record into the store
        insert: async function (values) {
            try {
                // Send a POST request to add a new user
                let result = await $.post("https://dummyjson.com/users/add", values);
                // Notify the user that the row was added
                DevExpress.ui.notify(`Row added: ${JSON.stringify(result)}`, 'success', 2000);
                return result;  // Return the result of the insert
            } catch (error) {
                // Handle error if the insert fails
                DevExpress.ui.notify('Error adding data', 'error', 2000);
                console.error('Insert Error:', error);
            }
        },

        // Update existing record
        update: async function (key, values) {
            try {
                // Send a PUT request to update the user by their key (ID)
                let result = await $.ajax({
                    url: `https://dummyjson.com/users/${key}`,
                    method: 'PUT',
                    data: values
                });
                // Notify the user that the row was updated
                DevExpress.ui.notify(`Row updated: ${JSON.stringify(result)}`, 'success', 2000);
                return result;  // Return the updated result
            } catch (error) {
                // Handle error if the update fails
                DevExpress.ui.notify('Error updating data', 'error', 2000);
                console.error('Update Error:', error);
            }
        },

        // Remove record from the store
        remove: async function (key) {
            try {
                // Send a DELETE request to remove the user by their key (ID)
                await $.ajax({
                    url: `https://dummyjson.com/users/${key}`,
                    method: 'DELETE'
                });
                // Notify the user that the row was removed
                DevExpress.ui.notify(`Row removed: ID = ${key}`, 'success', 2000);
            } catch (error) {
                // Handle error if the remove fails
                DevExpress.ui.notify('Error removing data', 'error', 2000);
                console.error('Remove Error:', error);
            }
        }
    });

    // Initialize the first data grid
    $('#gridContainer1').dxDataGrid({
        dataSource: virtualStore,  // Set the data source to the virtual store
        keyExpr: 'id',  // Define the key field for identifying records
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },  // Display the 'ID' field
            { dataField: 'firstName', caption: 'First Name', allowSorting: false },  // Display 'First Name' with no sorting
            { dataField: 'username', caption: 'Username' },  // Display 'Username' field
            { dataField: 'email', caption: 'Email' },  // Display 'Email' field
            { type: 'buttons', width: 110, buttons: ['edit', 'delete'] },  // Add Edit and Delete buttons to each row
        ],
        showBorders: true,  // Enable borders for the grid
        searchPanel: {
            visible: true  // Show the search panel for filtering data
        },
        selection: {
            mode: "multiple",  // Allow multiple rows to be selected at once
            selectAllMode: "page",  // Allow selecting all rows on the current page
            allowSelectAll: false,  // Disable the "select all" checkbox
            showCheckBoxesMode: "always",  // Show checkboxes for selection
            deferred: true  // Enable deferred selection, meaning selection is handled via API calls
        },
        paging: {
            pageSize: 10  // Display 10 rows per page
        },
        editing: {
            mode: 'row',  // Editing mode: rows can be edited directly
            allowUpdating: true,  // Allow updating records
            allowDeleting: true,  // Allow deleting records
            allowAdding: true  // Allow adding new records
        }
    });

    // Initialize the second data grid
    let gridContainer2 = $('#gridContainer2').dxDataGrid({
        dataSource: virtualStore,  // Set the data source to the same virtual store
        keyExpr: 'id',  // Define the key field for identifying records
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },  // Display 'ID'
            { dataField: 'firstName', caption: 'First Name', allowSorting: false },  // Display 'First Name' with no sorting
            { dataField: 'username', caption: 'Username' },  // Display 'Username'
            { dataField: 'email', caption: 'Email' },  // Display 'Email'
            { type: 'buttons', width: 110, buttons: ['edit', 'delete'] },  // Add Edit and Delete buttons
        ],
        showBorders: true,  // Enable borders for the grid
        searchPanel: {
            visible: true  // Enable the search panel
        },
        selection: {
            mode: "multiple",  // Allow multiple selection mode
            selectAllMode: "page",  // Select all rows on the current page
            allowSelectAll: false,  // Disable the select all checkbox
            showCheckBoxesMode: "always",  // Always show checkboxes for selection
        },
        selectedRowKeys: [1, 5, 18],  // Initially select rows with these IDs
        paging: {
            pageSize: 10  // Set pagination to 10 rows per page
        },
        editing: {
            mode: 'row',  // Set row editing mode
            allowUpdating: true,  // Allow updates
            allowDeleting: true,  // Allow deletions
            allowAdding: true  // Allow adding new rows
        },
        // Event handler for selection change
        onSelectionChanged: function(e) {
            // Log selection-related data to the console
            var currentSelectedRowKeys = e.currentSelectedRowKeys;  // Get newly selected row keys
            var currentDeselectedRowKeys = e.currentDeselectedRowKeys;  // Get deselected row keys
            var allSelectedRowKeys = e.selectedRowKeys;  // Get all currently selected row keys
            var allSelectedRowsData = e.selectedRowsData;  // Get data of all selected rows
            console.log(currentDeselectedRowKeys, currentDeselectedRowKeys, allSelectedRowKeys, allSelectedRowsData);
        }
    }).dxDataGrid("instance");

    // Function to select a single row in the grid
    var selectSingleRow = function (gridContainer2, key, preserve) {
        // If the row is not already selected, select it
        if (!gridContainer2.isRowSelected(key)) {
            gridContainer2.selectRows([key], preserve);  // Use 'preserve' to keep existing selections
        }
    }

    // Test selection of row with key '1' and preserve current selection
    console.log(selectSingleRow(gridContainer2, 1, true));

    // Deselect all selected rows in the second grid
    gridContainer2.deselectAll();
    gridContainer2.clearSelection();  // Clear the selection of the grid
});
