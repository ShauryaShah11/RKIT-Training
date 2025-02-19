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

    // Initializing the DevExtreme DataGrid
    $('#gridContainer').dxDataGrid({
        dataSource: virtualStore,  // Using the custom store
        showBorders: true,
        allowColumnReordering: true,  // Allow columns to be reordered by drag & drop

        // Column definitions
        columns: [
            {
                dataField: 'id',
                caption: 'ID',
                alignment: 'center',
                allowEditing: true,
                width: 50
            },
            {
                caption: 'Full Name',
                cellTemplate: function (container, options) {
                    let firstName = options.data.firstName;
                    let lastName = options.data.lastName;
                    container.text(firstName + " " + lastName);
                }
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
                dataField: 'age',
                caption: 'Age (Year)',
            },
            {
                dataField: 'birthDate',
                caption: 'BirthDate'
            },
            {
                dataField: 'gender',
                caption: 'Gender',
                alignment: 'center',
            },
            {
                type: 'buttons',
                caption: 'Action',
                buttons: [
                    { name: 'edit', icon: 'edit' },
                    { name: 'delete', icon: 'remove' }
                ]
            }
        ],

        // Virtual scrolling for handling large datasets
        scrolling: {
            mode: 'virtual'
        },
        summary: {
            totalItems: [
                {
                    column: "email",
                    summaryType: "count"
                }, 
                {
                    column: "age",
                    summaryType: "avg",
                    showInColumn: "gender",
                    alignment: "center" // or "left" | "right"
                },
                {
                    column: "age",
                    summaryType: "custom",
                    name: "customAgeSummary",
                    showInColumn: "age",
                    customizeText: function (data) {
                        return "Total Adults Age: " + data.value;
                    }
                }],
            calculateCustomSummary: function (options) {
                if (options.name === "customAgeSummary") {
                    if (options.summaryProcess === "start") {
                        options.totalValue = 0; // Reset total
                    }
                    if (options.summaryProcess === "calculate") {
                        if (options.value >= 18) { // Consider only users aged 18+
                            options.totalValue += 1;
                        }
                    }
                }
            },
        },

        // Editing configuration
        editing: {
            mode: 'cell',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        },

        // Event handlers for CRUD actions
        onRowUpdating: function (e) {
            console.log('Row updating:', e.oldData, '=>', e.newData);
        },
        onRowInserted: function (e) {
            console.log('Row inserted:', e.data);
        },
        onRowRemoved: function (e) {
            console.log('Row removed:', e.data);
        },

        // Toolbar with additional buttons and filters
        onToolbarPreparing: function (e) {
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    widget: 'dxButton',
                    options: {
                        icon: 'add',
                        text: 'Add Row',
                        onClick: function () {
                            $("#gridContainer").dxDataGrid("instance").addRow();
                        }
                    }
                },
                {
                    location: 'before',
                    widget: 'dxButton',
                    options: {
                        icon: 'refresh',
                        text: 'Refresh Data',
                        onClick: function () {
                            $("#gridContainer").dxDataGrid("instance").refresh();
                        }
                    }
                },
                {
                    location: 'before',
                    widget: 'dxSelectBox',
                    options: {
                        items: ['All', 'Male', 'Female'],
                        value: 'All',
                        onValueChanged: function (args) {
                            let grid = $("#gridContainer").dxDataGrid("instance");

                            if (args.value === 'All') {
                                grid.clearFilter(); // Remove all filters to show all data
                            } else {
                                grid.filter(["gender", "=", args.value]);
                            }
                        }
                    }
                },
                {
                    location: 'after',
                    widget: 'dxTextBox',
                    options: {
                        placeholder: "Search...",
                        onValueChanged: function (e) {
                            let grid = $("#gridContainer").dxDataGrid("instance");
                            grid.searchByText(e.value);
                        }
                    }
                }
            );
        }
    });

})

