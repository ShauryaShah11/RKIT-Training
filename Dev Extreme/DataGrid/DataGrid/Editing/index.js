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

    // Row Editing
    let rowDataGridInstance = $('#dataGridRowEditing').dxDataGrid({
        dataSource: virtualStore,  // Use the custom store with CRUD operations
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            { dataField: 'firstName', caption: 'First Name' },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,
        allowColumnReordering: true,
        scrolling: {
            mode: 'virtual'
        },
        editing: {
            mode: 'row',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
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
    }).dxDataGrid("instance");

    // Batch Editing
    let batchDataGridInstance = $('#dataGridBatchEditing').dxDataGrid({
        dataSource: virtualStore,  // Use the custom store with CRUD operations
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            { dataField: 'firstName', caption: 'First Name' },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,
        allowColumnReordering: true,
        scrolling: {
            mode: 'virtual'
        },
        editing: {
            mode: 'batch',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            allowExporting: true
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
    }).dxDataGrid("instance");

    // Cell Editing
    let cellDataGridInstance = $('#dataGridCellEditing').dxDataGrid({
        dataSource: virtualStore,  // Use the custom store with CRUD operations
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            { dataField: 'firstName', caption: 'First Name' },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,
        allowColumnReordering: true,
        scrolling: {
            mode: 'virtual'
        },
        editing: {
            mode: 'cell',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
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
    }).dxDataGrid("instance");

    // Form Editing
    let formDataGridInstance = $('#dataGridFormEditing').dxDataGrid({
        dataSource: virtualStore,  // Use the custom store with CRUD operations
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            { dataField: 'firstName', caption: 'First Name' },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,
        allowColumnReordering: true,
        scrolling: {
            mode: 'virtual'
        },
        editing: {
            mode: 'form',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
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
    }).dxDataGrid("instance");

    // Popup Editing for DataGrid with various event handlers
    let popupDataGridInstance = $('#dataGridPopupEditing').dxDataGrid({
        dataSource: virtualStore,  // Use the custom store with CRUD operations
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            { dataField: 'firstName', caption: 'First Name' },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' },
            { type: 'buttons', width: 110, buttons: ['edit', 'delete'] },
        ],
        showBorders: true,
        allowColumnReordering: true,
        scrolling: {
            mode: 'virtual'  // For large datasets, improves performance by loading rows as you scroll
        },
        editing: {
            mode: 'popup',   // Popup form for editing data
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            popup: {
                title: "Edit User",
                showTitle: true,
                width: 700,
                height: 345
            }
        },
        // Triggered before a row update. Use it to validate or log the changes.
        onRowUpdating: function (e) {
            console.log('Row updating:', e.oldData, '=>', e.newData);
            // Example use case: Validate data before updating.
        },
        // Triggered after a new row is inserted.
        onRowInserted: function (e) {
            console.log('Row inserted:', e.data);
            // Example use case: Show success message after adding a new record.
        },
        // Triggered after a row is removed.
        onRowRemoved: function (e) {
            console.log('Row removed:', e.data);
            // Example use case: Log or audit deletion.
        },
        // Customize the adaptive detail row for small screens.
        onAdaptiveDetailRowPreparing: function (e) {
            e.formOptions.colCount = 2;
            e.formOptions.colCountByScreen = { xs: 1, sm: 2 };
            e.formOptions.labelLocation = 'left';
            // Example use case: Customize form layout based on screen size.
        },
        // Log the clicked cell details.
        onCellClick: function (e) {
            console.log(e.rowIndex, e.column, e.value, e.data);
            // Example use case: Perform action based on clicked cell value.
        },
        // Log data when a cell is double-clicked.
        onCellDblClick: function (e) {
            console.log(e.rowIndex, e.data);
            // Example use case: Open a detailed view of the row on double-click.
        },
        // Change the background color on cell hover.
        onCellHoverChanged: function (e) {
            if (e.event.type === 'mouseover') {
                e.cellElement.css("background-color", "#f0f8ff");
            } else {
                e.cellElement.css("background-color", "");
            }
        },
        // Customize cell appearance.
        onCellPrepared: function (e) {
            if (e.rowType === "data" && e.column.dataField === "id") {
                e.cellElement.css("background-color", e.value % 2 === 0 ? "lightgreen" : "lightcoral");
            }
        },
        // Customize the right-click context menu.
        onContextMenuPreparing: function (e) {
            if (e.items == undefined) {
                e.items = [];
            }
            if (e.row && e.row.rowType === "data") {
                e.items.push({
                    text: "Export",
                    onItemClick: function () {
                        alert("Export data clicked!");
                    }
                });
            }
        },
        // Handle data errors.
        onDataErrorOccurred: function (e) {
            console.error("Data Error:", e.error.message);
            DevExpress.ui.notify("An error occurred: " + e.error.message, "error", 2000);
        },
        // Log when an edit is canceled.
        onEditCanceled: function (e) {
            console.log("Edit canceled for row:", e.rowIndex, "Row data:", e.data);
            DevExpress.ui.notify("Edit was canceled", "info", 2000);
        },
        // Confirm before canceling an edit.
        onEditCanceling: function (e) {
            if (!confirm("Are you sure you want to cancel the edit?")) {
                e.cancel = true;
            }
        },
        // Handle the start of editing.
        onEditingStart: function (e) {
            console.log("Editing started:", e);
        },
        // Customize editor after it is created.
        onEditorPrepared: function (e) {
            console.log("Editor prepared:", e);
        },
        // Handle editor preparation.
        onEditorPreparing: function (e) {
            console.log("Preparing editor for:", e);
        },
        // Log export event.
        onExporting: function (e) {
            console.log("Exporting data:", e);
        },
        // Log the focused cell change.
        onFocusedCellChanged: function (e) {
            console.log("Focused cell changed:", e);
        },
        // Confirm before changing the focused cell.
        onFocusedCellChanging: function (e) {
            console.log("Focused cell changing:", e);
        },
        // Handle new row initialization.
        oninitNewRow: function (e) {
            console.log("Initializing new row:", e);
        },
        // Handle key press events.
        onkeyDown: function (e) {
            console.log("Key down event:", e);
        },
        // Handle option changes.
        onOptionChanged: function (e) {
            console.log("Option changed:", e);
        },
        // Log row click event.
        onRowClick: function (e) {
            console.log("Row clicked:", e);
        },
        // Handle row collapse.
        onRowCollapsed: function (e) {
            console.log("Row collapsed:", e);
        },
        onrowCollapsing: function (e) {
            console.log("Row collapsing:", e);
        },
        onrowDblClick: function (e) {
            console.log("Row double-clicked:", e);
        },
        onrowExpanded: function (e) {
            console.log("Row expanded:", e);
        },
        onabortrowExpanding: function (e) {
            console.log("Row expansion aborted:", e);
        },
        onrowInserted: function (e) {
            console.log("Row inserted:", e);
        },
        onrowInserting: function (e) {
            console.log("Row inserting:", e);
        },
        onrowPrepared: function (e) {
            console.log("Row prepared:", e);
        },
        onrowRemoved: function (e) {
            console.log("Row removed:", e);
        },
        onrowRemoving: function (e) {
            console.log("Row removing:", e);
        },
        onSaving: function (e) {
            console.log("Saving changes:", e);
        },
        onselectionChanged: function (e) {
            console.log("Selection changed:", e);
        },
        ontoolbarPreparing: function (e) {
            console.log("Toolbar preparing:", e);
        }
    }).dxDataGrid("instance");

    $('#dataGridValidation').dxDataGrid({
        dataSource: virtualStore,  // The data source for the grid
        columns: [
            {
                dataField: 'id',
                caption: 'ID',
                width: 50,
                validationRules: [
                    { type: 'required', message: 'ID is required' },  // Ensures the field is not empty
                    { type: 'numeric', message: 'Id must be a number' },  // Ensures the value is numeric
                    { type: 'range', min: 1, max: 10000, message: 'Id must be between 1 and 10000' }  // Ensures the value is within a specific range
                ]
            },
            {
                dataField: 'firstName',
                caption: 'First Name',
                validationRules: [
                    { type: 'required', message: 'First Name is required' },
                    { type: 'stringLength', min: 3, max: 50, message: 'First Name must be between 3 and 50 characters' }
                ]
            },
            {
                dataField: 'username',
                caption: 'Username',
                validationRules: [
                    { type: 'required', message: 'Username is required' },
                    { type: 'stringLength', min: 5, max: 20, message: 'Username must be between 5 and 20 characters' },
                    {
                        type: 'custom',  // Custom validation rule
                        validationCallback: function (e) {
                            const regex = /^[a-zA-Z0-9_]+$/;
                            return regex.test(e.value);  // Ensures username contains only letters, numbers, and underscores
                        },
                        message: 'Username can only contain letters, numbers, and underscores'
                    }
                ]
            },
            {
                dataField: 'email',
                caption: 'Email',
                validationRules: [
                    { type: 'required', message: 'Email is required' },
                    { type: 'email', message: 'Please enter a valid email address' }
                ]
            },
            {
                type: 'buttons',
                width: 110,
                buttons: ['edit', 'delete']  // Provides Edit and Delete buttons for each row
            },
        ],
        showBorders: true,
        allowColumnReordering: true,  // Allows columns to be reordered by dragging
        scrolling: {
            mode: 'virtual'  // For large datasets, rows are loaded as you scroll for better performance
        },
        editing: {
            mode: 'popup',  // Editing is done in a popup form
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            popup: {
                title: "Edit User",
                showTitle: true,
                width: 700,
                height: 345
            },
        },

        // Triggered before the editor is created
        onEditorPreparing: function (e) {
            if (e.dataField == 'username') {
                e.editorOptions.placeholder = 'Enter a unique username';  // Adds a placeholder to the username editor
            }
        },

        // Triggered after the editor is created and ready to use
        onEditorPrepared: function (e) {
            if (e.dataField === 'email') {
                console.log('Email editor is ready');  // Logs a message when the email editor is ready
            }
        },

        // Triggered when a row is being validated (e.g., before saving changes)
        onRowValidating: function (e) {
            if (e.newData.username && e.newData.username.length < 5) {
                e.isValid = false;
                e.errorText = 'Username must be at least 5 characters long.';  // Adds a custom validation error for the row
            }
        },

        // Triggered when a row is being updated
        onRowUpdating: function (e) {
            if (!e.newData.firstName) {
                e.cancel = true;  // Cancels the update if the First Name is empty
                alert('First Name cannot be empty.');
            }
        },

        // Triggered when a new row is being inserted
        onRowInserting: function (e) {
            if (!e.data.email.includes('@')) {
                e.cancel = true;  // Cancels the insert if the email is invalid
                alert('Please enter a valid email.');
            }
        },

        // Triggered when a row is being removed
        onRowRemoving: function (e) {
            if (e.data.id < 10) {
                e.cancel = true;  // Cancels the removal if the ID is less than 10
                alert('You cannot delete rows with ID less than 10.');
            }
        },

        // Triggered before changes are saved
        onSaving: function (e) {
            console.log('Saving all changes:', e.changes);  // Logs all changes before they are saved
        },

        // Triggered after changes are successfully saved
        onSaved: function (e) {
            alert('Changes saved successfully.');
        },

        // Triggered when a validation error occurs
        onValidationError: function (e) {
            console.log('Validation error:', e.message);  // Logs the validation error message
        }
    });

    $('#dataBatchValidation').dxDataGrid({
        // Data source for the grid
        dataSource: virtualStore,

        // Defining the columns with validation rules
        columns: [
            {
                dataField: 'id',                // Column for 'id'
                caption: 'ID',                  // Display name for the column
                width: 50,                     // Width of the column
                validationRules: [             // Validation rules for 'id'
                    { type: 'required', message: 'ID is required' }, // Must be filled
                    { type: 'numeric', message: 'Id must be a number' }, // Must be a number
                    { type: 'range', min: 1, max: 10000, message: 'Id must be between 1 and 10000' } // Must be within the range 1–10000
                ]
            },
            {
                dataField: 'firstName',         // Column for 'firstName'
                caption: 'First Name',
                validationRules: [             // Validation rules for 'firstName'
                    { type: 'required', message: 'First Name is required' }, // Cannot be empty
                    { type: 'stringLength', min: 3, max: 50, message: 'First Name must be between 3 and 50 characters' } // String length between 3 and 50
                ]
            },
            {
                dataField: 'username',          // Column for 'username'
                caption: 'Username',
                validationRules: [             // Validation rules for 'username'
                    { type: 'required', message: 'Username is required' }, // Cannot be empty
                    { type: 'stringLength', min: 5, max: 20, message: 'Username must be between 5 and 20 characters' }, // Length check
                    {
                        type: 'custom',         // Custom validation for username
                        validationCallback: function (e) {
                            const regex = /^[a-zA-Z0-9_]+$/;
                            return regex.test(e.value); // Username must only contain letters, numbers, and underscores
                        },
                        message: 'Username can only contain letters, numbers, and underscores'
                    }
                ]
            },
            {
                dataField: 'email',             // Column for 'email'
                caption: 'Email',
                validationRules: [             // Validation rules for 'email'
                    { type: 'required', message: 'Email is required' }, // Cannot be empty
                    { type: 'email', message: 'Please enter a valid email address' } // Must be a valid email format
                ]
            },
            {
                type: 'buttons',               // Column for action buttons (edit, delete)
                width: 110,
                buttons: ['edit', 'delete']    // Provides buttons for editing and deleting rows
            }
        ],

        // Enabling borders and allowing column reordering
        showBorders: true,
        allowColumnReordering: true,

        // Scrolling configuration for large datasets
        scrolling: {
            mode: 'virtual'  // Loads rows as you scroll for improved performance
        },

        // Editing configuration
        editing: {
            mode: 'cell',   // Allows editing directly in the grid cell
            allowUpdating: true,  // Enables updating records
            allowDeleting: true,  // Enables deleting records
            allowAdding: true,    // Enables adding new records
            popup: {              // Popup configuration (not used here since 'cell' editing is enabled)
                title: "Edit User",
                showTitle: true,
                width: 700,
                height: 345
            }
        },

        // Triggered before an editor is created
        onEditorPreparing: function (e) {
            if (e.dataField === 'username') {
                e.editorOptions.placeholder = 'Enter a unique username'; // Adds a placeholder for the username field
            }
        },

        // Triggered after an editor is created
        onEditorPrepared: function (e) {
            if (e.dataField === 'email') {
                console.log('Email editor is ready'); // Logs when the email editor is prepared
            }
        },

        // Row-level validation before saving changes
        onRowValidating: function (e) {
            if (e.newData.username && e.newData.username.length < 5) {
                e.isValid = false;  // Marks validation as failed
                e.errorText = 'Username must be at least 5 characters long.';
            }
            if (e.newData.email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(e.newData.email)) {
                e.isValid = false;  // Marks validation as failed
                e.errorText = 'Please enter a valid email address.';
            }
        }
    });

});
