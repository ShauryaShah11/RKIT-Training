$(function () {
    let virtualStore = new DevExpress.data.CustomStore({
        key: 'id',  // Define the key field for identifying records
        load: async function () {
            try {
                let data = await $.get("https://dummyjson.com/users?limit=50&skip=0&select=id,firstName,lastName,age,username,email");
                return data.users;
            } catch (error) {
                DevExpress.ui.notify('Error loading data', 'error', 2000);
                console.error('Load Error:', error);
            }
        },
        insert: async function (values) {
            try {
                let result = await $.post("https://dummyjson.com/users/add", values);
                DevExpress.ui.notify(`Row added: ${JSON.stringify(result)}`, 'success', 2000);
                return result;
            } catch (error) {
                DevExpress.ui.notify('Error adding data', 'error', 2000);
                console.error('Insert Error:', error);
            }
        },
        update: async function (key, values) {
            try {
                let result = await $.ajax({
                    url: `https://dummyjson.com/users/${key}`,
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
        remove: async function (key) {
            try {
                await $.ajax({
                    url: `https://dummyjson.com/users/${key}`,
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
    $('#dataGridRowEditing').dxDataGrid({
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
    });

    // Batch Editing
    $('#dataGridBatchEditing').dxDataGrid({
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
    })

    // Cell Editing
    $('#dataGridCellEditing').dxDataGrid({
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
    });

    // Form Editing
    $('#dataGridFormEditing').dxDataGrid({
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
    })

    $('#dataGridPopupEditing').dxDataGrid({
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
            mode: 'popup',
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
    })
});
