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

    $('#gridContainer1').dxDataGrid({
        dataSource: virtualStore,
        columns: [
            {
                dataField: 'id', caption: 'ID', width: 50
            },
            {
                dataField: 'firstName', caption: 'First Name', allowSorting: false
            },
            {
                dataField: 'username', caption: 'Username'
            },
            {
                dataField: 'email', caption: 'Email'
            },
            {
                type: 'buttons', width: 110, buttons: ['edit', 'delete']
            },
        ],
        showBorders: true,
        searchPanel: {
            visible: true
        },
        // In multiple mode, the user clicks a column header while pressing the Shift key to sort by the column. Sorting settings of other columns remain intact.
        sorting: {
            mode: "multiple" // or "multiple" | "none"
        },
        paging: {
            pageSize: 10
        },
        editing: {
            mode: 'cell',  // Options: 'row', 'popup', 'form', 'batch'
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        }
    });

    // API :- Initial and Runtime Sorting
    let gridContainer2 = $('#gridContainer2').dxDataGrid({
        dataSource: virtualStore,
        columns: [
            {
                dataField: 'id', caption: 'ID', width: 50
            },
            {
                dataField: 'firstName', caption: 'First Name', sortIndex: 1, sortOrder: 'asc'
            },
            {
                dataField: 'username', caption: 'Username', sortIndex: 0, sortOrder: 'desc'
            },
            {
                dataField: 'email', caption: 'Email'
            },
            {
                type: 'buttons', width: 110, buttons: ['edit', 'delete']
            },
        ],
        showBorders: true,
        searchPanel: {
            visible: true
        },
        // In multiple mode, the user clicks a column header while pressing the Shift key to sort by the column. Sorting settings of other columns remain intact.
        sorting: {
            mode: "multiple" // or "multiple" | "none"
        },
        paging: {
            pageSize: 10
        },
        editing: {
            mode: 'cell',  // Options: 'row', 'popup', 'form', 'batch'
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        }
    }).dxDataGrid("instance");

    $('#button').on('click', function (e) {
        gridContainer2.columnOption("username", {
            sortIndex: 0,
            sortOrder: 'asc'
        })
    });

    $('#btnClearSort').on('click', function (e) {
        gridContainer2.columnOption("firstName", {
            sortOrder: "none",  // Removes the sort order
            sortIndex: undefined
        });
        // gridContainer2.clearSorting();
    });


    // Custom Sorting 
    let data = [
        { id: 1, firstName: "John", lastName: "Doe", position: "Manager", isOnVacation: false },
        { id: 2, firstName: "Jane", lastName: "Smith", position: "Developer", isOnVacation: true },
        { id: 3, firstName: "Alice", lastName: "Brown", position: "Designer", isOnVacation: false },
        { id: 4, firstName: "Bob", lastName: "Johnson", position: "QA Engineer", isOnVacation: true },
        { id: 5, firstName: "Charlie", lastName: "Davis", position: "Team Lead", isOnVacation: false },
        { id: 6, firstName: "Emily", lastName: "Clark", position: "HR Specialist", isOnVacation: true },
        { id: 7, firstName: "Michael", lastName: "Lewis", position: "Product Owner", isOnVacation: false },
        { id: 8, firstName: "Olivia", lastName: "Wilson", position: "Scrum Master", isOnVacation: true },
        { id: 9, firstName: "Daniel", lastName: "Taylor", position: "Business Analyst", isOnVacation: false },
        { id: 10, firstName: "Sophia", lastName: "Anderson", position: "Support Engineer", isOnVacation: false }
    ];
    let gridContainer3 = $("#gridContainer3").dxDataGrid({
        dataSource: data,
        columns: [
            { dataField: "id", caption: "ID", width: 50 },
            { dataField: "firstName", caption: "First Name" },
            { dataField: "lastName", caption: "Last Name" },
            {
                dataField: "position",
                sortOrder: "asc", // desc will sort true value then false vale on isOnVacation field.
                // calculateSortValue: 'isOnVacation',
                calculateSortValue: function (rowData) {
                    // Custom sorting logic based on both isOnVacation and position
                    if (rowData.isOnVacation) {
                        return "zzz_" + rowData.position;  // Vacationing employees are sorted last
                    }
                    return "aaa_" + rowData.position;  // Non-vacationing employees are sorted first
                }
            }
        ],
        showBorders: true,
        sorting: {
            mode: "multiple"
        },
        paging: {
            pageSize: 5
        }
    });

})