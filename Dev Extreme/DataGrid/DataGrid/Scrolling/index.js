$(function() {
    // ================== Custom Store for Virtual Scrolling ==================
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

    // ================== Virtual Scrolling ==================
    $('#dataGridContainer1').dxDataGrid({
        dataSource: virtualStore,  // Use custom store for virtual scrolling
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
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
        allowColumnReordering: true,  // Allow column reordering
        scrolling: {
            mode: 'virtual'  // Enable virtual scrolling
        }
    });

    // ================== Infinite Scrolling ==================
    $('#dataGridContainer2').dxDataGrid({
        dataSource: virtualStore,  // Use the same custom store for infinite scrolling
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
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
        allowColumnReordering: true,  // Allow column reordering
        scrolling: {
            mode: 'infinite'  // Enable infinite scrolling
        }
    });
})