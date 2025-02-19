$(function () {
    // Creating a custom store for data management with CRUD operations
    let virtualStore = new DevExpress.data.CustomStore({
        key: 'id',  // Unique identifier for each record

        // Function to load data from API
        load: async function () {
            try {
                let data = await $.get("https://67ac7b0a5853dfff53dae5a1.mockapi.io/api/v1/users");
                return data;
            } catch (error) {
                DevExpress.ui.notify('Error loading data', 'error', 2000);
                console.error('Load Error:', error);
            }
        },

        // Function to insert new data into API
        insert: async function (values) {
            try {
                let result = await $.post("https://67ac7b0a5853dfff53dae5a1.mockapi.io/api/v1/users/add", values);
                DevExpress.ui.notify(`Row added: ${JSON.stringify(result)}`, 'success', 2000);
                return result;
            } catch (error) {
                DevExpress.ui.notify('Error adding data', 'error', 2000);
                console.error('Insert Error:', error);
            }
        },

        // Function to update an existing record
        update: async function (key, values) {
            try {
                let result = await $.ajax({
                    url: `https://67ac7b0a5853dfff53dae5a1.mockapi.io/api/v1/users/${key}`,
                    method: 'PUT',
                    data: values
                });
                DevExpress.ui.notify(`Row updated: ${JSON.stringify(result)}`, 'success', 2000);
                return result;
            } catch (error) {
                DevExpress.ui.notify('Error updating data', 'error', 2000);
                console.error('Update Error:', error);
            }
        },

        // Function to delete a record
        remove: async function (key) {
            try {
                await $.ajax({
                    url: `https://67ac7b0a5853dfff53dae5a1.mockapi.io/api/v1/users/${key}`,
                    method: 'DELETE'
                });
                DevExpress.ui.notify(`Row removed: ID = ${key}`, 'success', 2000);
            } catch (error) {
                DevExpress.ui.notify('Error removing data', 'error', 2000);
                console.error('Remove Error:', error);
            }
        }
    });

    // Define fixed columns to merge with dynamically generated ones
    let customColumns = [
        { dataField: 'id', caption: 'ID', width: 50, fixed: true },
        { dataField: 'firstName', caption: 'First Name', allowSorting: false },
        { dataField: 'username', caption: 'Username', visible: false }, // Initially hidden
        { dataField: 'email', caption: 'Email', sortOrder: 'asc' }, // Default sorting applied
        { type: 'buttons', width: 110, buttons: ['edit', 'delete'] } // Action buttons
    ];

    let dynamicColumns;
    // Load data to extract dynamic column names
    virtualStore.load().done(function (data) {
        if (data && data.length > 0) {
            dynamicColumns = Object.keys(data[0]).map(field => ({
                dataField: field,
                caption: field.charAt(0).toUpperCase() + field.slice(1), // Capitalize column names
                allowEditing: field !== "id" // Make ID column read-only
            }));
        }
    });

    // Initialize DevExtreme DataGrid
    $("#gridContainer").dxDataGrid({
        dataSource: virtualStore, // Attach custom store
        columns: customColumns, // Use merged columns
        columnFixing: { enabled: true }, // Enable column fixing
        columnChooser: { enabled: true }, // Allow users to choose columns dynamically
        showBorders: true, // Show grid borders
        allowColumnReordering: true, // Allow dragging to reorder columns
        allowColumnResizing: true, // Allow resizing columns
        columnAutoWidth: true, // Auto width adjustment
        paging: { pageSize: 10 }, // Pagination settings
        editing: {
            mode: 'row', // Enable inline editing
            allowUpdating: true, // Allow editing
            allowDeleting: true, // Allow deletion
            allowAdding: true // Allow adding new rows
        },
        onRowUpdating: function (e) {
            console.log('Row updating:', e.oldData, '=>', e.newData);
        },
        onRowInserted: function (e) {
            console.log('Row inserted:', e.data);
        },
        onRowRemoved: function (e) {
            console.log('Row removed:', e.data);
        }
    });

    // Multi - Row Header
    // Initialize DevExtreme DataGrid
    $("#multiRowHeader").dxDataGrid({
        dataSource: virtualStore, // Attach custom store
        columns: [
            {
                dataField: 'id',
                caption: 'ID',
                width: 50,
                fixed: true
            },
            {
                caption: 'Full Name',
                columns: [
                    { dataField: 'firstName', caption: 'First Name' },
                    { dataField: 'lastName', caption: 'Last Name' }
                ]
            },
            {
                dataField: 'username',
                caption: 'Username'
            },
            {
                dataField: 'email',
                caption: 'Email'
            },
            {
                type: "buttons",
                width: 150,
                buttons: [
                    {
                        name: 'edit',
                        icon: 'edit'
                    },
                    {
                        name: 'delete',
                        icon: 'remove',
                        // visible: function(e){
                        //     return e.row.data.id % 2 == 0;
                        // },
                    },
                    {
                        hint: "Send Email",
                        icon: "email",
                        onClick: function (e) {
                            alert(`Sending email to ${e.row.data.firstName}`);
                        }
                    }
                ]
            }

        ], // Use merged columns
        columnFixing: { enabled: true }, // Enable column fixing
        columnChooser: { enabled: true }, // Allow users to choose columns dynamically
        showBorders: true, // Show grid borders
        allowColumnReordering: true, // Allow dragging to reorder columns
        allowColumnResizing: true, // Allow resizing columns
        columnResizingMode: "widget", // widget, nextColumn
        columnAutoWidth: true, // Auto width adjustment
        paging: { pageSize: 10 }, // Pagination settings
        editing: {
            mode: 'row', // Enable inline editing
            allowUpdating: true, // Allow editing
            allowDeleting: true, // Allow deletion
            allowAdding: true // Allow adding new rows
        },
        stateStoring: {
            enabled: true,
            type: 'localStorage', // sessionStorage, custom
            key: 'gridState',
            ignore: ["sorting", "filtering"]
        },
        onRowUpdating: function (e) {
            console.log('Row updating:', e.oldData, '=>', e.newData);
        },
        onRowInserted: function (e) {
            console.log('Row inserted:', e.data);
        },
        onRowRemoved: function (e) {
            console.log('Row removed:', e.data);
        }
    });

    $("#resetState").dxButton({
        text: "Reset Grid State",
        onClick: function () {
            localStorage.removeItem("storage"); // Clear stored state
            location.reload(); // Reload page to apply reset
        }
    });
    
});
